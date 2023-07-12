using System.ComponentModel.DataAnnotations;

namespace Doctor_Appointment.Models
{
    public enum PGender
    {
        Female = 1,
        Male
    }
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EnumDataType(typeof(PGender))]
        [Display(Name = "Gender")]
        public PGender gender { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }
      
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public string Address { get; set; }

        //public Appointment? appointment { get; set; }


        }
    }

