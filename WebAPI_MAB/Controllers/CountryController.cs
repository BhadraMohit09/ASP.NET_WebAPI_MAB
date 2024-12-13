using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_MAB.Data;
using WebAPI_MAB.Models;

namespace WebAPI_MAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _countryRepository;

        public CountryController(CountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        #region GetAll City
        [HttpGet]
        public IActionResult Index()
        {
            var countries = _countryRepository.SelectAll();
            return Ok(countries);
        }
        #endregion

        #region GetByID Country
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var country = _countryRepository.SelectByPK(id);

            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }
        #endregion

        #region Delete Country
        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var isDeleted = _countryRepository.Delete(id);

            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region InsertCountry
        [HttpPost]
        public IActionResult InsertCountry([FromBody] CountryModel country)
        {
            if (country == null)
            {
                return BadRequest();
            }

            bool isInserted = _countryRepository.Insert(country);

            if (isInserted)
                return Ok(new { Message = "Country inserted..." });
            return StatusCode(500, "An error occurred");
        }
        #endregion


        #region UpdateCountry
        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] CountryModel country)
        {
            if (country == null || id != country.CountryID)
            {
                return BadRequest();
            }

            var isUpdated = _countryRepository.Update(country);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        #endregion
    }
}
