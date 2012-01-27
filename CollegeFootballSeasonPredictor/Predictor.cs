using CollegeFootballSeasonPredictor.Model;

namespace CollegeFootballSeasonPredictor
{
    public class Predictor
    {
        private Team _userSelectedTeam { get; set; }

        public Predictor(Team userSelectedTeam)
        {
            this._userSelectedTeam = userSelectedTeam;
        }
        public void predict(Game game)
        {
            game.Winner = game.HomeTeam;
        }
    
    }
}