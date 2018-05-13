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

namespace GENERATOR
{
    /// <summary>
    /// Логика взаимодействия для PassTest.xaml
    /// </summary>
    public partial class PassTest : Window
    {
        public PassTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.TestWindow = new Test1();
            App.TestWindow.Show();
            this.Close();
        }
    }
}
