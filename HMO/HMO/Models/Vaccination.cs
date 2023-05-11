using System;
using System.Collections.Generic;

namespace HMO.Models;

public partial class Vaccination
{
    public int VaccinationId { get; set; }

    public string Vname { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public virtual ICollection<PatientVaccination> PatientVaccinations { get; set; } = new List<PatientVaccination>();
}
