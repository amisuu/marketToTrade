using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
    public interface IErrorRepository
    {
        public Task<ActionResult<AppUser>> GetNotFound();
        public Task<ActionResult<string>> GetServerError();
    }
}
