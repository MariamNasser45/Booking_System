using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment.Models
{
    public enum AppointmentType
    {
        ClinicalExaminiation=1,
        HomeExamination
    }

    public class Appointment
    {
        [Key]
        public int appointmentID { get; set; }

        [ForeignKey("Doctor")]
        [Display(Name = "Doctor Name")]
        public int DoctorID { get; set; }
        public Doctor? doctor { get; set; }

        [ForeignKey("Patient")]
        [Display(Name = "Patient Name")]
        public int PatientID { get; set; }
        public Patient? patient { get; set; }
        public ICollection<DailyAvailbility> availableDays { get; set; } = new HashSet<DailyAvailbility>();

        public string DateTime { get; set; }

        [EnumDataType(typeof(AppointmentType))]
        [Display(Name = "Appointment Type")]
        public AppointmentType AppointmentType { get; set; }

        [Display(Name = "Medical History")]
        public string? MedicalHistory { get; set; }

        public override string ToString()
        {
            return doctor.FullName;
        }
    }

    }

