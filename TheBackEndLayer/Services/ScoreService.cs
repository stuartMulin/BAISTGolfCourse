using TheBackEndLayer.DbModels;
using TheBackEndLayer.ViewModels.Scores;
using System;
using System.Collections.Generic;
using BAISTGOLF.InViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBackEndLayer.Services
{
    public class ScoreService
{
    private ScoreService scoreService = new ScoreService();
    private MembersService memberService = new MembersService();
}

//public List<PlayerScoreViewModel> GetScorebyID(int memberID)

//    {

//        using (var db = new BAISTGolfCourseDbContext())
//        {

//            {
//                var scores = db.Scores.Where(x => x.MemberID == memberID).ToList();
//                if
//                    (scores.Count > 0)
//                {
//                    var listOfScores = new List<PlayerScoreViewModel>();
//                    foreach (var score in scores)
//                    {
//                        var playerscoreModel = populateViewModel(scores);
//listOfScores.Add(playerscoreModel);

//                    }
//                    return listOfScores;
//                }


//                else

//                {
//                    return null;
//                }
//            }
//        }
    }

//    public bool CreatePlayerScore(CreatePlayerScore model)
//{
//    {
//        var successful = false;

//        try
//        {
//            using (var db = new BAISTGolfCourseDbContext())
//            {
//                foreach (/*member in  model.MemberID*/)
//                {
//                    var score = new Scores
//                    {
//                        DatePlayed = model.DatePlayed,
//                        HandicapId = model.HandicapID,
//                        HoleId = model.HoleID,
//                        Score = model.Score,
//                        ReservationID = model.ReservationID,
//                        MemberID = model.MemberID,
//                        CourseId = model.GolfCourseID,
//                    };


//                    db.Scores.Add(score);
//                }

//                db.SaveChanges();
//            }

//            successful = true;
//            return successful;
//        }
//        catch
//        {
//            return successful;


//        }

//    }

//}

//public CreateInputByMember GetInPutModelByAdmin()
//{
//    var createIn
//    }
//}


