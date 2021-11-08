using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaQChallengeHub.Challanges.Challenge08
{
    public class Challenge08 : ChallengeBase<int>
    {
        private class Groceries
        {
            public DateTime Date { get; set; }
            public int Milk { get; set; } //Mls
            public int Cereal { get; set; } //Grams
            public int ExpirationTime { get; set; } = 5;
            public bool IsExpired => ExpirationTime == 0;
            public bool HasMilk => Milk > 0;
            public bool HasCereal => Cereal > 0;

            public override string ToString()
            {
                return $"Date: {Date.ToShortDateString()}, Milk: {Milk}, Cereal: {Cereal}, Expiration Time: {(HasMilk ? ExpirationTime : 0)}";
            }
        }

        private List<Groceries> _groceries = new();

        protected override int SolveChallenge()  //Unsolved
        {
            int cereal = _groceries.First().Cereal;
            DateTime milkDate = _groceries.First().Date;
            int n = _groceries.Count;
            for (int i = 1; i < n; i++)
            {
                Groceries current = _groceries[i];

                if (current.HasCereal)
                    cereal += current.Cereal;

                Groceries temp = _groceries.FirstOrDefault(v => v.Date == milkDate);
                if (temp != null && temp.HasMilk && cereal > 0)
                {
                    temp.Milk -= 100;
                    cereal -= 100;
                }

                int daysApart = current.Date.Subtract(milkDate).Days;
                if (daysApart == 5)
                {
                    temp.Milk = 0;
                    milkDate = DateTime.MinValue;
                }

                if (milkDate == DateTime.MinValue && current.HasMilk)
                    milkDate = current.Date;
            }

            if (milkDate != DateTime.MinValue && _groceries.Last().Date.Subtract(milkDate).Days == 5)
            {
                _groceries.First(v => v.Date == milkDate).Milk = 0;
            }

            return _groceries.Sum(v => v.Milk) + cereal;
        }

        protected override void ReadData()
        {
            bool useInput = true;
            string path = $@"C:\Users\Andreas\Desktop\AquaQChallengeHub\Challange input\{GetType().Name}\{(useInput ? "input" : "sample")}.txt";
            var lines = File.ReadAllLines(path).Skip(1);
            foreach (var s in lines)
            {
                var split = s.Split(',').ToList();
                Groceries item = new() { Date = useInput ? DateTime.Parse(split[0]) : DateTime.Now.AddDays(int.Parse(split[0]) - 1), Milk = int.Parse(split[1]), Cereal = int.Parse(split[2]) };
                _groceries.Add(item);
            }
            _groceries = _groceries.OrderBy(i => i.Date).ToList();
        }
    }
}
