using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace Memory
{
    //Trevor Sisk, CIS 229 Assignemnt 3 3/6/2016
    //}
    /// <summary>
    /// Interaction logic for IntroPage.xaml
    /// </summary>
    /// 

    //create player object to store name, score, and level
    public class Player
    {
        public int PlayerScore { get; set; }
        public string PlayerName { get; set; }
        public int Level { get; set; }


    }


    public partial class IntroPage : Page
    {
        public static string playerName = null;
        public int CurrentLevel { get; set; }


        public IntroPage()
        {
            InitializeComponent();

            var doc = XDocument.Load("highscores.xml");

            var players = doc.Root.Descendants("player").Select(x => new Player 
            { 
                PlayerName = x.Attribute("name").Value,
                PlayerScore = int.Parse(x.Attribute("score").Value),
                Level = int.Parse(x.Attribute("level").Value)
            });

            EasyS.ItemsSource = players.Where(p => p.Level == GameLevel.Easy).OrderBy(p => p.PlayerScore).Take(3);
            NormalS.ItemsSource = players.Where(p => p.Level == GameLevel.Normal).OrderBy(p => p.PlayerScore).Take(3);
            HardS.ItemsSource = players.Where(p => p.Level == GameLevel.Hard).OrderBy(p => p.PlayerScore).Take(3);
        }

        //initialize easy board, 18 cards
        private void Beginner(object sender, RoutedEventArgs e)
        {
            Player p;

            int currentLevel = GameLevel.Easy;
            CurrentLevel = currentLevel;
            p = new Player() 
            { 
                Level = currentLevel 
            };

            GamePage gPage = new GamePage(CurrentLevel);
            this.NavigationService.Navigate(gPage);
            gPage.Start(CurrentLevel);
        }
        //initialize normal board, 32 cards
        private void Normal(object sender, RoutedEventArgs e)
        {
            Player p;

            int currentLevel = GameLevel.Normal;
            CurrentLevel = currentLevel;
            p = new Player()
            {
                Level = currentLevel
            };

            GamePage gPage = new GamePage(CurrentLevel);
            this.NavigationService.Navigate(gPage);
            gPage.Start(CurrentLevel);
        }

        //initialize hard board, 72 cards
        private void Hard(object sender, RoutedEventArgs e)
        {
            Player p;

            int currentLevel = GameLevel.Hard;
            CurrentLevel = currentLevel;
            p = new Player()
            {
                Level = currentLevel
            };

            GamePage gPage = new GamePage(CurrentLevel);
            this.NavigationService.Navigate(gPage);
            gPage.Start(CurrentLevel);
        }

    }
}
