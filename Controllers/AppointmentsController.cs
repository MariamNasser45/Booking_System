using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doctor_Appointment.Data;
using Doctor_Appointment.Repo;
using Doctor_Appointment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Elfie.Extensions;

namespace Doctor_Appointment.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IAppointmentRepo Repo { get; }
        public IPatientRepo PatientRepo { get; }
        public IDoctorRepo DoctorRepo { get; }

        public AppointmentsController(ApplicationDbContext context, IAppointmentRepo repo, IPatientRepo patientRepo, IDoctorRepo doctorRepo)
        {
            _context = context;
            Repo = repo;
            PatientRepo = patientRepo;
            DoctorRepo = doctorRepo;
        }

        // GET: Appointments
        public IActionResult Index(string PatientName)
        {
                try
                {
                //return View(Repo.GetAll(PatientID));
                return View(Repo.GetAllName(PatientName));

            }
            catch
                {
                    return BadRequest("Don't have any appointments to show them");
                }
        }

        // GET: Appointments/Details/5
        public IActionResult Details(int id)
        {
            if (Repo.GetById(id)!=null)
            {
                return View(Repo.GetById(id));
            }
            return BadRequest();


        }

        [Authorize(Roles = "Patient")]
        // GET: Appointments/Create
        public IActionResult Create(int id)
        {
            ViewData["DoctorID"] = new SelectList(_context.Doctors.Where(d => d.DoctorID == id), "DoctorID", "FullName");

            //ViewData["DoctorID"] = DoctorRepo.GetById(id).FullName;

            //using ToShortDateString to ignore time only print date 
            //using Devide to convert into 12 hours format instead 24
            var Days = _context.dailyAvailbilities.Where(d => d.DoctorID == id && d.Isavailable==true)
                .Select(d => new{ d.Day , Date=  d.Date.ToShortDateString() ,Clinic_Time = d.Clinic_Time});

            ViewBag.days =  new SelectList(Days);

            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(Appointment appointment, Patient patient)
        {
            try
            {
                //if(_context.Patients.Any(p=>p.FullName==patient.FullName))
                //{
                //    appointment.PatientID=_context.Patients.Where(p=>p.FullName==patient.FullName).Select(p=>p.PatientID).SingleOrDefault();
                //    Repo.Insert(appointment);
                //    Details(appointment.appointmentID);
                //    return View("Details");
                //}
                    PatientRepo.Insert(patient);
                    appointment.PatientID=patient.PatientID;
                    Repo.Insert(appointment);
                    Details(appointment.appointmentID);
                    return View("Details");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            
            return RedirectToAction("Details");
        }
        [Authorize(Roles = "Patient")]
        // GET: Appointments/Edit/5
        public IActionResult Edit(int id, int docId)
        {
            ViewData["DoctorID"] = new SelectList(_context.Doctors.Where(D => D.DoctorID == docId), "DoctorID", "FullName");

            var Days = _context.dailyAvailbilities.Where(d => d.DoctorID == docId && d.Isavailable == true)
             .Select(d => new { d.Day, Date = d.Date.ToShortDateString(), Clinic_Time = d.Clinic_Time });

            ViewBag.days = new SelectList(Days);

            if (Repo.GetById(id) != null)
            {
                return View(Repo.GetById(id));
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Appointment appointment,string PatientName)
        {
   

            if (ModelState.IsValid)
            {
                try
                {
                    Repo.Update(appointment.DoctorID,appointment.PatientID,appointment);
                    Index(PatientName);
                    return View("Index");
                }
                catch 
                {
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
           
            return View();
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(Repo.GetById(id));
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, string PatientName)
        {
            if (Repo.GetById(id)!=null)
            {
                Repo.Delete(id);
                Index(PatientName);
                return View("Index");
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
