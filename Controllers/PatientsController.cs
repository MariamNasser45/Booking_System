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

namespace Doctor_Appointment.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPatientRepo patientRepo;

        public PatientsController(ApplicationDbContext context, IPatientRepo patientRepo)
        {
            _context = context;
            this.patientRepo = patientRepo;
        }

        // GET: Patients
        public IActionResult Index(int PatientId)
        {
            return  View(patientRepo.GetById(PatientId));
        }


        //all appointmr

        public IActionResult AllApp(int PatId)
        {
            return RedirectToAction("Index" , "Appointments");
        }

        // GET: Patients/Details/5
        public IActionResult Details(int id)
        {
            if (patientRepo.CheckExistance(id))
            {
                 return View(patientRepo.GetById(id));
            }

                return NotFound();
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    patientRepo.Insert(patient);
                    Index(patient.PatientID);
                    return View("Index");
                }
                catch (Exception ex)
                {

                }
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public IActionResult Edit(int id)
        {

            if (patientRepo.CheckExistance(id))
            {
                 return View(patientRepo.GetById(id));
            }
                return NotFound();
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Patient patient)
        {
            if (id != patient.PatientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                }
                catch 
                {
                    throw new Exception();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public IActionResult Delete(int id)
        {
            if (patientRepo.CheckExistance(id))
            {
                  return View(patientRepo.GetById(id));
            }
                return NotFound();

        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (patientRepo.CheckExistance(id))
            {
                patientRepo.Delete(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
