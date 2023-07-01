﻿using Doctor_Appointment.Data;
using Doctor_Appointment.Models;
using Doctor_Appointment.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Doctor_Appointment.Controllers
{
    //[Authorize(Roles = "Doctor")]
    public class DoctorsController : Controller
    {
        public ApplicationDbContext Context { get; }
        public IDoctorRepo doctor { get; }
        public IDailyAvailbilityRepo AvailbilityRepo { get; }

        public DoctorsController(ApplicationDbContext context , IDoctorRepo _doctor)
        {
            Context = context;
            doctor = _doctor;
        }
        // GET: DoctorsController
        public ActionResult Index()
        {
            return View(doctor.GetAll());
        }

        //[HttpPost]
        //public ActionResult Index(Spectialist spectialist)
        //{
        //    return View(doctor.GetBySpecialist(spectialist));
        //}

        // GET: DoctorsController/Details/5
        public ActionResult Details(int id)
        {
            var check = Context.Doctors.FirstOrDefault(c=>c.DoctorID==id);
            if(check!=null)
            {
                try
                {
                    return View(doctor.GetById(id));
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
              return NotFound();
        }

       
        // GET: DoctorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Doctor doc )
        {
            try
            {
                doctor.Insert(doc);

                return RedirectToAction("Create", "DailyAvailbilities");
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorsController/Edit/5
        public ActionResult Edit(int id)
        {
            var doc=Context.Doctors.FirstOrDefault(d=>d.DoctorID==id);
            if(doc!=null)
            {
                try
                {
                return View(doc);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return View();
        }

        // POST: DoctorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Doctor doc)
        {
            try
            {
                doctor.Update(id,doc);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                doctor.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}