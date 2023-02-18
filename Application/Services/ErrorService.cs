using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class ErrorService : IErrorService
    {
        private readonly IErrorRepository _errorRepository;

        public ErrorService(IErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }

        public string GetSecret()
        {
            return "secret text";
        }

        public ActionResult<string> GetBadRequest()
        {
            return "This was not a good request";
        }

        public async Task<ActionResult<AppUser>> GetNotFound()
        {
            return await _errorRepository.GetNotFound();
        }

        public async Task<ActionResult<string>> GetServerError()
        {
            return await _errorRepository.GetServerError();
        }
    }
}
