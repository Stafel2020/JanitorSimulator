using System.ComponentModel.DataAnnotations;

namespace JanitorSimulator.Core
{
    public class Player
    {

        [Key]
        public int Id { get; set; }

        public int Day { get; set; }
        public int Meal { get; set; }
        public int Mood { get; set; }
        public int Salary { get; set; }
        public int Age { get; set; }
        public bool Employed { get; set; }
        public bool Debtor { get; set; }

        public Player(int day, int meal, int mood, int salary, int age, bool employed, bool debtor)
        {
            Day = day;
            Meal = meal;
            Mood = mood;
            Salary = salary;
            Age = age;
            Employed = employed;
            Debtor = debtor;
        }
    }
}
