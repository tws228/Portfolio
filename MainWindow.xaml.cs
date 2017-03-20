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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory
{
    //Trevor Sisk, CIS 229 Assignemnt 3 3/6/2016
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public int width { get; set; }
        public int height { get; set; }


        public MainWindow()
        {
           
            InitializeComponent();
            this.height = 600;
            this.width = 1100;
            Width = width;
            Height = height;
                        
        }

       

    }
}
