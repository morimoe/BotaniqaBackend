using Botaniqa.BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Botaniqa.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly CartActions _actions;

        public FavoritesController(CartActions actions) => _actions = actions;

        private int GetUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _actions.GetFavorites(GetUserId()));

        [HttpPost("{productId}")]
        public async Task<IActionResult> Toggle(int productId)
        {
            await _actions.ToggleFavorite(GetUserId(), productId);
            return Ok();
        }
    }
}