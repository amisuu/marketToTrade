using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IErrorService
    {
        string GetSecret();
        ActionResult<string> GetBadRequest();
        Task<ActionResult<AppUser>> GetNotFound();
        Task<ActionResult<string>> GetServerError();
    }
}
