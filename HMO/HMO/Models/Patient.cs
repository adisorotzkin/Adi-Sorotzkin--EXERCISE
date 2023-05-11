using System;
using System.Collections.Generic;

namespace HMO.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int HouseNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Phone { get; set; } = null!;

    public string MobilePhone { get; set; } = null!;

    public DateTime PositiveResultDate { get; set; }

    public DateTime RecoveryDate { get; set; }

    public byte[]? Photo { get; set; }
    public virtual ICollection<PatientVaccination> PatientVaccinations { get; set; } = new List<PatientVaccination>();
}
