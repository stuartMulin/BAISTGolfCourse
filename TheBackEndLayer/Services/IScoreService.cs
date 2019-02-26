using BAISTGOLF.InViewModels;
using System.Collections.Generic;
using TheBackEndLayer.InViewModels;
using TheBackEndLayer.ViewModels.Scores;

namespace TheBackEndLayer.Services
{
    public interface IScoreService
    {
        CreateInputModel CreatePlayerScore(CreateInputModel model, string name);
        CreateInputModel GetInputModel(string userIdentity);
        CreateInputModelWithMember GetInputModelForAdmin();
        //CreateInputModel CreatePlayerScore(CreateInputModel model, string userIdentity);
        CreateInputModelWithMember CreatePlayerScore(CreateInputModelWithMember model);
        List<ReservationScoreViewModel> GetReservationsForMember(string memberID);
        ScoreCard GetWeeklyReport(string userIdentity);
        List<UserScoreViewModel> GetAllUserScores();
        List<UserScoreViewModel> GetAllUserScoresMale();
        //List<UserScoreViewModel> GetAllUserScoresFemale();

    }
}