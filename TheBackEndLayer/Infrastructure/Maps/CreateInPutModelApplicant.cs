using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Helpers;
using TheBackEndLayer.DbModels;
using TheBackEndLayer.Enums;

namespace TheBackEndLayer.Infrastructure.Maps
{
    public class CreateInPutModelApplicant:IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<ViewModels.ForInputModels.Applicants.CreateInPutModel,Applicants>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.ShareHolder1, opt => opt.Ignore())
                .ForMember(dest => dest.ShareHolder2, opt => opt.Ignore())
                .ForMember(dest => dest.HasShareHolderOneConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.HasShareHolderTwoConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.ShareHolder1ID, opt => opt.Ignore())
                .ForMember(dest => dest.Gender, opt => opt.Ignore())
                .ForMember(dest => dest.RejectionReason, opt => opt.Ignore())
                .ForMember(dest => dest.ShareHolder2ID, opt => opt.Ignore())
                .ForMember(dest => dest.ProspectiveMembers, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Status = ApplicantStatus.Initiated;
                    dest.DateCreated = DateTime.UtcNow;
                    dest.Gender = (src.Gender) ? Gender.Male : Gender.Female;
                });

            Mapper.CreateMap<DbModels.Applicants, ViewModels.Applicants.ApplicantsViewModel>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            Mapper.CreateMap<DbModels.Applicants, ViewModels.Applicants.AppRequestViewModel>()
                .ForMember(dest => dest.FirstShareHolder, opt => opt.MapFrom(src => src.ShareHolder1))
                .ForMember(dest => dest.SecondShareHolder, opt => opt.MapFrom(src => src.ShareHolder2));
        }
    }
}
