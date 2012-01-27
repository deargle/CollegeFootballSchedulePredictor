using CollegeFootballSeasonPredictor.Model;
using System;


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
          
            Random rand = new Random();
            double random = rand.Next();
            
            if (random <= game.Confidence)
            {
                game.Winner = _userSelectedTeam;
            }
            else{
                if (_userSelectedTeam == game.HomeTeam)
                {
                    game.Winner = game.AwayTeam;
                }
                else{
                    game.Winner = game.HomeTeam;
                }
        }

            
            
        }
    
    }
}