using HMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMO.Controllers
{
    public class QueriesController : Controller
    {
        private readonly HmoDbContext dbContext;

        public QueriesController(HmoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }




        [HttpGet("unvaccinated-patients")]
        public IActionResult GetUnvaccinatedCopaMembers()
        {
            var unvaccinatedCount = dbContext.Patients
                .Where(p => p.PatientVaccinations.Count == 0)
                .Count();

            return Ok(unvaccinatedCount);
        }


        [HttpGet("active-patients")]
        private async Task<ActionResult<int>> GetActivePatients(DateTime date)
        {
            var activePatients = await dbContext.Patients
                .Where(p => p.PositiveResultDate <= date && p.RecoveryDate >= date)
                .CountAsync();

            return activePatients;
        }

        [HttpGet("active-patients-last-month")]
        public async Task<ActionResult<List<int?>>> GetActivePatientsLastMonth()
        {
            var activePatientsLastMonth = new List<int?>();
            var date = DateTime.Today.AddDays(-30);

            while (date <= DateTime.Today)
            {
                var activePatients = await GetActivePatients(date);
                activePatientsLastMonth.Add(activePatients != null ? activePatients.Value : null);
                date = date.AddDays(1);
            }

            return activePatientsLastMonth;
        }
    }
}
