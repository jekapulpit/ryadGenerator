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
using System;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

namespace GENERATOR
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public USER CurrentUser;
         
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://latex.codecogs.com/gif.latex?%5Csum_%7B%5Cinfty%7D%5E%7B0%7D%5Cfrac%7B1%7D%7Bn%5E2%7D";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, AppDomain.CurrentDomain.BaseDirectory + "/test.gif");
            }
           
        }
    }

}

