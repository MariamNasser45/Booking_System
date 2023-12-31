﻿using Doctor_Appointment.Models;

namespace Doctor_Appointment.Repo
{
    public interface IAppointmentRepo
    {
        //public List<Appointment> GetAll(int PatId);

        public List<Appointment> GetAll(int AppId);
        public List<Appointment> GetAllName(string PatientName);
        public Appointment GetById(int Id);
        public void Insert(Appointment appointment);
        public void Update(int DocId, int PatId, Appointment appointment);
        public void Delete(int id);
    }
}
