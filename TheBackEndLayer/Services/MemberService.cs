using TheBackEndLayer.DbModels;
using TheBackEndLayer.Helpers;
using TheBackEndLayer.ViewModels.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using TheBackEndLayer.Repositories;
using System.Data.Entity;
using AutoMapper;

namespace TheBackEndLayer.Services
{
    public class MemberService : IMemberService
    {

        private readonly IMemberRepository _memberRepository;
        private readonly IReserveRepository _reservationRepository;
        private readonly IPlayerScoreRepository _playerScoreRepository;
        private readonly IAutoMapper _autoMapper;


        public MemberService(IMemberRepository memberRepository, IReserveRepository reservationRepository,
            IPlayerScoreRepository playerscoreRepository, IAutoMapper autoMapper)
        {
            _memberRepository = memberRepository;
            _reservationRepository = reservationRepository;
            _playerScoreRepository = playerscoreRepository;
            _autoMapper = autoMapper;
        }
        public MembersViewModel GetMemberByEmail(string email)
        {
            var member = _memberRepository.FindBy(x => x.EmailAddress == email).
                  SingleOrDefault();
            if (member == null)
            {
                return null;
            }
            var memberViewModel = PopulateViewModel(member);
            return memberViewModel;
        }
        private MembersViewModel GetuserByEmailSearch(string EmailAddress)
        {

            var member = _memberRepository.FindBy(x => x.EmailAddress == EmailAddress).SingleOrDefault();

            if (member != null)
            {
                return PopulateViewModel(member);
            }
            else
            {
                return new MembersViewModel();
            }

        }

        public MembersViewModel PopulateViewModel(Members member)
        {
            var memberViewModel = new MembersViewModel
            {

                ID = member.ID,
                Address1 = member.Address1,
                EmailAddress = member.EmailAddress,
                DateOfBirth = member.DateOfBirth,
                FirstName = member.FirstName,
                LastName = member.LastName,
                MembershipID = member.MembershipID,
                Gender = member.Gender.ToString(),
                Password = member.Password,
                PasswordSalt = member.PasswordSalt
            };

            return memberViewModel;
        }

        public MembersViewModel GetMemberById(int id)
        {
            using (var db = new BAISTGolfCourseDbContext())
            {
                var member = db.Members.SingleOrDefault(x => x.ID == id);

                if (member != null)
                {
                    return PopulateViewModel(member);
                }
                else
                {
                    return null;
                }
            }
        }

        public MembersViewModel ValidateUser(string passwordEntered, MembersViewModel member)
        {
            var hashedPassword = PasswordEncryptor.CreatePasswordHash(passwordEntered,
                 member.PasswordSalt);

            if (hashedPassword.Equals(member.Password))
            {
                return member;
            }
            else
                return null;
        }

        public Members GetMemberByMembershipNumber(string membershipNumber)
        {
            var member = _memberRepository.FindBy(x => x.MembershipID == membershipNumber).SingleOrDefault();
            return member;
        }
        public MembersViewModel GetMemberByMembershipID(string membershipID)
        {
            var members = _memberRepository.FindBy(x => x.MembershipID == membershipID).
                SingleOrDefault();
            if (members == null)
            {
                return null;
            }
            var membersViewModel = _autoMapper.Map<MembersViewModel>(members);
            membersViewModel.AverageScore = ScoreReport(members);
            membersViewModel.ReservationStats = ReservationShortReport(members);

            return membersViewModel;
        }
        public AverageScore ScoreReport(Members members)
        {
            var averageScore = new AverageScore();

            var allMemberScores = _playerScoreRepository.FindBy(x =>
            x.MemberID == members.ID).ToList();

            if (allMemberScores.Count > 0)
            {
                //Week Score 
                var weekScore = allMemberScores.Where(x =>
                GetWeekOfYear(x.DatePlayed) == GetWeekOfYear(DateTime.Now));


                var actualWeekScore = 0.0;
                if (weekScore.Count() > 0)
                    actualWeekScore = weekScore.Select(x => x.Score).Sum() / weekScore.Count();

                //Month Score
                var monthScore = allMemberScores.Where(x =>
               x.DatePlayed.Month == DateTime.Now.Month);

                var actualMonthScore = 0.0;
                if (monthScore.Count() > 0)
                    actualMonthScore = monthScore.Select(x => x.Score).Sum() / monthScore.Count();

                //Year Score
                var yearScore = allMemberScores.Where(x =>
               x.DatePlayed.Year == DateTime.Now.Year);

                var actualYearScore = 0.0;
                if (yearScore.Count() > 0)
                    actualYearScore = yearScore.Select(x => x.Score).Sum() / yearScore.Count();

                averageScore.ScoreMonth = actualMonthScore;
                averageScore.ScoreWeek = actualWeekScore;
                averageScore.ScoreYear = actualYearScore;
            }

            return averageScore;

        }
        public ReservationStats ReservationShortReport(Members member)
        {
            var reservationStat = new ReservationStats();

            //Get All Reservation
            var allReservations = _reservationRepository.GetListReportForMember(member.ID);

            if (allReservations.Count > 0)
            {
                //Week Reservation 
                var weekReservation = allReservations.Where(x =>
                GetWeekOfYear(x.TeeTime.StartDate) == GetWeekOfYear(DateTime.Now)).Count();

                //Month Reservation
                var monthReservation = allReservations.Where(x =>
               x.TeeTime.StartDate.Month == DateTime.Now.Month).Count();

                //Year Reservation
                var yearReservation = allReservations.Where(x =>
               x.TeeTime.StartDate.Year == DateTime.Now.Year).Count();

                reservationStat.ReservationAll = allReservations.Count;
                reservationStat.ReservationMonth = monthReservation;
                reservationStat.ReservationWeek = weekReservation;
                reservationStat.ReservationYear = yearReservation;
            }

            return reservationStat;

        }
        private int GetWeekOfYear(DateTime date)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var weekNoToday = currentCulture.Calendar.GetWeekOfYear(date,
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);
            return weekNoToday;
        }
    }
}