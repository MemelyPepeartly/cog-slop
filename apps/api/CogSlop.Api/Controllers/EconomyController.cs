using CogSlop.Api.Models.Dtos;
using CogSlop.Api.Models.Requests;
using CogSlop.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CogSlop.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class EconomyController(IEconomyService economyService) : ControllerBase
{
    [HttpGet("dashboard")]
    public async Task<ActionResult<DashboardDto>> GetDashboard(CancellationToken cancellationToken)
    {
        var dashboard = await economyService.GetDashboardAsync(User, cancellationToken);
        return Ok(dashboard);
    }

    [HttpGet("store")]
    public async Task<ActionResult<IReadOnlyList<StoreItemDto>>> GetStore(CancellationToken cancellationToken)
    {
        var storeItems = await economyService.GetStoreItemsAsync(includeInactive: false, cancellationToken);
        return Ok(storeItems);
    }

    [HttpPost("store/{gearItemId:int}/buy")]
    public async Task<ActionResult<PurchaseReceiptDto>> BuyGear(
        int gearItemId,
        [FromBody] BuyGearRequest? request,
        CancellationToken cancellationToken)
    {
        try
        {
            var quantity = request?.Quantity ?? 1;
            var receipt = await economyService.BuyGearAsync(User, gearItemId, quantity, cancellationToken);
            return Ok(receipt);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
