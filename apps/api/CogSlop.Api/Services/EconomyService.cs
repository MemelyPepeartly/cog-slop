using System.Security.Claims;
using CogSlop.Api.Data;
using CogSlop.Api.Models.Dtos;
using CogSlop.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CogSlop.Api.Services;

public class EconomyService(
    CogSlopDbContext dbContext,
    ICurrentUserService currentUserService) : IEconomyService
{
    public async Task<DashboardDto> GetDashboardAsync(ClaimsPrincipal principal, CancellationToken cancellationToken)
    {
        var user = await currentUserService.EnsureUserAsync(principal, cancellationToken);
        var profile = await currentUserService.BuildProfileAsync(user, cancellationToken);
        var storeItems = await GetStoreItemsAsync(includeInactive: false, cancellationToken);

        return new DashboardDto(profile, storeItems);
    }

    public async Task<IReadOnlyList<StoreItemDto>> GetStoreItemsAsync(bool includeInactive, CancellationToken cancellationToken)
    {
        var query = dbContext.GearItems.AsNoTracking();
        if (!includeInactive)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderBy(x => x.CostInCogs)
            .ThenBy(x => x.Name)
            .Select(x => new StoreItemDto(
                x.GearItemId,
                x.Name,
                x.Description,
                x.GearType,
                x.CostInCogs,
                x.StockQuantity,
                x.IsActive,
                x.FlavorText))
            .ToListAsync(cancellationToken);
    }

    public async Task<PurchaseReceiptDto> BuyGearAsync(
        ClaimsPrincipal principal,
        int gearItemId,
        int quantity,
        CancellationToken cancellationToken)
    {
        if (quantity < 1 || quantity > 20)
        {
            throw new InvalidOperationException("You can buy between 1 and 20 gears at a time.");
        }

        var user = await currentUserService.EnsureUserAsync(principal, cancellationToken);

        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        var gear = await dbContext.GearItems
            .FirstOrDefaultAsync(x => x.GearItemId == gearItemId && x.IsActive, cancellationToken);

        if (gear is null)
        {
            throw new KeyNotFoundException("That gear is not in this workshop.");
        }

        if (gear.StockQuantity.HasValue && gear.StockQuantity.Value < quantity)
        {
            throw new InvalidOperationException("That gear shelf is running low. Not enough stock.");
        }

        var currentBalance = await currentUserService.GetCogBalanceAsync(user.UserAccountId, cancellationToken);
        var totalCost = checked(gear.CostInCogs * quantity);

        if (currentBalance < totalCost)
        {
            throw new InvalidOperationException("Not enough cogs in your pocket to spin this purchase.");
        }

        if (gear.StockQuantity.HasValue)
        {
            gear.StockQuantity -= quantity;
            gear.UpdatedAtUtc = DateTime.UtcNow;
        }

        var inventory = await dbContext.UserInventories
            .FirstOrDefaultAsync(
                x => x.UserAccountId == user.UserAccountId && x.GearItemId == gear.GearItemId,
                cancellationToken);

        if (inventory is null)
        {
            inventory = new UserInventory
            {
                UserAccountId = user.UserAccountId,
                GearItemId = gear.GearItemId,
                Quantity = quantity,
                LastGrantedAtUtc = DateTime.UtcNow,
            };

            dbContext.UserInventories.Add(inventory);
        }
        else
        {
            inventory.Quantity += quantity;
            inventory.LastGrantedAtUtc = DateTime.UtcNow;
        }

        dbContext.CogTransactions.Add(new CogTransaction
        {
            UserAccountId = user.UserAccountId,
            Amount = -totalCost,
            TransactionType = CogTransactionTypes.Purchase,
            Description = $"Bought {quantity} x {gear.Name}",
            GearItemId = gear.GearItemId,
            CreatedAtUtc = DateTime.UtcNow,
        });

        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        var newBalance = currentBalance - totalCost;

        return new PurchaseReceiptDto(
            $"{quantity} {gear.Name} added to your cog locker.",
            quantity,
            totalCost,
            newBalance,
            new InventoryItemDto(
                gear.GearItemId,
                gear.Name,
                gear.GearType,
                inventory.Quantity,
                gear.FlavorText,
                gear.CostInCogs));
    }
}
