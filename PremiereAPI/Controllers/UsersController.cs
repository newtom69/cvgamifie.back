using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PremiereAPI.Controllers
{
    /// <summary>
    /// Controlleur POC. Ne doit pas être pusher en prod
    /// </summary>
    public class UsersController : Controller
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// liste des users de l'application retournée en Json
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> Index()
        {
            return new JsonResult(await _context.Users.ToListAsync());
        }

        /// <summary>
        /// détails d'un user retournée en Json
        /// </summary>
        /// <param name="id">id du user</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null)
                return new JsonResult(null);

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            return new JsonResult(user);
        }

        /// <summary>
        /// supprimer un user de l'application
        /// </summary>
        /// <param name="id">id du user à supprimer</param>
        /// <returns></returns>
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
