namespace CogSlop.Api.Models.Dtos;

public record MarketplacePurchaseReceiptDto(
    string Message,
    int CogsSpent,
    int NewCogBalance,
    InventoryItemDto Gear);
