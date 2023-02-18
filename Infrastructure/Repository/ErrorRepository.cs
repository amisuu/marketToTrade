using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Repository
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly ApplicationDbContext _context;

        public ErrorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<AppUser>> GetNotFound()
        {
            var user = await _context.Users.FindAsync(-1);

            return user;
        }

        public async Task<ActionResult<string>> GetServerError()
        {
            var thing = await _context.Users.FindAsync(-1);

            var thingToReturn = thing.ToString();

            return thingToReturn;
        }   
    }
}
