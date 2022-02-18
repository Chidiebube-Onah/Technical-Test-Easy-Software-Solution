using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalTest.DAL.Entities;
using TechnicalTest.Models.ViewModels;
using TechnicalTest.Models.ViewModels.Patient;
using TechnicalTest.Models.ViewModels.Visit;

namespace TechnicalTest.BLL.Interfaces
{
    public interface IVisitService
    {
        Task<(bool save, string msg)> UpdateAsync(UpdateVisitViewModel model);
        Task<(bool save, string msg)> SignOutAsync(int id);
        Task<Visit> GetByAsync(int id);
        Task<IEnumerable<VisitViewModel>> GetAllAsync(string searchTerm = null);
    }
}