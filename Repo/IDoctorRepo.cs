using Doctor_Appointment.Models;

namespace Doctor_Appointment.Repo
{
    public interface IDoctorRepo
    {
        public List<Doctor> GetAll();
        public Doctor GetById(int id);
        public bool CheckExistance(int id);
        public List<Doctor> GetBySpecialistAndDegree(Spectialist spl, MedicalDegree medicalDegree);
        public List<Doctor> GetBySpecialistOnly(Spectialist spl);
        public List<Doctor> GetByDegreeOnly(MedicalDegree medicalDegree);
        public bool CheckSpecialistAndDegreeExistance(Spectialist spl, MedicalDegree medicalDegree);
        public bool CheckSpecialistExistance(Spectialist spl);
        public bool CheckDegreeExistance( MedicalDegree medicalDegree);
        public string CheckHomeExamination(int id);
        public void Insert(Doctor doctor);
        public void Update(int id, Doctor doctor);
        public void Delete(int id);
    }
}
