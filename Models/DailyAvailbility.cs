using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doctor_Appointment.Models
{
    public class DailyAvailbility
    {
        [ForeignKey("Doctor")]
        [Display(Name = "Doctor Name")]
        public int DoctorID { get; set; }

        [Key]
        public int Dayid { get; set; }

        public DayOfWeek Day { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public TimeSpan Clinic_Time { get; set; }
        [Display(Name = "Is Available")]
        public bool Isavailable { get; set; }


    }
}
