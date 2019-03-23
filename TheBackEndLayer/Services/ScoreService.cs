using TheBackEndLayer.DbModels;
using TheBackEndLayer.ViewModels.Scores;
using System;
using System.Collections.Generic;
using BAISTGOLF.InViewModels;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TheBackEndLayer.Repositories;
using System.Globalization;
using TheBackEndLayer.InViewModels;
using CreateInputModel = TheBackEndLayer.InViewModels.CreateInputModel;
using HoleViewModel = TheBackEndLayer.InViewModels.HoleViewModel;
using TheBackEndLayer.Enums;

namespace TheBackEndLayer.Services
{
    public class ScoreService : IScoreService
    {//create fields for repository classes that will be used here//
        private readonly IHoleRepository _holeRepository;
        private readonly IPlayerScoreRepository _playerScoreRepository;
        private readonly IReserveRepository _reservationRepository;
        private readonly IGolfCourseRepository _golfCourseRepository;
        private readonly IHandiCapRepository _handicapRepository;
        private readonly IMemberRepository _memberRepository;

        public DateTime DatePlayed { get; private set; }

        public ScoreService(IHoleRepository holeRepository,
            IPlayerScoreRepository playerScoreRepository,
            IReserveRepository reservationRepository,
            IGolfCourseRepository golfCourseRepository,
     IHandiCapRepository handicapRepository,
         IMemberRepository memberRepository)
        {
            _playerScoreRepository = playerScoreRepository;
            _holeRepository = holeRepository;
            _golfCourseRepository = golfCourseRepository;
            _handicapRepository = handicapRepository;
            _memberRepository = memberRepository;
            _reservationRepository = reservationRepository;
        }

        public CreateInputModel GetInputModel(string userIdentity)
        {
            var createInputModel = new CreateInputModel();

            var holes = _holeRepository.GetAll().ToList();
            var handicap = _handicapRepository.GetAll().ToList();

            var member = _memberRepository.FindBy(x => x.EmailAddress == userIdentity).SingleOrDefault();

            var reservations = _reservationRepository.GetNormalListForMember(member.ID).ToList();

            createInputModel.Holes = holes.Select
             (x => (new HoleViewModel { ID = x.ID, Name = x.Name })).ToList();


            createInputModel.Handicaps = handicap.Select
             (x => (new HandicapViewModel { ID = x.Id, Name = x.Name })).ToList();

            createInputModel.Reservations = reservations.Select
            (x => (new ReservationScoreViewModel
            {
                ID = x.ID,
                Date = x.TeeTime.StartDate.ToString("f")
            + " - " + x.TeeTime.EndDate.ToString("f")
            })).ToList();

            return createInputModel;
        }

        public List<ReservationScoreViewModel> GetReservationsForMember(string memberID)
        {
            var member = _memberRepository.FindBy(x => x.MembershipID == memberID).SingleOrDefault();

            var reservations = _reservationRepository.FindBy(x => x.MemberID == member.ID).ToList();

            return reservations.Select
            (x => (new ReservationScoreViewModel
            {
                ID = x.ID,
                Date = x.TeeTime.StartDate.ToString("f")
            + " - " + x.TeeTime.EndDate.ToString("f")
            })).ToList();
        }

        public CreateInputModelWithMember GetInputModelForAdmin()
        {
            var createInputModel = new CreateInputModelWithMember();

            var holes = _holeRepository.GetAll().ToList();

            var handicaps = _handicapRepository.GetAll().ToList();

            var Golfcourse = _golfCourseRepository.FindBy(x => x.CourseName.Contains(
                "BAIST")).FirstOrDefault();

            createInputModel.Holes = holes.Select
             (x => (new HoleViewModel { ID = x.ID, Name = x.Name })).ToList();

            createInputModel.Handicaps = handicaps.Select
             (x => (new HandicapViewModel { ID = x.Id, Name = x.Name })).ToList();


            return createInputModel;
        }

        public CreateInputModel CreatePlayerScore(CreateInputModel model, string userIdentity)
        {
            var member = _memberRepository.FindBy(x => x.EmailAddress == userIdentity).
                SingleOrDefault();

            var reservation = _reservationRepository.GetWithGolfCourse(model.ReservationID);

            var golfCourse = reservation.TeeTime.GolfCourse;

            var calculatedScore = model.Score - golfCourse.Rating / golfCourse.Slope * 113;
            
            
            
       
            var playerScoreModel = new PlayerScores
            {
                ReservationID = model.ReservationID,
                MemberID = member.ID,
                Score = calculatedScore,
                HoleId = model.HoleID,
                HandicapId = model.HandicapID,
                DateCreated = DateTime.UtcNow,
                DatePlayed = model.DatePlayed,
            };

            _playerScoreRepository.Add(playerScoreModel);
            _playerScoreRepository.SaveChanges();

            return model;
        }

        public CreateInputModelWithMember CreatePlayerScore(CreateInputModelWithMember model)
        {
            var member = _memberRepository.FindBy(x => x.MembershipID == model.MemberID).
                SingleOrDefault();

            var playerScoreModel = new PlayerScores
            {
                ReservationID = model.ReservationID,
                MemberID = member.ID,
                
                Score = model.Score,
                HoleId = model.HoleID,
                HandicapId = model.HandicapID,
                DateCreated = DateTime.UtcNow,
                DatePlayed = model.DatePlayed,
            };

            _playerScoreRepository.Add(playerScoreModel);
            _playerScoreRepository.SaveChanges();

            return model;
        }

        public ScoreCard GetWeeklyReport(string userIdentity)
        {
            var user = _memberRepository.FindBy(x => x.EmailAddress == userIdentity).SingleOrDefault();

            var weeklyScoreReport = new ScoreCard();

            if (user != null)
            {
                var weeklyScore = new List<double>();

                var monthlyScore = new List<double>();

                var allMemberScores = _playerScoreRepository.FindBy(x =>
                x.MemberID == user.ID).ToList();

                if (allMemberScores.Count > 0)
                {
                    //Weekly Score Report
                    var currentWeekScore = allMemberScores.Where(x =>
                    GetWeekOfYear(x.DatePlayed) == GetWeekOfYear(DateTime.Now)).ToList();

                    if (currentWeekScore.Count > 0)
                    {
                        foreach (var day in weeklyScoreReport.DaysOfWeeks)
                        {
                            var score = currentWeekScore.Where(x => x.DatePlayed.
                            DayOfWeek.ToString()
                            == day).SingleOrDefault();

                            if (score != null)
                            {
                                weeklyScore.Add(score.Score);
                            }
                            else
                            {
                                weeklyScore.Add(0);
                            }
                        }
                    }

                    //Monthly Score
                    var currentMonthScore = allMemberScores.Where(x =>
                    x.DatePlayed.Year == DateTime.Now.Year).ToList();

                    if (currentMonthScore.Count > 0)
                    {
                        foreach (var month in weeklyScoreReport.MonthsOfYears)
                        {
                            var score = currentMonthScore.Where(x => x.DatePlayed.ToString("MMMM")
                            == month).ToList();

                            if (score.Count > 0)
                            {
                                var scoreAverage = score.Select(x => x.Score).Sum() / score.Count();
                                monthlyScore.Add(scoreAverage);
                            }
                            else
                            {
                                monthlyScore.Add(0);
                            }
                        }
                    }

                    //Year Scores
                    var yearScore = allMemberScores.Where(x => x.DatePlayed.Year
                    == DateTime.Now.Year).ToList();

                    if (yearScore.Count > 0)
                    {
                        weeklyScoreReport.Years.Add(DateTime.Now.Year.ToString());
                        weeklyScoreReport.YearAverageScores.Add(yearScore.Select(x => x.Score).Sum() / yearScore.Count);
                    }

                }

            }
            return weeklyScoreReport;
        }

        private int GetWeekOfYear(DateTime date)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var weekNoToday = currentCulture.Calendar.GetWeekOfYear(date,
                            currentCulture.DateTimeFormat.CalendarWeekRule,
                            currentCulture.DateTimeFormat.FirstDayOfWeek);
            return weekNoToday;
        }

        public List<UserScoreViewModel> GetAllUserScores()
        {
            var playerScores = _playerScoreRepository.GetAllPlayerScores().Select(
                x => new UserScoreViewModel
                {
                    FName = x.Member.FirstName + " " + x.Member.LastName,
                    Gender = x.Member.Gender.ToString(),
                    Handicap = x.Handicap.Name,
                    Score = x.Score.ToString(),
                    Date = x.DatePlayed.ToString("f")
                }).ToList();

            return playerScores;
        }

        public List<UserScoreViewModel> GetAllUserScoresMale()
        {
            var playerScores = _playerScoreRepository.GetAllPlayerScores().Where(x => x.Member.Gender == Gender.Male).Select(
                x => new UserScoreViewModel
                {
                    FName = x.Member.FirstName + " " + x.Member.LastName,
                    Gender = x.Member.Gender.ToString(),
                    Handicap = x.Handicap.Name,
                    Score = x.Score.ToString(),
                    Date = x.DatePlayed.ToString("f")
                }).ToList();

            return playerScores;
        }

        public List<UserScoreViewModel> GetAllUserScoresFemale()
        {
            var playerScores = _playerScoreRepository.GetAllPlayerScores().Where(x => x.Member.Gender == Gender.Female).Select(
                x => new UserScoreViewModel
                {
                    FName = x.Member.FirstName + " " + x.Member.LastName,
                    Gender = x.Member.Gender.ToString(),
                    Handicap = x.Handicap.Name,
                    Score = x.Score.ToString(),
                    Date = x.DatePlayed.ToString("f")
                }).ToList();

            return playerScores;
        }
    }
}

