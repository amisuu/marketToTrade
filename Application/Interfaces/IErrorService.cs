using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface IErrorService
    {
        public string GetSecret();
        public ActionResult<string> GetBadRequest();
        public Task<ActionResult<AppUser>> GetNotFound();
        public Task<ActionResult<string>> GetServerError();
    }
}
