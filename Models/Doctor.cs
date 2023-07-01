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
        public String FullName { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(Spectialist))]
        public Spectialist specialist { get; set; }

        [Required]
        [EnumDataType(typeof(MedicalDegree))]
        public MedicalDegree Degree { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Clinic_Location { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public int Clinic_PhoneNumber { get; set; }

        public ICollection<DailyAvailbility> availableDays { get; set; } = new HashSet<DailyAvailbility>();
      
        public bool HomeExamination { get; set; }

        [Required]
        public int Price { get; set; }
        
        public string? WatingTime { get; set; }

        public override string ToString()
        {
            return $"{specialist}";
       
        }

    }
}
