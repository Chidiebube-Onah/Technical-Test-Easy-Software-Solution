using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.BLL.Interfaces;
using TechnicalTest.DAL.Entities;
using TechnicalTest.DAL.Interfaces;
using TechnicalTest.Models.ViewModels.Visit;

namespace TechnicalTest.BLL.Implementations
{
    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Visit> _visitRepo;

        public VisitService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _visitRepo = _unitOfWork.GetRepository<Visit>();
        }
       
        public async Task<(bool save, string msg)>UpdateAsync(UpdateVisitViewModel model)
        {
            var visit = await _visitRepo.GetSingleByAsync(p => p.Id == model.Id, tracking:true);
            if (visit == null) return (false, "Visit Not Found!");
            _mapper.Map(model, visit);
            try
            {
                return (await _unitOfWork.SaveChangesAsync() > 0, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
            
        }

        public async Task<(bool save, string msg)> SignOutAsync(int id)
        {
            var visit = await _visitRepo.GetSingleByAsync(p => p.Id == id, tracking: true);
            if (visit == null) return (false, "Visit Not Found!");
            visit.SignOut= DateTime.Now;

            try
            {
                return (await _unitOfWork.SaveChangesAsync() > 0, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public async Task<Visit> GetByAsync(int id)
        {

            var visit = await _visitRepo.GetSingleByAsync(p => p.Id == id, tracking: true);
            return visit ?? null;
        }

        public async Task<IEnumerable<VisitViewModel>> GetAllAsync(string searchTerm = null)
        {
            var visits = string.IsNullOrWhiteSpace(searchTerm)
                ? await _visitRepo.GetByAsync(include: v => v.Include(x => x.Patient),
                    orderBy: v => v.OrderByDescending(x => x.VisitedAt))
                : await _visitRepo.GetByAsync(v =>
                        v.Patient.CardNo.ToLower().Contains(searchTerm.ToLower()) ||
                        v.Patient.Fullname.ToLower().Contains(searchTerm), include: v => v.Include(x => x.Patient),
                    orderBy: v => v.OrderByDescending(x => x.VisitedAt));

           return _mapper.Map<IEnumerable<VisitViewModel>>(visits);

        }
    }
}