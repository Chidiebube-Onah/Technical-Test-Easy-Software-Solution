using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalTest.DAL.Entities;
using TechnicalTest.Models.ViewModels.Patient;
using TechnicalTest.Models.ViewModels.Visit;

namespace TechnicalTest.BLL.Interfaces
{
    public interface IPatientService
    {
        Task<(bool save, string msg)> AddOrUpdateAsync(CreateOrUpdatePatientViewModel model);
        Task<(bool save, string msg)> AddVisitAsync(CreateVisitViewModel model);
        Task<Patient> GetAsync(int id);
        Task<IEnumerable<PatientViewModel>> GetAllAsync(string searchTerm = null);

    }
}
