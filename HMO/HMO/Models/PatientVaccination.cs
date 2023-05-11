using System;
using System.Collections.Generic;

namespace HMO.Models;

public partial class PatientVaccination
{
    public int PatientId { get; set; }

    public int VaccinationId { get; set; }

    public DateTime? Vdate { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual Vaccination Vaccination { get; set; } = null!;
}
