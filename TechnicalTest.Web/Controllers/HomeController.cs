using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TechnicalTest.BLL.Interfaces;
using TechnicalTest.Models.ViewModels.Patient;
using TechnicalTest.Models.ViewModels.Visit;
using TechnicalTest.Web.Models;

namespace TechnicalTest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientService _patientService;
        private readonly IVisitService _visitService;

        public HomeController(ILogger<HomeController> logger, IPatientService patientService, IVisitService visitService)
        {
            _logger = logger;
            _patientService = patientService;
            _visitService = visitService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllPatients(string searchTerm = null)
        {
            var patients = await _patientService.GetAllAsync(searchTerm);
            return new JsonResult(patients);
        }

        public async Task<IActionResult> GetAllVisits(string searchTerm = null)
        {
            var patients = await _visitService.GetAllAsync(searchTerm);
            return new JsonResult(patients);
        }
        public IActionResult AddPatient()
        {
            return View("SavePatient");
        }


        
        public IActionResult AddPatientVisit(int id)
        {

            return View("AddPatientVisit", new CreateVisitViewModel{PatientId = id});
        }

        public async Task<IActionResult> UpdatePatientVisit(int id)
        {
            var visit = await _visitService.GetByAsync(id);

            if (visit is null)
                ViewBag.ErrMsg = $"visit with id: {id} not found";

            var model = new UpdateVisitViewModel
                { Id = visit?.Id ?? 0, CameToSee = visit?.CameToSee, Reason = visit?.Reason};
            return View("UpdatePatientVisit", model);

        }

        public async Task<IActionResult> UpdatePatient(int id)
        {
            var patient = await _patientService.GetAsync(id);

            if (patient is null) 
                ViewBag.ErrMsg = $"patient with id: {id} not found";

            var model = new CreateOrUpdatePatientViewModel 
            {Id = patient?.Id, CardNo = patient?.CardNo, FullName = patient?.Fullname};
            return View("SavePatient", model);
        }


        [HttpPost]
        public async Task<IActionResult> SavePatient(CreateOrUpdatePatientViewModel model)
        {
            if (!ModelState.IsValid) 
                return View("SavePatient",model);
            
            var ( success, msg) = await _patientService.AddOrUpdateAsync(model);

            if (success) return RedirectToAction("Patients");
            ModelState.AddModelError("", msg);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> AddPatientVisit(CreateVisitViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            
            var (success, msg) = await _patientService.AddVisitAsync(model);

            if (success) 
                return RedirectToAction("Index");
            ModelState.AddModelError("", msg);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePatientVisit(UpdateVisitViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var (success, msg) = await _visitService.UpdateAsync(model);

            if (success)
                return RedirectToAction("Index");
            ModelState.AddModelError("", msg);
            return View(model);

        }



        [HttpGet]
        public async Task<IActionResult> SignOutPatientVisit(int id)
        {
            var (success, msg) = await _visitService.SignOutAsync(id);
            if (success)
                return RedirectToAction("Index");
            TempData["ErrMsg"] = msg;

            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Patients()
        {
            return View();
        }
    }
}
