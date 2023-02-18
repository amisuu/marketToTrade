using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ErrorController : BaseController
    {
        private readonly IErrorService _errorService;

        public ErrorController(IErrorService errorService)
        {
            _errorService = errorService;
        }

        [Authorize]
        [HttpGet("auth")]
        public string GetSecretError()
        {
            return _errorService.GetSecret();
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequestError()
        {
            return BadRequest(_errorService.GetBadRequest());
        }

        [HttpGet("not-found")]
        public async Task<ActionResult<AppUser>> GetNotFoundError()
        {
            var result = await _errorService.GetNotFound();

            if (result.Result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("server-error")]
        public async Task<ActionResult<string>> GetServerError()
        {
            return await _errorService.GetServerError();
        }
    }
}
