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
        private readonly DefaultContext _defaultContext;

        public TrainingCourseController(DefaultContext defaultContext)
        {
            _defaultContext = defaultContext;
        }


        public async Task<JsonResult> Index()
        {
            return new JsonResult(await _defaultContext.TrainingCourses.ToListAsync());
        }


        [HttpGet]
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null)
                return new JsonResult(null);

            var tc = await _defaultContext.TrainingCourses.FirstOrDefaultAsync(m => m.Id == id);
            return new JsonResult(tc);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(int id)
        {
            var tc = await _defaultContext.TrainingCourses.FindAsync(id);
            _defaultContext.TrainingCourses.Remove(tc);
            await _defaultContext.SaveChangesAsync();
            return await Index();
        }
    }
}
