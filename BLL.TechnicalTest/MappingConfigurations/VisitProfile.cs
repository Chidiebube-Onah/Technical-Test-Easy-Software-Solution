using AutoMapper;
using TechnicalTest.DAL.Entities;
using TechnicalTest.Models.ViewModels.Visit;

namespace TechnicalTest.BLL.MappingConfigurations
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<CreateVisitViewModel, Visit>();
            CreateMap<UpdateVisitViewModel, Visit>();
            CreateMap<Visit, VisitViewModel>()
                .ForMember(v => v.PatientFullname, opt => opt.MapFrom(src => src.Patient.Fullname))
                .ForMember(v => v.SignOut, opt => opt.MapFrom(src => src.SignOut.Value.ToString("F")))
                .ForMember(v => v.SignIn, opt => opt.MapFrom(src => src.VisitedAt.ToString("F")))
                .ForMember(v => v.PatientCardNo, opt => opt.MapFrom(src => src.Patient.CardNo));
        }
    }
}