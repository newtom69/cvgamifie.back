using Data;
using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrainerAPI.Business;

namespace TrainerAPI.Controllers
{
    /// <summary>
    /// Controlleur de l'api REST des utilisateurs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserBusiness _userBusiness;

        public UsersController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost("")]
        public ActionResult Create(User user)
        {
            user.Id = 0;
            var userResult = _userBusiness.Create(user);

            if (userResult == null)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(Read), new { id = userResult.Id }, userResult);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Read(int id)
        {
            var tc = _userBusiness.Read(id);

            if (tc == null)
                return NotFound();

            return tc;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<User>> Read()
        {
            var tc = _userBusiness.List();

            if (tc == null)
                return NotFound();

            return new ObjectResult(tc);
        }

        [HttpPut("")]
        public StatusCodeResult Update(User user)
        {
            if (!_userBusiness.Update(user))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public StatusCodeResult Delete(int id)
        {
            if (!_userBusiness.Delete(id))
                return NotFound();

            return NoContent();
        }
    }
}
