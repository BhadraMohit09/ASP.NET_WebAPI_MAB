using Microsoft.AspNetCore.Http;
using WebAPI_MAB.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI_MAB.Data;

namespace WebAPI_MAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _stateRepository;

        public StateController(StateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        #region GetAll States
        [HttpGet]
        public IActionResult Index()
        {
            var states = _stateRepository.SelectAll();
            return Ok(states);
        }
        #endregion

        #region GetByID State
        [HttpGet("{id}")]
        public IActionResult GetStateById(int id)
        {
            var state = _stateRepository.SelectByPK(id);

            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
        #endregion

        #region Delete State
        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            var isDeleted = _stateRepository.Delete(id);

            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region InsertState
        [HttpPost]
        public IActionResult InsertState([FromBody] StateModel state)
        {
            if (state == null)
            {
                return BadRequest();
            }

            bool isInserted = _stateRepository.Insert(state);

            if (isInserted)
                return Ok(new { Message = "State inserted..." });
            return StatusCode(500, "An error occureed");
        }
        #endregion


        #region UpdateState
        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, [FromBody] StateModel state)
        {
            if (state == null || id != state.StateID)
            {
                return BadRequest();
            }

            var isUpdated = _stateRepository.Update(state);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion
    }
}