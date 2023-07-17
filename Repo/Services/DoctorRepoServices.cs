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
            return Context.Doctors.Where(d => d.DoctorID == id).Include(d=>d.availableDays).SingleOrDefault();
        }

        public void Insert(Doctor doctor )
        {
            Context.Add(doctor);
            //Context.Add(daily);
            Context.SaveChanges();
        }

        public void Update(int id, Doctor doctor)
        {
            var del_Doc = Context.Doctors.SingleOrDefault(p => p.DoctorID == id);
            var doc_WorkDay = Context.dailyAvailbilities.SingleOrDefault(d => d.DoctorID == id);

            del_Doc.FullName = doctor.FullName;
            del_Doc.Email = doctor.Email;
            del_Doc.Degree = doctor.Degree;
            del_Doc.Description = doctor.Description;
            del_Doc.Clinic_Location = doctor.Clinic_Location;
            del_Doc.Clinic_PhoneNumber = doctor.Clinic_PhoneNumber;
            del_Doc.HomeExamination = doctor.HomeExamination;
            del_Doc.Price= doctor.Price;
            del_Doc.WatingTime= doctor.WatingTime;

            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            var del_Doc = Context.Doctors.SingleOrDefault(p => p.DoctorID == id);
            var doc_WorkDay = Context.dailyAvailbilities.SingleOrDefault(d => d.DoctorID == id);
            Context.dailyAvailbilities.Remove(doc_WorkDay);
            Context.Doctors.Remove(del_Doc);
            Context.SaveChanges();
        }


        public bool CheckSpecialistAndDegreeExistance(Spectialist spl, MedicalDegree medicalDegree)
        {
            return Context.Doctors.Any(d => d.specialist==spl && d.Degree==medicalDegree);
        }

        public bool CheckSpecialistExistance(Spectialist spl)
        {
            return Context.Doctors.Any(d => d.specialist == spl);
        }
        public bool CheckDegreeExistance(MedicalDegree medicalDegree)
        {
            return Context.Doctors.Any(d => d.Degree == medicalDegree);
        }
        //specialist filter
        public List<Doctor> GetBySpecialistAndDegree(Spectialist spl , MedicalDegree medicalDegree)
        {
            var docSpl = Context.Doctors.Where(s => s.specialist == spl && s.Degree==medicalDegree).ToList();
            return docSpl;
        }

        public List<Doctor> GetBySpecialistOnly(Spectialist spl)
        {
            return Context.Doctors.Where(d=>d.specialist==spl).ToList();
        }
        public List<Doctor> GetByDegreeOnly(MedicalDegree medicalDegree)
        {
            return Context.Doctors.Where(d => d.Degree == medicalDegree).ToList();

        }
        public bool CheckExistance(int id)
        {
            return Context.Doctors.Any(d => d.DoctorID == id);
        }

        public string CheckHomeExamination(int id)
        {
            return Context.Doctors.Where(d=>d.DoctorID==id).Select(d=>d.HomeExamination).ToString();
        }

    }
}