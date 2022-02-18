using Microsoft.Extensions.DependencyInjection;
using TechnicalTest.BLL.Implementations;
using TechnicalTest.BLL.Interfaces;
using TechnicalTest.DAL;
using TechnicalTest.DAL.Implementations;
using TechnicalTest.DAL.Interfaces;

namespace TechnicalTest.Web.Extensions
{
    public static class MiddlewareExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TechTestContext>>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IVisitService, VisitService>();

        }
    }
}
