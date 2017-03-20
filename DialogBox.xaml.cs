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

namespace Memory
{
    //Trevor Sisk, CIS 229 Assignemnt 3 3/6/2016
    /// <summary>
    /// Interaction logic for DialogBox.xaml
    /// </summary>
    /// 

    //create dialogbox for player to enter name, show player score.
    public partial class DialogBox : Window
    {
  
        public DialogBox()
        {
            InitializeComponent();
          }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
