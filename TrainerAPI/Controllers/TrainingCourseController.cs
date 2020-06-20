using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TrainerAPI.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class TrainingCourseController : Controller
    {
        private readonly Context _context;

        public TrainingCourseController(Context context)
        {
            _context = context;
        }


        public async Task<JsonResult> Index()
        {
            return new JsonResult(await _context.TrainingCourses.ToListAsync());
        }


        [HttpGet]
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null)
                return new JsonResult(null);

            var tc = await _context.TrainingCourses.FirstOrDefaultAsync(m => m.Id == id);
            return new JsonResult(tc);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            var tc = await _context.TrainingCourses.FindAsync(id);
            _context.TrainingCourses.Remove(tc);
            await _context.SaveChangesAsync();
            return await Index();
        }
    }
}
