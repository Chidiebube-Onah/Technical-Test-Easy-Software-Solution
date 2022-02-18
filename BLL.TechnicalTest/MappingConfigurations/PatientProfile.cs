using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TechnicalTest.DAL.Entities;
using TechnicalTest.Models.ViewModels.Patient;

namespace TechnicalTest.BLL.MappingConfigurations
{
   public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<CreateOrUpdatePatientViewModel, Patient>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

            CreateMap<Patient, PatientViewModel>();
        }
    }
}
