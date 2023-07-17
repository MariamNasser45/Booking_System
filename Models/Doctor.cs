using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doctor_Appointment.Models
{
    public enum Gender
    {
        Female=1,
        Male
    }

    public enum Spectialist
    {
        Neurology=1,
        Dentists,
        Ophthalmology,
        Orthopedics,
        Cancer_Department,
        Internal_medicine,
        ENT

    }

    public enum MedicalDegree
    {
        specialist = 1,

        Advisor,

        professor
    }

    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Full Name")]
        public String FullName { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        [Display(Name = "Gender")]
        public Gender gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(Spectialist))]
        [Display(Name = "Spectialist")]
        public Spectialist specialist { get; set; }

        [Required]
        [EnumDataType(typeof(MedicalDegree))]
        [Display(Name = "Medical Degree")]
        public MedicalDegree Degree { get; set; }

        public string? Description { get; set; }

        [Required]
        [Display(Name = "Clinic Location")]
        public string Clinic_Location { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Clinic Phone Number")]
        public int Clinic_PhoneNumber { get; set; }
       
        [Display(Name = "Available Days")]
        public ICollection<DailyAvailbility> availableDays { get; set; } = new HashSet<DailyAvailbility>();

        [Display(Name = "Home Examination")]
        public bool HomeExamination { get; set; }

        [Required]
        public int Price { get; set; }

        [Display(Name = "Wating Time")]
        public string? WatingTime { get; set; }
        public override string ToString()
        {
            return $"{specialist}";
       
        }

    }
}
