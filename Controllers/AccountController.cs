using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudTeacher.Data;
using StudTeacher.DTO;
using StudTeacher.Models;
using Microsoft.EntityFrameworkCore;

namespace StudTeacher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Accounts
                .Select(a => new { a.Id, a.Name, a.Dob, a.Designation, a.Email })
                .ToListAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return Ok(account);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Account updated)
        {
            var existing = await _context.Accounts.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = updated.Name;
            existing.Dob = updated.Dob;
            existing.Designation = updated.Designation;
            existing.Email = updated.Email;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var acc = await _context.Accounts.FindAsync(id);
            if (acc == null) return NotFound();

            _context.Accounts.Remove(acc);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}