using Doctor_Appointment.Data;
using Doctor_Appointment.Models;
using Doctor_Appointment.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace Doctor_Appointment.Controllers
{
    [Authorize(Roles = "Doctor")]
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
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SpecialistFilter()
        {
            return View(doctor.GetAll());
        }
       
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SpecialistFilter(Spectialist spectialist, MedicalDegree medicalDegree)
        {
            //if(doctor.CheckSpecialistAndDegreeExistance(spectialist,medicalDegree))
            //{

                if(doctor.GetBySpecialist(spectialist, medicalDegree)!=null)
                {
                    try
                    {
                        return View(doctor.GetBySpecialist(spectialist,medicalDegree));

                    }
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                //}
            }
            else
            { 
                return View(doctor.GetAll());
            }
            return NotFound();

        }
        [AllowAnonymous]
        // GET: DoctorsController/Details/5
        public ActionResult Details(int id)
        {
            //get value of hashset for the doctor
            ViewBag.day = Context.dailyAvailbilities.Where(d=>d.DoctorID==id).Select(d => new {Day=d.Day ,  Date = d.Date , Clinic_Time=d.Clinic_Time});

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
                Details(id);
                return View("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: DoctorsController/Delete/5
        public ActionResult Delete(int id)
        {
            var doc=Context.Doctors.FirstOrDefault(d=>d.DoctorID == id);
            return View(doc);
        }

        // POST: DoctorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                doctor.Delete(id);
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return View();
            }
        }
    }
}
