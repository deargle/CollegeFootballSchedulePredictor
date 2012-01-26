using CollegeFootballSeasonPredictor.Model;

namespace CollegeFootballSeasonPredictor
{
    public class Predictor
    {
        public void predict(Game game)
        {
            game.Winner = game.HomeTeam;
        }
    
    }
}