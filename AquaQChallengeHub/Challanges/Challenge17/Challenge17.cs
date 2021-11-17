using AquaQChallengeHub.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge17
{
    public class Challenge17 : BaseChallenge<string>
    {
        private class Team
        {
            public string Name { get; set; }
            public DateTime MaxStartLosingDate { get; private set; } = DateTime.MinValue;
            public DateTime MaxStopLosingDate { get; private set; } = DateTime.MinValue;
            public DateTime CurrentStartLosingDate { get; set; }
            public DateTime CurrentStopLosingDate { get; set; }
            public bool IsGoallessStreakActive { get; set; } = false;

            public void UpdateGoalllessStreak()
            {
                if (CurrentStopLosingDate.Subtract(CurrentStartLosingDate) > MaxStopLosingDate.Subtract(MaxStartLosingDate))
                {
                    MaxStartLosingDate = CurrentStartLosingDate;
                    MaxStopLosingDate = CurrentStopLosingDate;
                }
            }
        }

        private class Game
        {
            public Team Hometeam { get; set; }
            public int HometeamScore { get; set; } = 0;
            public Team Awayteam { get; set; }
            public int AwayteamScore { get; set; } = 0;
            public Tournament Tournament { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public bool Neutral { get; set; }
            public DateTime Date { get; set; }
        }

        private class Tournament
        {
            public string Name { get; set; }
        }

        private List<Team> _teams = new();
        private readonly List<Game> _games = new();

        protected override string SolveChallenge()
        {
            foreach (var game in _games)
            {
                if (game.HometeamScore == 0 && !game.Hometeam.IsGoallessStreakActive)
                {
                    game.Hometeam.IsGoallessStreakActive = true;
                    game.Hometeam.CurrentStartLosingDate = game.Date;
                }
                else if (game.HometeamScore > 0 && game.Hometeam.IsGoallessStreakActive)
                {
                    game.Hometeam.IsGoallessStreakActive = false;
                    game.Hometeam.CurrentStopLosingDate = game.Date;
                    game.Hometeam.UpdateGoalllessStreak();
                }
                
                if (game.AwayteamScore == 0 && !game.Awayteam.IsGoallessStreakActive)
                {
                    game.Awayteam.IsGoallessStreakActive = true;
                    game.Awayteam.CurrentStartLosingDate = game.Date;
                }
                else if (game.AwayteamScore > 0 && game.Awayteam.IsGoallessStreakActive)
                {
                    game.Awayteam.IsGoallessStreakActive = false;
                    game.Awayteam.CurrentStopLosingDate = game.Date;
                    game.Awayteam.UpdateGoalllessStreak();
                }
            }

            Team loser = _teams.OrderByDescending(t => t.MaxStopLosingDate.Subtract(t.MaxStartLosingDate)).First();

            return $"{Answer(loser)}";
        }

        private string Answer(Team team)
        {
            return $"{team.Name} {FormatDate(team.MaxStartLosingDate)} {FormatDate(team.MaxStopLosingDate)}";
        }

        private string FormatDate(DateTime date)
        {
            return $"{date.Year}{date.Month.ToString().PadLeft(2, '0')}{date.Day.ToString().PadLeft(2, '0')}";
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            var lines = File.ReadAllLines(path).Skip(1).Select(s => s.Split(',').ToList());

            Dictionary<string, Tournament> tournaments = new();
            Dictionary<string, Team> teams = new();
            foreach (var array in lines)
            {
                var homeTeam = array[1];
                var awayTeam = array[2];
                if (!teams.TryGetValue(homeTeam, out Team home))
                {
                    home = new Team() { Name = homeTeam };
                    teams.Add(homeTeam, home);
                }
                if (!teams.TryGetValue(awayTeam, out Team away))
                {
                    away = new Team() { Name = awayTeam };
                    teams.Add(awayTeam, away);
                }

                var tourny = array[5];
                if (!tournaments.TryGetValue(tourny, out Tournament tournament))
                {
                    tournament = new Tournament() { Name = tourny };
                    tournaments.Add(tourny, tournament);
                }

                var date = DateTime.Parse(array[0]);
                var homeScore = int.Parse(array[3]);
                var awayScore = int.Parse(array[4]);
                var city = array[6];
                var country = array[7];
                var neutral = bool.Parse(array[8]);
                Game game = new()
                {
                    Hometeam = home,
                    Awayteam = away,
                    Tournament = tournament,
                    HometeamScore = homeScore,
                    AwayteamScore = awayScore,
                    City = city,
                    Country = country,
                    Neutral = neutral,
                    Date = date
                };
                _games.Add(game);
            }
            _teams = teams.Values.ToList();
        }
    }
}
