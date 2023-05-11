using HMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : Controller
    {
        private readonly HmoDbContext dbContext;

        public PatientController(HmoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            return Ok(await dbContext.Patients.ToListAsync());
        }

        [HttpGet]
        [Route("{PatientId:int}")]
        public async Task<IActionResult> GetPatient([FromRoute] int PatientId)
        {
            var patient = await dbContext.Patients.FindAsync(PatientId);

            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient([FromBody] Patient patient)
        {
            if (ValidationTests.IsPatientInputValid(patient,dbContext) == false)
            {
                return BadRequest("Invalid input");
            }
            await dbContext.Patients.AddAsync(patient);
            
            await dbContext.SaveChangesAsync();
            
            return Ok(patient);
        }

        [HttpPost("{PatientId}/photo")]
        public async Task<IActionResult> UploadPhoto(int PatientId, IFormFile photo)
        {
            // retrieve the member from the database
            var member = await dbContext.Patients.FindAsync(PatientId);
            if (member == null)
            {
                return NotFound();
            }

            // save the photo to the database
            using (var stream = new MemoryStream())
            {
                await photo.CopyToAsync(stream);
                member.Photo = stream.ToArray();
                await dbContext.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet("{PatientId}/photo")]
        public async Task<IActionResult> GetPhoto(int PatientId)
        {
            // retrieve the member from the database
            var member = await dbContext.Patients.FindAsync(PatientId);
            if (member == null || member.Photo == null)
            {
                return NotFound();
            }

            // return the photo data as a response
            return File(member.Photo, "image/jpeg");
        }
        
       
    }
}
