using HMO.Models;


namespace HMO.Controllers
{
    public class ValidationTests
    {
        public static bool IsPatientInputValid(Patient patient, HmoDbContext dbContext)
        {
            // Check required fields
            if (string.IsNullOrWhiteSpace(patient.FirstName) ||
                string.IsNullOrWhiteSpace(patient.LastName) ||
                string.IsNullOrWhiteSpace(patient.PatientId.ToString()) ||
                string.IsNullOrWhiteSpace(patient.City) ||
                string.IsNullOrWhiteSpace(patient.Street) ||
                string.IsNullOrWhiteSpace(patient.Phone) ||
                string.IsNullOrWhiteSpace(patient.MobilePhone))
            {
                return false;
            }

            // Check length of fields
            if (patient.FirstName.Length > 20 ||
                patient.LastName.Length > 20 ||
                patient.PatientId.ToString().Length != 9 ||
                patient.City.Length > 20 ||
                patient.Street.Length > 20 ||
                patient.Phone.Length > 20 ||
                patient.MobilePhone.Length > 20)
            {
                return false;
            }

            //Ensure that the patient's ID and vaccination ID are not negative or zero:
            if (patient.PatientId <= 0)
            {
                return false;
            }

            //Check if the date of birth is a valid date and that the patient's age is reasonable
            DateTime today = DateTime.Today;
            if (patient.DateOfBirth >= today || patient.DateOfBirth.Year < today.Year - 150)
            {
                return false;
            }

            // Check if dates are valid
            if (patient.PositiveResultDate > patient.RecoveryDate)
            {
                return false;
            }

            //Check if the patient's phone and mobile phone numbers are unique in the database:
            var existingPatient = dbContext.Patients.FirstOrDefault(p => p.Phone == patient.Phone || p.MobilePhone == patient.MobilePhone);
            if (existingPatient != null)
            {
                return false;
            }

            // Phone number format:
            if (patient.Phone.Length != 9)
            {
                return false;
            }
            if (patient.Phone[0] != '0')
            {
                return false;
            }
            if (!patient.Phone.All(char.IsDigit))
            {
                return false;
            }


            // Mobile number format:
            if (patient.MobilePhone.Length != 10)
            {
                return false;
            }
            if (patient.MobilePhone[0] != '0')
            {
                return false;
            }
            if (!patient.MobilePhone.All(char.IsDigit))
            {
                return false;
            }

            //Validate the format of the patient's address fields:
            const string VALID_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-# ";
            if (!patient.City.All(c => VALID_CHARS.Contains(c)) ||
                !patient.Street.All(c => VALID_CHARS.Contains(c)))
            {
                return false;
            }

            // Check if ID is unique
            existingPatient = dbContext.Patients.FirstOrDefault(p => p.PatientId == patient.PatientId);
            if (existingPatient != null)
            {
                return false;
            }
           
            return true;
        }

        public static bool IsVaccineInputValid(Vaccination vaccine, HmoDbContext dbContext)
        {
            // Check required fields
            if (string.IsNullOrWhiteSpace(vaccine.Vname) ||
                string.IsNullOrWhiteSpace(vaccine.Manufacturer))
            {
                return false;
            }

            // Check if ID is unique

            var existingPatient = dbContext.Vaccinations.FirstOrDefault(v => v.VaccinationId == vaccine.VaccinationId);
            if (existingPatient != null)
            {
                return false;
            }

            return true;
        }

        public static bool IsPatientVaccinationInputValid(InputPV inputPV, HmoDbContext dbContext)
        {
            // Check if patient exists
            if (!dbContext.Patients.Any(p => p.PatientId == inputPV.PatientId))
            {
                return false;
            }

            // Check if vaccine exists
            if (!dbContext.Vaccinations.Any(v => v.VaccinationId == inputPV.VaccinationId))
            {
                return false;
            }

            // Check if patient has received 4 or more vaccinations
            if (dbContext.PatientVaccinations.Count(pv => pv.PatientId == inputPV.PatientId) >= 4)
            {
                return false;
            }

            return true;
        }
    }
}
