using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using TheBackEndLayer.Helpers;
using TheBackEndLayer.Repositories;
using TheBackEndLayer.ViewModels.Members;
using TheBackEndLayer.ViewModels.Reservation;
using TheBackEndLayer.ViewModels.Reservations;

namespace TheBackEndLayer.Services
{
    public class TeeTimesService : ITeeTimesService
    {
        private readonly ITeeTimeRepository _teeTimeRepository;
        private readonly IReserveRepository _reservationRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IEmpRepository _employeeRepository;

        private readonly IAutoMapper _autoMapper;
        public TeeTimesService(ITeeTimeRepository teeTimeRepository,
            IReserveRepository reservationRepository,
            IMemberRepository memberRepository,
            IEmpRepository employeeRepository, IAutoMapper autoMapper)
        {
            _teeTimeRepository = teeTimeRepository;
            _reservationRepository = reservationRepository;
            _memberRepository = memberRepository;
            _employeeRepository = employeeRepository;
            _autoMapper = autoMapper;
        }
        public TeeTimeWithMembersViewModel GetWithMembers(int id)
        {
            var teeTime = _teeTimeRepository.GetWithMembers(id);

            var teeTimeViewModel = _autoMapper.Map<TeeTimeWithMembersViewModel>(teeTime);

            var membersReserved = teeTime.Reservations.Select(x => x.Member);

            teeTimeViewModel.MembersOnReservation = _autoMapper.Map<List<MembersViewModel>>(membersReserved);

            return teeTimeViewModel;
        }

        public List<TeeTimeViewModel> GetListBySearchDate(DateTime searchDate)
        {
            using (var db = new DbModels.BAISTGolfCourseDbContext())
            {
                var startingBusinessTime = new DateTime(DateTime.Now.Year, searchDate.Month, searchDate.Day, 9, 0, 0);
                var closingBusinessTime = new DateTime(DateTime.Now.Year, searchDate.Month, searchDate.Day, 17, 0, 0);

                var teeTimes = db.TeeTime
                    .Include(x => x.GolfCourse)
                    .Include(x => x.Reservations)
                    .Where(x => (x.StartDate > searchDate &&
                            x.EndDate < closingBusinessTime) && x.Status ==
                            Enums.TeeTimeStatus.Open && x.Reservations.Count < 4).ToList();

                var teeTimesViewModel = teeTimes.Select(x => new TeeTimeViewModel
                {
                    ID = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Status = x.Status,
                    GolfCourseName = x.GolfCourse.CourseName,
                    ReservationCount = x.Reservations.Count
                }).ToList();

                return teeTimesViewModel;
            }
        }
    }
}

