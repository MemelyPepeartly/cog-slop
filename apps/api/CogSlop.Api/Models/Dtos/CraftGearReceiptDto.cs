namespace CogSlop.Api.Models.Dtos;

public record CraftGearReceiptDto(
    string Message,
    int CogsSpent,
    int NewCogBalance,
    InventoryItemDto Gear);
