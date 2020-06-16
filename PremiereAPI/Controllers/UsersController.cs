using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PremiereAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        public async Task<JsonResult> Index()
        {
            return new JsonResult(await _context.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null)
                return new JsonResult(null);

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            return new JsonResult(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return await Index();
        }
    }
}
