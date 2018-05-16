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
    /// Логика взаимодействия для PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Window
    {
        public PasswordChange()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(USERContext T = new USERContext())
                {
                    if (Pass.Password != Pass1.Password) throw new Exception();
                    T.Users.Find(App.CurrentUser.username).password = Pass.Password.GetHashCode().ToString();
                    MessageBox.Show("Вы успешно сменили свой пароль!");
                    T.SaveChanges();
                    this.Close();
                }
            }
            catch
            {
                Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                Pass1.BorderBrush = new SolidColorBrush(Colors.Red);
                err.Visibility = Visibility.Visible;
            }
        }
    }
}
