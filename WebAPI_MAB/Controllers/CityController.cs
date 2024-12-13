using Microsoft.AspNetCore.Http;
using WebAPI_MAB.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI_MAB.Data;

namespace WebAPI_MAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository _cityRepository;

        public CityController(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        #region GetAll City
        [HttpGet]
        public IActionResult Index()
        {
            var cities = _cityRepository.SelectAll();
            return Ok(cities);
        }
        #endregion


        #region GetByID City
        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = _cityRepository.SelectByPK(id);

            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
        #endregion

        #region Delete City
        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var isDeleted = _cityRepository.Delete(id);

            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region InsertCity
        [HttpPost]
        public IActionResult InsertCity([FromBody] CityModel city)
        {
            if (city == null)
            {
                return BadRequest();
            }

            bool isInserted = _cityRepository.Insert(city);

            if (isInserted)
                return Ok(new { Message = "City inserted..." });
            return StatusCode(500, "An error occureed");
        }
        #endregion


        #region UpdateCity
        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, [FromBody] CityModel city)
        {
            if(city == null || id != city.CityID)
            {
                return BadRequest();
            }

            var isUpdated = _cityRepository.Update(city);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion

    }
}
