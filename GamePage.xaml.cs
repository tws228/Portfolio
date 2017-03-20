//Trevor Sisk

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
using System.Windows.Threading;
using System.Xml.Linq;

namespace Memory
{
    //Trevor Sisk, CIS 229 Assignemnt 3 3/6/2016

    class PlayerCard
    {
        public int xCoord { get; set; }
        public int yCoord { get; set; }
        public int state { get; set; }
        public Rectangle a;
    }
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
  
        private int numberOfPairs;
        private Random rnd;
        private List<PlayerCard> playerCards;
        private Button[] cardArray;
        private int turnCounter;
        private int remainingPairs;
        private int gameLevel;

        public GamePage(int currentLevel)
        {
            
            InitializeComponent();
            gameLevel = currentLevel;
            numberOfPairs = (currentLevel / 2) * (currentLevel / 2);
            rnd = new Random();
            playerCards = new List<PlayerCard>();
            cardArray = new Button[2];
            turnCounter = 0;
            turns.Text = turnCounter.ToString();
            remainingPairs = numberOfPairs;
            pairs.Text = remainingPairs.ToString();
        }

        public void Start(int currentLevel)
        {
 
            BuildGrid(currentLevel);
            SetUpCardAttributes(currentLevel);
            ShuffleDeck(playerCards);
            AddCardsToGrid();
           
        }

        //build grid (showlines)
        private void BuildGrid(int currentLevel)
        {
           
            cardGrid.Children.Clear();
            cardGrid.ColumnDefinitions.Clear();
            cardGrid.RowDefinitions.Clear();
            
            for (int i = 0; i < currentLevel / 2; i++)
            {
                cardGrid.RowDefinitions.Add(new RowDefinition());
                            }
            for (int i = 0; i < currentLevel; i++)
            {
                cardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }               
            
        }
 
        //add card attributes to list PlayerCard<> playerCards; grid location
        private void SetUpCardAttributes(int currentLevel)
        {
            playerCards.Clear();
            PlayerCard newP;
            for (int i = 0; i < currentLevel / 2; i++)
            {
                for (int j = 0; j < currentLevel; j++)
                {
                   
                    newP = new PlayerCard();
                    newP.xCoord = i;
                    newP.yCoord = j;
                    newP.a = new Rectangle();
                    newP.state = 0;
                    playerCards.Add(newP);
                }
            }
        }

        //shuffle deck
        private void ShuffleDeck(List<PlayerCard> playerCards)
        {
            int n = playerCards.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                PlayerCard value = playerCards[k];
                playerCards[k] = playerCards[n];
                playerCards[n] = value;
               
            }
        }

        //add cards to grid(board)
        private void AddCardsToGrid()
        {  
            int cardNumber = 0;
            foreach (PlayerCard p in playerCards)
            {
                if (cardNumber >= numberOfPairs)
                {
                    cardNumber = 1;
                }
                else
                    cardNumber++;

                Button b = new Button();
                b.Focusable = false;
                b.Margin = new Thickness(3);
                b.Cursor = Cursors.Hand;
                b.Tag = cardNumber.ToString();
                b.Background = new SolidColorBrush(Colors.Blue);
                b.Click += new RoutedEventHandler(CardClickCheckForMatch);
                
                p.a.Margin = new Thickness(5);
                p.a.Tag = 0;
                p.a.Fill = new ImageBrush(GetImage(String.Format("images/{00}.jpg", cardNumber)));
                
                b.Content = p.a;
                b.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                b.VerticalContentAlignment = VerticalAlignment.Stretch;
                p.a.Visibility = Visibility.Hidden;

                Grid.SetColumn(b, p.yCoord);
                Grid.SetRow(b, p.xCoord);                

                cardGrid.Children.Add(b);
            }
        }

        //add image to cards from images file. Image name format {0}; number
        private BitmapImage GetImage(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = Application.GetResourceStream(new Uri(path, UriKind.Relative)).Stream;
            image.EndInit();
            return image;
        }

        //store button event object as button with attributes, check for match
        private void CardClickCheckForMatch(object b, RoutedEventArgs e)
        {
           
                Button newB = (Button)b;
                if (newB == null) return;

                int x = Grid.GetRow(newB);
                int y = Grid.GetColumn(newB);
                
               
                foreach (PlayerCard p in playerCards)
                {
                    if (p.xCoord == x && p.yCoord == y)
                    {
 
                        p.a.Visibility = Visibility.Visible;
                        p.state = 1;
                        AddToArray(newB);
                        break;
                    }

                }
                Wait(200);
            if (cardArray[0] != null && cardArray[1] != null)
            {
                turnCounter++;
                turns.Text = turnCounter.ToString();
                
                MatchCheck();
            }

        }

        //put button(card) in array, size 2 for evaluation
        private void AddToArray(Button newB)
        {
            
                if (cardArray[0] == null)
                {
                    cardArray[0] = newB;
                }
                else
                {
                    cardArray[1] = newB;
                }
          
        }

       //evaluate if match or not. If game ends, save player data to highscore via dialogbox
        private void MatchCheck()
        {           
            if (cardArray[0].Tag.ToString() == cardArray[1].Tag.ToString())
            {
               

                statusDisplay.Text = "Match!";
                cardArray[0].IsEnabled = false;
                cardArray[1].IsEnabled = false;
                Array.Clear(cardArray, 0, cardArray.Length);
                remainingPairs--;
                pairs.Text = remainingPairs.ToString();
                foreach (PlayerCard p in playerCards)
                {
                    if(p.state == 1)
                    {
                        p.state = 2;
                        p.a.Visibility = Visibility.Visible;
            
                    }
                }

                if(remainingPairs == 0)
                {
                    string name = null; 
                    DialogBox dB = new DialogBox();
                    dB.score.Content = turnCounter.ToString();

                    if (IntroPage.playerName == null)
                    {
                        dB.ShowDialog();
                        name = dB.name.Text;
                        IntroPage.playerName = name;
                    }
                    else
                       
                        name = IntroPage.playerName;               
    
                        var doc = XDocument.Load("highscores.xml");

                        var player = doc.Root.Descendants("player").FirstOrDefault(x => x.Attribute("name").Value == "_ _ _");
        
                        doc.Root.Add(new XElement("player",
                            new XAttribute("name", name),
                            new XAttribute("score", turnCounter),
                            new XAttribute("level", gameLevel)
                        ));
                   
                        doc.Save("highscores.xml");
                }
              
                                      
            }
            else
            {
                statusDisplay.Text = "No Match!";
                Array.Clear(cardArray, 0, cardArray.Length);
                Wait(1500);
                foreach (PlayerCard p in playerCards)
                {
                    if (p.state == 1)
                    {
                        p.a.Visibility = Visibility.Hidden;
                        p.state = 0;
 
                    }
                 
                }
            }


        }

        //set up to delay methods, delegate to different thread
        private void Wait(int wait)
        {
            int w = wait;

            Application.Current.Dispatcher.Invoke(
                               System.Windows.Threading.DispatcherPriority.Background,
                               new Action(delegate { }));

            System.Threading.Thread.Sleep(w);
        }
    }    
}

