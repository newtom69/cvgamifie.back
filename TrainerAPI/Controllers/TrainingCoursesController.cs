using Data;
using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        private readonly ITrainingCourseBusiness _trainingCourseBusiness;

        public TrainingCoursesController(ITrainingCourseBusiness trainingCourseBusiness)
        {
            _trainingCourseBusiness = trainingCourseBusiness;
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

        [HttpGet("")]
        public ActionResult<IEnumerable<TrainingCourse>> Read()
        {
            var tc = _trainingCourseBusiness.List();

            if (tc == null)
                return NotFound();

            return new ObjectResult(tc);
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
