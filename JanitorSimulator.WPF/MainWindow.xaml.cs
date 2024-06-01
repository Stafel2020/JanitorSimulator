using JanitorSimulator.Core;
using JanitorSimulator.DB;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JanitorSimulator.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player player;
        private DispatcherTimer timer;
        public SQLiteContext context;


        // Конструктор
        public MainWindow()
        {
            context = new SQLiteContext();
            context.Database.EnsureCreated();

            if(context.Players.Any())
            {
                
                player = context.Players.OrderBy(p => p.Id).Last();
            }
            else
            {
                player = new Player(0,100,100,0,18,true,false);
                context.Players.Add(player);
                context.SaveChanges();
            }
            

            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;
            timer.Start();

            UpdateUI();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {


            if (player.Meal <= 0 || player.Mood <= 0 || player.Salary <= -900 || player.Age >= 20)
            {
                player.Employed = false;
                timer.Stop();
                context.SaveChanges();
            }
            else
            {
                player.Day++;
                player.Meal -= 10;
                player.Mood -= 10;
            }

            if (player.Salary <= -1)
            {
                player.Debtor = true;
            }
            else
            {
                player.Debtor = false;
            }

            if (player.Day % 10 == 0)
            {
                player.Age++;
            }

            if (player.Meal == 100)
            {
                timer.Start();

            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            timeText.Text = "День: " + player.Day.ToString();
            mealText.Text = "Еда: " + player.Meal.ToString();
            moodText.Text = "Настроение: " + player.Mood.ToString();
            salaryText.Text = "Деньги: " + player.Salary.ToString() + "₽";
            ageText.Text = "Возраст: " + player.Age.ToString();
            statusText.Text = player.Employed ? "Статус: Жив" : "Статус: Мёртв";
            debtsText.Text = player.Debtor ? "Долги: Есть" : "Долги: Нету";
        } 

        private void WorkButton_Click(object sender, RoutedEventArgs e)
        {
            if (player.Employed)
            {
                player.Salary += 25;

                UpdateUI();
            }
        }

        private void FoodButton_Click(object sender, RoutedEventArgs e)
        {
            if (player.Meal < 100 && player.Employed)
            {
                player.Meal += 10;
                player.Salary -= 100;

                UpdateUI();
            }
        }

        private void PartyButton_Click(object sender, RoutedEventArgs e)
        {
            if (player.Mood < 100 && player.Employed)
            {
                player.Mood += 10;
                player.Salary -= 100;

                UpdateUI();
            }
        }

        private void ChangesButton_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();

            int PlayerId;

            try
            {
               PlayerId = Convert.ToInt32(NameId.Text);
            }
            catch (Exception ex)
            {
                PlayerId = 1;
            }

            if(context.Players.Any(p => p.Id == PlayerId))
            {
                player = context.Players.First(p => p.Id == PlayerId);
            }
            else
            {
                player = new Player(0, 100, 100, 0, 18, true, false);
                context.Players.Add(player);
                context.SaveChanges();
            }
            UpdateUI();
            timer.Start();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }
    }
}