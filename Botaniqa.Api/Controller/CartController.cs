using Botaniqa.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Botaniqa.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly CartActions _actions;

        public CartController(CartActions actions) => _actions = actions;

        private int GetUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _actions.GetCart(GetUserId()));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartItemDto dto)
        {
            await _actions.AddToCart(GetUserId(), dto.ProductId, dto.Quantity);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Remove(int productId)
        {
            await _actions.RemoveFromCart(GetUserId(), productId);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Clear()
        {
            await _actions.ClearCart(GetUserId());
            return Ok();
        }
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}