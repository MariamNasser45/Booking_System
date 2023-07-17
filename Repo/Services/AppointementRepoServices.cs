using Doctor_Appointment.Data;
using Doctor_Appointment.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment.Repo.Services
{
    public class AppointementRepoServices : IAppointmentRepo
    {
        public ApplicationDbContext Context { get; }

        public AppointementRepoServices(ApplicationDbContext context)
        {
            Context = context;
        }


        public List<Appointment> GetAll(int PatientID)
        {
            return Context.Appointments.Include(d => d.doctor)
                .Include(p => p.patient).Where(d=>d.PatientID== PatientID).ToList();
        }


        public Appointment GetById(int Id)
        {
            return Context.Appointments.Where(d => d.appointmentID==Id)
                .Include(d => d.doctor)
                .Include(p => p.patient)
                .Include(da=>da.availableDays).SingleOrDefault();
        }

        public void Insert(Appointment appointment)
        {
            Context.Add(appointment);
            Context.SaveChanges();
        }

        public void Update(int DocId, int PatId, Appointment appointment)
        {
            var upd_app = Context.Appointments.Where(d => d.DoctorID == DocId && d.PatientID == PatId).FirstOrDefault();
            upd_app.availableDays = appointment.availableDays;
            upd_app.AppointmentType = appointment.AppointmentType;
            upd_app.MedicalHistory = appointment.MedicalHistory;

            Context.Update(upd_app);
            Context.SaveChanges();

        }
        public void Delete(int id)
        {
            var del_app = Context.Appointments.Where(d => d.appointmentID==id).SingleOrDefault();
            Context.Appointments.Remove(del_app);
            Context.SaveChanges();
        }
    }
}
