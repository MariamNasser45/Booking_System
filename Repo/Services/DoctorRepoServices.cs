using Doctor_Appointment.Data;
using Doctor_Appointment.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment.Repo.Services
{
    public class DoctorRepoServices : IDoctorRepo
    {
        public ApplicationDbContext Context { get; }

        public DoctorRepoServices(ApplicationDbContext context)
        {
            Context = context;
        }


        public List<Doctor> GetAll()
        {
            return Context.Doctors.ToList();

        }

        public Doctor GetById(int id )
        {
            return Context.Doctors.Where(d => d.DoctorID == id).Include(d=>d.availableDays).FirstOrDefault();
        }

        public void Insert(Doctor doctor )
        {
            Context.Add(doctor);
            //Context.Add(daily);
            Context.SaveChanges();
        }

        public void Update(int id, Doctor doctor)
        {
            var del_Doc = Context.Doctors.FirstOrDefault(p => p.DoctorID == id);
            var doc_WorkDay = Context.dailyAvailbilities.FirstOrDefault(d => d.DoctorID == id);

            del_Doc.FullName = doctor.FullName;
            del_Doc.Email = doctor.Email;
            del_Doc.Degree = doctor.Degree;
            del_Doc.Description = doctor.Description;
            del_Doc.Clinic_Location = doctor.Clinic_Location;
            del_Doc.Clinic_PhoneNumber = doctor.Clinic_PhoneNumber;
            del_Doc.HomeExamination = doctor.HomeExamination;
            del_Doc.Price= doctor.Price;
            del_Doc.WatingTime= doctor.WatingTime;

            Context.Doctors.Update(del_Doc);
            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            var del_Doc = Context.Doctors.FirstOrDefault(p => p.DoctorID == id);
            var doc_WorkDay = Context.dailyAvailbilities.FirstOrDefault(d => d.DoctorID == id);
            Context.dailyAvailbilities.Remove(doc_WorkDay);
            Context.Doctors.Remove(del_Doc);
            Context.SaveChanges();
        }


        //specialist filter
        public List<Doctor> GetBySpecialist(Spectialist spl)
        {
            var docSpl = Context.Doctors.Where(s => s.specialist == spl).ToList();
            return docSpl;
        }
    }
}