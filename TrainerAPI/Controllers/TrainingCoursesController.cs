using Data;
using Microsoft.AspNetCore.Mvc;
using Data.Model;
using Microsoft.AspNetCore.Http;
using TrainerAPI.Business;

namespace TrainerAPI.Controllers
{
    /// <summary>
    /// Controlleur de l'api REST des formations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingCoursesController : Controller
    {
        private readonly TrainingCourseBusiness _trainingCourseBusiness;

        public TrainingCoursesController(DefaultContext defaultContext)
        {
            _trainingCourseBusiness = new TrainingCourseBusiness(defaultContext);
        }

        [HttpPost("")]
        public ActionResult Create(TrainingCourse trainingCourse)
        {
            trainingCourse.Id = 0;
            var trainingCourseResult = _trainingCourseBusiness.Create(trainingCourse);

            if (trainingCourseResult == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(Read), new { id = trainingCourseResult.Id }, trainingCourseResult);
        }

        [HttpGet("{id}")]
        public ActionResult<TrainingCourse> Read(int id)
        {
            var tc = _trainingCourseBusiness.Read(id);

            if (tc == null)
                return NotFound();

            return tc;
        }

        [HttpPut("")]
        public StatusCodeResult Update(TrainingCourse trainingCourse)
        {
            if (!_trainingCourseBusiness.Update(trainingCourse))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            if (!_trainingCourseBusiness.Delete(id))
                return NotFound();

            return NoContent();
        }
    }
 }
