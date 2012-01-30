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
            double random = rand.NextDouble();
            
            // If the SelectedTeam is the HomeTeam, we need to reverse the confidence, because if they
            // move the slider towards the home team, then that signifies that they think that
            // that team will win, whereas the confidence, unreversed, would indicate the opposite.
            double _convertedConfidence;

            _convertedConfidence = _userSelectedTeam.Equals(game.HomeTeam) ? 1 - game.Confidence : game.Confidence;
            
            if (random <= _convertedConfidence)
            {
                game.Winner = _userSelectedTeam;
            }
            else
            {
                game.Winner = ( _userSelectedTeam.Equals(game.HomeTeam) ? game.AwayTeam : game.HomeTeam );
            }

            Team winner = game.Winner;
        }
    
    }
}