using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTest.BLL.Interfaces;
using TechnicalTest.DAL.Entities;
using TechnicalTest.DAL.Interfaces;
using TechnicalTest.Models.ViewModels.Patient;
using TechnicalTest.Models.ViewModels.Visit;

namespace TechnicalTest.BLL.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Patient> _patientRepo;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _patientRepo = _unitOfWork.GetRepository<Patient>();
        }
        public async Task<(bool save, string msg)> AddOrUpdateAsync(CreateOrUpdatePatientViewModel model)
        {
            if (model.Id is null or <= 0)
            {
                var cardNoExists = await _patientRepo.AnyAsync(c => c.CardNo.ToLower() == model.CardNo.ToLower());

                if (cardNoExists)
                    return (false, $"{model.CardNo} exists already!");
                var patient = _mapper.Map<Patient>(model);
                patient.Created = DateTime.Now;
                patient.Updated = DateTime.Now;
                _patientRepo.Add(patient);

                try
                {
                    return (await _unitOfWork.SaveChangesAsync() > 0, string.Empty);
                }
                catch (Exception e)
                {
                    return (false, e.Message);
                }
            }

            var patientToUpdate = await _patientRepo.GetByIdAsync(model.Id.Value);
            if (patientToUpdate == null) return (false, "Patient Not Found!");
            var cardNoTaken =
                await _patientRepo.AnyAsync(c => c.Id != model.Id && c.CardNo.ToLower() == model.CardNo.ToLower());

            if (cardNoTaken)
                return (false, $"{model.CardNo} is taken already!");

            _mapper.Map(model, patientToUpdate);
            patientToUpdate.Updated = DateTime.Now;

            try
            {
                return (await _unitOfWork.SaveChangesAsync() > 0, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }


        public async Task<(bool save, string msg)> AddVisitAsync(CreateVisitViewModel model)
        {
            var patient = await _patientRepo.GetSingleByAsync(p => p.Id == model.PatientId, include: p => p.Include(x => x.Visits), tracking:true);
            if (patient == null) return (false, "Patient Not Found!");
            var visit = _mapper.Map<Visit>(model);
            visit.VisitedAt = DateTime.Now;
            patient.Visits.Add(visit);
            try
            {
                return (await _unitOfWork.SaveChangesAsync() > 0, string.Empty);
            }

            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public async Task<Patient> GetAsync(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            return patient ?? null;
        }

        public async Task<IEnumerable<PatientViewModel>> GetAllAsync(string searchTerm = null)
        {
            var patients = string.IsNullOrWhiteSpace(searchTerm)
                ? await _patientRepo.GetAllAsync()
                : await _patientRepo.GetByAsync(p =>
                        p.Fullname.ToLower().Contains(searchTerm.ToLower()) || p.CardNo.ToLower().Contains(searchTerm),
                    orderBy: p => p.OrderByDescending(x => x.Created));
            return _mapper.Map<IEnumerable<PatientViewModel>>(patients);
        }
    }
}
