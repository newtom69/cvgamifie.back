using Data;
using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrainerAPI.Business;

namespace TrainerAPI.Controllers
{
    /// <summary>
    /// Controlleur de l'api REST des formations des apprenants
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingCourseStudentsController : Controller
    {
        private readonly ITrainingCourseStudentBusiness _trainingCourseStudentBusiness;

        public TrainingCourseStudentsController(ITrainingCourseStudentBusiness trainingCourseStudentBusiness)
        {
            _trainingCourseStudentBusiness = trainingCourseStudentBusiness;
        }

        [HttpPost("")]
        public ActionResult Create(TrainingCourseStudent trainingCourse)
        {
            trainingCourse.Id = 0;
            var trainingCourseStudentResult = _trainingCourseStudentBusiness.Create(trainingCourse);

            if (trainingCourseStudentResult == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(Read), new { id = trainingCourseStudentResult.Id }, trainingCourseStudentResult);
        }

        [HttpGet("{id}")]
        public ActionResult<TrainingCourseStudent> Read(int id)
        {
            var tcs = _trainingCourseStudentBusiness.Read(id);

            if (tcs == null)
                return NotFound();

            return tcs;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<TrainingCourseStudent>> Read()
        {
            var tcss = _trainingCourseStudentBusiness.List();

            if (tcss == null)
                return NotFound();

            return new ObjectResult(tcss);
        }

        [HttpPut("")]
        public StatusCodeResult Update(TrainingCourseStudent trainingCourseStudent)
        {
            if (!_trainingCourseStudentBusiness.Update(trainingCourseStudent))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            if (!_trainingCourseStudentBusiness.Delete(id))
                return NotFound();

            return NoContent();
        }
    }
}
