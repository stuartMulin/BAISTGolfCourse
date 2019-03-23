using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Helpers;

namespace TheBackEndLayer.Infrastructure.Maps
{
    public class ReservationsViewModel  : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<DbModels.Reservations, ViewModels.Reservation.ReservViewModels>()
               .ForMember(dest => dest.MemberFullName, opt => opt.MapFrom(src =>
               src.Member.FirstName + " " + src.Member.LastName))
               .ForMember(dest => dest.MembersOnReservation, opt => opt.Ignore())
               .ForMember(dest => dest.GolfCourse, opt => opt.MapFrom(src =>
               src.TeeTime.GolfCourse.CourseName));
        }
    
    }
}
