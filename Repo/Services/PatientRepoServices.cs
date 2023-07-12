using Doctor_Appointment.Data;
using Doctor_Appointment.Models;

namespace Doctor_Appointment.Repo.Services
{
    public class PatientRepoServices : IPatientRepo
    {
        public ApplicationDbContext Context { get; }

        public PatientRepoServices(ApplicationDbContext context)
        {
            Context = context;
        }
        public List<Patient> GetAll()
        {
           return Context.Patients.ToList();
        }

        public Patient GetById(int id)
        {
            return Context.Patients.SingleOrDefault(p => p.PatientID == id);
        }
        public void Insert(Patient patient)
        {
            Context.Add(patient);
            Context.SaveChanges();
        }

        public void Update(int id, Patient patient)
        {
            var pat = Context.Patients.SingleOrDefault(p => p.PatientID == id);
            
            pat.EmailAddress=patient.EmailAddress;
            pat.Address=patient.Address;
            pat.Age=patient.Age;
            pat.FullName=patient.FullName;
            pat.PhoneNumber=patient.PhoneNumber;
            Context.Update(pat);
            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            var del_pat = Context.Patients.SingleOrDefault(p => p.PatientID == id);
            Context.Remove(del_pat);
            Context.SaveChanges();
        }
        public bool CheckExistance(int id)
        {
            return Context.Patients.Any(p => p.PatientID == id);
        }
    }
}
