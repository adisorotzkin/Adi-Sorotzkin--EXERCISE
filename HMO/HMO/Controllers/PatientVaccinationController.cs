using HMO.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMO.Controllers
{
    public class InputPV
    {
        public int PatientId { get; set; }

        public int VaccinationId { get; set; }

        public DateTime? Vdate { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class PatientVaccinationController : Controller
    {
        private readonly HmoDbContext dbContext;

        public PatientVaccinationController(HmoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllPatientVaccinations()
        {
            return Ok(dbContext.PatientVaccinations.ToList());
        }


        [HttpGet]
        [Route("{PatientId:int}/{VaccinationId:int}")]
        public async Task<IActionResult> GetPatientVaccination([FromRoute] int PatientId, [FromRoute] int VaccinationId)
        {
            var patientVaccination = await dbContext.PatientVaccinations.FindAsync(PatientId,VaccinationId);

            if (patientVaccination == null)
            {
                return NotFound();
            }
            return Ok(patientVaccination);
        }




        [HttpPost]
        public async Task<IActionResult> AddPatientVaccination([FromBody] InputPV inputPv)
        {
            if (ValidationTests.IsPatientVaccinationInputValid(inputPv,dbContext) == false)
            {
               return BadRequest("Invalid input");
            }
            var patientVaccination = new PatientVaccination
            {
                PatientId = inputPv.PatientId,
                VaccinationId = inputPv.VaccinationId,
                Vdate = inputPv.Vdate
            };
            await dbContext.PatientVaccinations.AddAsync(patientVaccination);
            await dbContext.SaveChangesAsync();
            return Ok(patientVaccination);
        }
    }
}
