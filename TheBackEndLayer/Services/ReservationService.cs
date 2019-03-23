
using TheBackEndLayer.DbModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.ViewModels.Reservation;
using TheBackEndLayer.Services;
using TheBackEndLayer.Repositories;
using TheBackEndLayer.ViewModels.ForInputModels;
using TheBackEndLayer.ViewModels.Members;
using TheBackEndLayer.Enums;
using TheBackEndLayer.ViewModels.HandlesInPutModels;
using TheBackEndLayer.ViewModels.Reservations;
using TheBackEndLayer.Helpers;

namespace TheBackEndLayer.Services
{
    public class ReservationService: IReservationService
    {
        //create fields for repository classes that will be used here//
        private readonly ITeeTimeRepository _teeTimeRepository;
        private readonly IReserveRepository _reservationRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IEmpRepository _employeeRepository;
        private readonly IAutoMapper _IAutoMapperr;
        private readonly IAutoMapper _autoMapper;
        public ReservationService(IMemberRepository memberRepository,
            IReserveRepository reserveRepository,
            ITeeTimeRepository teeTimeRepository,
            IEmpRepository empRepository ,IAutoMapper autoMapper)
        {
            _memberRepository = memberRepository;
            _reservationRepository = reserveRepository;
            _teeTimeRepository= teeTimeRepository;
            _autoMapper = autoMapper;
            _employeeRepository = empRepository;
            _IAutoMapperr = autoMapper;

        }

        public List<ReservViewModels> GetReservations(int memberId)
        {

            using (var db = new BAISTGolfCourseDbContext())
            {
                var reservations = db.Reservations
                    .Include(x => x.Member)
                    .Include(x => x.TeeTime.GolfCourse)
                    .Where(x => x.MemberID == memberId).ToList();

                if (reservations.Count > 0)
                {
                    var listOfReservations = new List<ReservViewModels>();

                    foreach (var reservation in reservations)
                    {
                        var reservationModel = PopulateViewModel(reservation);
                        listOfReservations.Add(reservationModel);
                    }

                    return listOfReservations;
                }
                else
                {
                    return null;

                }

            }
        }

        
        private ReservViewModels PopulateViewModel(Reservations reservation)
        {
            var reservationViewModel = new ReservViewModels
            {
                ID = reservation.ID,
                DateCreated = reservation.DateCreated,
                TeeTimeID = reservation.TeeTimeID,
                GolfCourse = reservation.TeeTime.GolfCourse.CourseName,
                TeeTimeStartDate = reservation.TeeTime.StartDate,
                TeeTimeEndDate = reservation.TeeTime.EndDate,
                Status = reservation.Status
            };

            return reservationViewModel;

        }

        public void CreateNormalReservation(CreateReservationModel model, int memberID)
        {
            if (model.PotentialReservations.Count > 4)
            {
                throw new Exception("Members should not be more than 4");
            }

            var teeTimeRepo = new TeeTimeRepository(new BAISTGolfCourseDbContext());

            var teeTime = teeTimeRepo.GetWithReservationsById(model.TeeTimeID);

            foreach (var member in model.PotentialReservations)
            {
                var reservation = new Reservations
                {
                    DateCreated = DateTime.Now,
                    TeeTimeID = model.TeeTimeID,
                    MemberID = member.MemberID,
                    Status = member.Status,
                    IsApproved = false,
                    Type = Enums.ReservationType.Normal
                };

                teeTime.Reservations.Add(reservation );
            }

            var reservationCount = teeTime.Reservations.Count;

            if (reservationCount == 4)
            {
                teeTime.Status = Enums.TeeTimeStatus.Closed;
            }

            teeTimeRepo.SaveChanges();
        }

        public void CreateStandingReservation(CreateReservationModel model, int memberID)
        {
            var teeTimeRepo = new TeeTimeRepository(new BAISTGolfCourseDbContext());

            var teeTime = teeTimeRepo.GetWithReservationsById(model.TeeTimeID);

            foreach (var member in model.PotentialReservations)
            {
                var reservation = new Reservations
                {
                    DateCreated = DateTime.Now,
                    TeeTimeID = model.TeeTimeID,
                    MemberID = member.MemberID,
                    Status = member.Status,
                    IsApproved = false,
                    Type = Enums.ReservationType.Standing
                };

                teeTime.Reservations.Add(reservation);
            }

            teeTime.Status = Enums.TeeTimeStatus.Closed;

            teeTimeRepo.SaveChanges();
        }
        public MembersViewModel AddMemberToReservation(string memberID, int teeTimeID,
           string currentMemberID)
        {

            Members member = _memberRepository.FindBy(x => x.EmailAddress == memberID)
                .SingleOrDefault();

            if (member == null)
            {
                memberID = memberID.ToLower();
                member = _memberRepository.FindBy(x => x.MembershipID.ToLower() == memberID).
                SingleOrDefault();
            }


            if (member != null)
            {
                if (member.MembershipID.ToLower() == currentMemberID.ToLower())
                {
                    return null;
                }

                var reservation = _reservationRepository.FindBy(x => x.MemberID == member.ID
                && x.TeeTimeID == teeTimeID).SingleOrDefault();

                if (reservation != null)
                {
                    return null;
                }
                var memberViewModel = _autoMapper.Map<MembersViewModel>(member);

                return memberViewModel;
            }
            else
            {
                return null;
            }
        }

        public List<TeeTimeViewModel> FindTeeTimes(FindTeeTimeViewModel teeTimeFinder)
        {
            var startDate = DateTime.Parse(teeTimeFinder.StartDate + " " + teeTimeFinder.StartTime);
            var endDate = DateTime.Parse(teeTimeFinder.EndDate + " " + teeTimeFinder.EndTime);

            var teeTimes = _teeTimeRepository.GetList(startDate, endDate);

            var teeTimesViewModel = teeTimes.Select(x => new TeeTimeViewModel
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                ID = x.Id,
                ReservationCount = x.Reservations.Count
            });

            return teeTimesViewModel.ToList();
        }

        public List<MembersViewModel> AddMemberToReservationDB(string memberID, int teeTimeID)
        {
            var listOfMembersInTeeTime = new List<MembersViewModel>();

            Members member = _memberRepository.FindBy(x => x.EmailAddress == memberID).SingleOrDefault();

            if (member == null)
                member = _memberRepository.FindBy(x => x.MembershipID == memberID).
                SingleOrDefault();

            if (member != null)
            {
                var reservation = new Reservations
                {
                    DateCreated = DateTime.UtcNow,
                    MemberID = member.ID,
                    TeeTimeID = teeTimeID,
                    Status = ReservationStatus.Rejected
                };

                //TODO Send Email To Members Invited

                _reservationRepository.Add(reservation);
                _reservationRepository.SaveChanges();

                var memberViewModel = _autoMapper.Map<MembersViewModel>(member);
                listOfMembersInTeeTime.Add(memberViewModel);
                return listOfMembersInTeeTime;
            }
            else
            {
                return listOfMembersInTeeTime;
            }
        }
        public bool CreateReservation(CreateReserveInputModel inputModel, string currentMemberID)
        {
            var currentMember = _memberRepository.FindBy(x => x.EmailAddress ==
            currentMemberID).SingleOrDefault();

            var checkReservation = _reservationRepository.FindBy(x => x.MemberID ==
            currentMember.ID &&
            x.TeeTimeID == inputModel.TeetimeId).SingleOrDefault();

            if (checkReservation == null)
            {
                //Create Reservation For Current Member
                var reservationMember = new Reservations
                {
                    DateCreated = DateTime.UtcNow,
                    Status = ReservationStatus.Accepted,
                    MemberID = currentMember.ID,
                    TeeTimeID = inputModel.TeetimeId
                };
                _reservationRepository.Add(reservationMember);
                _reservationRepository.SaveChanges();


                if (inputModel.Reservations.Count > 0)
                {
                    foreach (var reservation in inputModel.Reservations)
                    {
                        var check = _reservationRepository.FindBy(x => x.MemberID ==
                        reservation.MemberId &&
                        x.TeeTimeID == inputModel.TeetimeId).SingleOrDefault();

                        if (check == null)
                        {
                            var newReservation = new Reservations
                            {
                                DateCreated = DateTime.UtcNow,
                                Status = (ReservationStatus)reservation.CurrentStatus,
                                MemberID = reservation.MemberId,
                                TeeTimeID = inputModel.TeetimeId
                            };
                            _reservationRepository.Add(newReservation);
                            _reservationRepository.SaveChanges();
                        }
                    }
                }
                return true;
            }

            return false;
        }

        public List<ReservViewModels> GetReservations(string email)
        {
            var member = _memberRepository.FindBy(x => x.EmailAddress == email).SingleOrDefault();

            var reservations = _reservationRepository.GetListForMember(member.ID);
            var reservationViewModels = _autoMapper.
                Map<List<ReservViewModels>>(reservations);

            return reservationViewModels;
        }
        public void AddCurrentUserToReservation(CreateReservationModel model, int memberID)
        {
            model.PotentialReservations.Add(new ReservationCreateModel
            { MemberID = memberID, Status = ReservationStatus.Accepted });
        }

        public List<TeeTimeViewModel> FindTeeTimes(FindTeeTimeModel teeTimeFinder)
        {
            var startDate = DateTime.Parse(teeTimeFinder.StartDate + " " + teeTimeFinder.StartTime);
            var endDate = DateTime.Parse(teeTimeFinder.EndDate + " " + teeTimeFinder.EndTime);

            var teeTimes = _teeTimeRepository.GetList(startDate, endDate);

            var teeTimesViewModel = teeTimes.Select(x => new TeeTimeViewModel
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                ID = x.Id,
                ReservationCount = x.Reservations.Count
            });

            return teeTimesViewModel.ToList();
        }

        //public bool CreateReservation(CreateReserveInputModel inputModel, string currentMemberID)
        //{
        //    var currentMember = _memberRepository.FindBy(x => x.EmailAddress ==
        //   currentMemberID).SingleOrDefault();

        //    var checkReservation = _reservationRepository.FindBy(x => x.MemberID ==
        //    currentMember.ID &&
        //    x.TeeTimeID == inputModel.TeetimeId).SingleOrDefault();

        //    if (checkReservation == null)
        //    {
        //        //Create Reservation For Current Member
        //        var reservationMember = new Reservations
        //        {
        //            DateCreated = DateTime.UtcNow,
        //            Status = ReservationStatus.Accepted,
        //            MemberID = currentMember.ID,
        //            TeeTimeID = inputModel.TeetimeId
        //        };
        //        _reservationRepository.Add(reservationMember);
        //        _reservationRepository.SaveChanges();


        //        if (inputModel.Reservations.Count > 0)
        //        {
        //            foreach (var reservation in inputModel.Reservations)
        //            {
        //                var check = _reservationRepository.FindBy(x => x.MemberID ==
        //                reservation.MemberId &&
        //                x.TeeTimeID == inputModel.TeetimeId).SingleOrDefault();

        //                if (check == null)
        //                {
        //                    var newReservation = new Reservations
        //                    {
        //                        DateCreated = DateTime.UtcNow,
        //                        Status = (ReservationStatus)reservation.CurrentStatus,
        //                        MemberID = reservation.MemberId,
        //                        TeeTimeID = inputModel.TeetimeId
        //                    };
        //                    _reservationRepository.Add(newReservation);
        //                    _reservationRepository.SaveChanges();
        //                }
        //            }
        //        }
        //        return true;
        //    }

        //    return false;
        //}

        //public bool CreateReservation(ReservationCreateModel inputModel, string currentMemberID)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<TeeTimeViewModel> FindTeeTimes(FindTeeTimeModel teeTimeFinder)
        //{
        //    throw new NotImplementedException();
        //}

        //public MembersViewModel AddMemberToReservation(string memberID, int teeTimeID, string currentMemberID)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<ReservViewModels> GetReservations(string email)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
