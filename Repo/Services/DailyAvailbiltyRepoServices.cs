﻿using Doctor_Appointment.Data;
using Doctor_Appointment.Models;

namespace Doctor_Appointment.Repo.Services
{
    public class DailyAvailbiltyRepoServices : IDailyAvailbilityRepo
    {
        public ApplicationDbContext Context { get; }
        public DailyAvailbiltyRepoServices(ApplicationDbContext context)
        {
            Context = context;
        }

        public List<DailyAvailbility> GetAll(int docid)
        {
            return Context.dailyAvailbilities.Where(d => d.DoctorID == docid).ToList();
        }

        public DailyAvailbility GetById(int id)
        {
            return Context.dailyAvailbilities.SingleOrDefault(d => d.Dayid == id);

        }
        public void Insert(DailyAvailbility dailyAvailbility)
        {
            Context.dailyAvailbilities.Add(dailyAvailbility);
            Context.SaveChanges();
        }


        public void Update(int id, DailyAvailbility dailyAvailbility)
        {
            var upd_daily = Context.dailyAvailbilities.SingleOrDefault(d => d.Dayid == id);

            upd_daily.DoctorID = dailyAvailbility.DoctorID;
            upd_daily.Day = dailyAvailbility.Day;
            upd_daily.Date = dailyAvailbility.Date;
            upd_daily.Clinic_Time = dailyAvailbility.Clinic_Time;
            upd_daily.Isavailable = dailyAvailbility.Isavailable;

            Context.dailyAvailbilities.Update(upd_daily);
            Context.SaveChanges();
        }
        public void Delete(int id)
        {
            var del = Context.dailyAvailbilities.SingleOrDefault(d => d.Dayid == id);
            Context.dailyAvailbilities.Remove(del);
            Context.SaveChanges();

        }
        public bool CheckExistance(int id)
        {
            return Context.dailyAvailbilities.Any(d => d.Dayid == id);
        }

        public List<DailyAvailbility> GetAvailbleDay(int id)
        {
            //return Context.dailyAvailbilities.Where(d => d.DoctorID == docid).ToList();
            return (List<DailyAvailbility>)Context.dailyAvailbilities.Where(d => d.DoctorID == id && d.Isavailable == true)
                .Select(d => new { d.Day, Date = d.Date.ToShortDateString(), Clinic_Time = d.Clinic_Time });
        }

    }
}