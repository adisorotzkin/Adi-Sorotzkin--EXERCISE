using HMO.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinationsController : Controller
    {
        private readonly HmoDbContext dbContext;

        public VaccinationsController(HmoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllVaccinations()
        {
            return Ok(dbContext.Vaccinations.ToList());
        }


        [HttpGet]
        [Route("{VaccinationId:int}")]
        public async Task<IActionResult> GetVaccination([FromRoute] int VaccinationId)
        {
            var vaccination = await dbContext.Vaccinations.FindAsync(VaccinationId);

            if (vaccination == null)
            {
                return NotFound();
            }
            return Ok(vaccination);
        }

        [HttpPost]
        public async Task<IActionResult> AddVaccination(Vaccination vaccination)
        {
            if (ValidationTests.IsVaccineInputValid(vaccination, dbContext) == false)
            {
                return BadRequest("Invalid input");
            }
            await dbContext.Vaccinations.AddAsync(vaccination);
            await dbContext.SaveChangesAsync();
            return Ok(vaccination);
        }
    }
}
