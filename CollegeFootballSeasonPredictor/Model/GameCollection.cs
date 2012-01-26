using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CollegeFootballSeasonPredictor.Model
{
    public class GameCollection<Game> : ObservableCollection<Game>
    {
        private Team _scheduleTeam;
        public Team ScheduleTeam
        {
            get
            {
                return _scheduleTeam;
            }
            set
            {
                _scheduleTeam = value;
            }
        }

    }
}