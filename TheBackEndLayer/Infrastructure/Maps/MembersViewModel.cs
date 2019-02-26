using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Helpers;

namespace TheBackEndLayer.Infrastructure.Maps
{
    public class MembersViewModel : IAutoMapperTypeConfigurator
    {
        
        public void Configure()
        {
            Mapper.CreateMap<DbModels.Members, ViewModels.Members.MembersViewModel>()
                 .ForMember(dest => dest.AverageScore, opt => opt.Ignore())
                .ForMember(dest => dest.ReservationStats, opt => opt.Ignore());

            Mapper.CreateMap<DbModels.TeeTime, ViewModels.Reservations.TeeTimeWithMembersViewModel>()
                .ForMember(dest => dest.MembersOnReservation, opt => opt.Ignore());

            Mapper.CreateMap<DbModels.Applicants, DbModels.Members>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.PreviousApplication, opt => opt.Ignore())
                .ForMember(dest => dest.Scores, opt => opt.Ignore())
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.MembershipType, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicantID, opt => opt.Ignore())
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.MembershipID, opt => opt.Ignore())
                .ForMember(dest => dest.Reservation, opt => opt.Ignore())
                .ForMember(dest => dest.Applicants, opt => opt.Ignore());
        }
    }
}
