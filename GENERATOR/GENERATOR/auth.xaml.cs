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
using System.Data.SqlClient;
using System.Configuration;
 

namespace GENERATOR
{
    /// <summary>
    /// Логика взаимодействия для auth.xaml
    /// </summary>
    public partial class auth : Window
    {
        public auth()
        {
            InitializeComponent();
        }
        private void But_Click(object sender, RoutedEventArgs e)
        {
            using (RYAD db = new RYAD())
            {
                USER T = db.Users.Check(Log.Text , Pass.Password.GetHashCode().ToString());
                if (T != null)
                {
                    App.newbee = new MainWindow();
                    App.CurrentUser = T;
                    App.newbee.Resources.Add("CurrentUser", App.CurrentUser.username);
                    App.newbee.Show();
                    this.Close();
                }
                else
                {
                    Log.BorderBrush = new SolidColorBrush(Colors.Red);
                    Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                    validate.Visibility = Visibility.Visible;
                }
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            register reg = new register();
            reg.Show();
            this.Close(); 
        }
    }
}
