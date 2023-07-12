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

        public AppointmentsController(ApplicationDbContext context, IAppointmentRepo repo)
        {
            _context = context;
            Repo = repo;
        }

        // GET: Appointments
        public IActionResult Index(int PatientID)
        {
            if (Repo.GetAll(PatientID).Count != 0)
            {
                return View(Repo.GetAll(PatientID));
            }
            else
                return BadRequest("Don't have any appointments to show them");
        }

        // GET: Appointments/Details/5
        public IActionResult Details(int id)
        {
            //var appointment =  _context.Appointments
            //    .Include(a => a.doctor)
            //    .Include(a => a.patient)
            //    .Where(m => m.appointmentID==id);
            if (Repo.GetById(id)!=null)
            {
                return View(Repo.GetById(id));
            }
            return BadRequest();


        }

        [Authorize(Roles = "Patient")]
        // GET: Appointments/Create
        public IActionResult Create(int id , int Patid)
        {
            ViewData["DoctorID"] = new SelectList(_context.Doctors.Where(D=>D.DoctorID==id), "DoctorID", "FullName");

            ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName");

            //using ToShortDateString to ignore time only print date 
            //using Devide to convert into 12 hours format instead 24
            var Days = _context.dailyAvailbilities.Where(d => d.DoctorID == id && d.Isavailable==true)
                .Select(d => new{ d.Day , Date=  d.Date.ToShortDateString() ,Clinic_Time = d.Clinic_Time});

            ViewBag.days =  new SelectList(Days);

            //    var AppType=_context.Appointments.Where(d => d.DoctorID == id).Select(d => d.AppointmentType);
            //ViewBag.type = new SelectList(AppType);


            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("DoctorID,PatientID,DateTime,AppointmentType,MedicalHistory")] Appointment appointment)
        {
            if(ModelState.IsValid)
            {

            try
            {
                    Repo.Insert(appointment);
                    Details(appointment.appointmentID);
                    return View("Details");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            }
            //ViewData["DoctorID"] = new SelectList(_context.Doctors, "DoctorID", "FullName", appointment.DoctorID);
            //ViewData["PatientID"] = new SelectList(_context.Patients, "PatientID", "FullName", appointment.PatientID);

            return RedirectToAction("Details");
        }
        [Authorize(Roles = "Patient")]
        // GET: Appointments/Edit/5
        public IActionResult Edit(int id, int docId)
        {
            
            if (_context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = _context.Appointments.SingleOrDefault(a => a.appointmentID==id);

            if (appointment == null)
            {
                return NotFound();
            }
            var Days = _context.dailyAvailbilities.Where(d => d.DoctorID == docId && d.Isavailable == true)
             .Select(d => new { d.Day, Date = d.Date.ToShortDateString(), Clinic_Time = d.Clinic_Time });


            ViewBag.days = new SelectList(Days);

            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("DoctorID,PatientID,DateTime,AppointmentType,MedicalHistory")] Appointment appointment)
        {
   

            if (ModelState.IsValid)
            {
                try
                {
                    Repo.Update(appointment.DoctorID,appointment.PatientID,appointment);
                    Index(appointment.PatientID);
                    return View("Index");
                }
                catch (DbUpdateConcurrencyException)
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

            var appointment = await _context.Appointments
                .Include(a => a.doctor)
                .Include(a => a.patient)
                .FirstOrDefaultAsync(a => a.appointmentID==id);

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, int PatientId)
        {
            //if (_context.Appointments == null)
            //{
            //    return Problem("Entity set 'MedcareDbContext.Appointments'  is null.");
            //}
            var appointment = _context.Appointments.FirstOrDefault(m=>m.appointmentID == id);
            if (appointment != null)
            {
                Repo.Delete(id);
                Index(PatientId);
                return View("Index");

            }
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.DoctorID == id)).GetValueOrDefault();
        }
    }
}
