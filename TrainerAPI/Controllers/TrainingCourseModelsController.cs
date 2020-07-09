using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrainerAPI.Business;
using TrainerAPI.Business.Model;

namespace TrainerAPI.Controllers
{
    /// <summary>
    /// Controlleur de l'api REST des formations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingCourseModelsController : Controller
    {
        private readonly ITrainingCourseModelBusiness _trainingCourseModelBusiness;

        public TrainingCourseModelsController(ITrainingCourseModelBusiness trainingCourseModelBusiness)
        {
            _trainingCourseModelBusiness = trainingCourseModelBusiness;
        }

        [HttpPost("")]
        public ActionResult Create(TrainingCourseModel trainingCourseModel)
        {
            trainingCourseModel.Id = 0;
            var trainingCourseModelResult = _trainingCourseModelBusiness.Create(trainingCourseModel);

            if (trainingCourseModelResult == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(Read), new { id = trainingCourseModelResult.Id }, trainingCourseModelResult);
        }

        [HttpGet("{id}")]
        public ActionResult<TrainingCourseModel> Read(int id)
        {
            var tc = _trainingCourseModelBusiness.Read(id);

            if (tc == null)
                return NotFound();

            return tc;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<TrainingCourseModel>> Read()
        {
            var tc = _trainingCourseModelBusiness.List();

            if (tc == null)
                return NotFound();

            return new ObjectResult(tc);
        }

        [HttpPut("")]
        public StatusCodeResult Update(TrainingCourseModel trainingCourseModel)
        {
            if (!_trainingCourseModelBusiness.Update(trainingCourseModel))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            if (!_trainingCourseModelBusiness.Delete(id))
                return NotFound();

            return NoContent();
        }
    }
}
