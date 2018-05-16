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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Profile()
        {
            InitializeComponent();
            using (USERContext y = new USERContext())
            {
                log.Content = App.CurrentUser.username;
                lvl.Content = App.CurrentUser.lvl == 1 ? "Продвинутый" : "Новичок";
                nryads.Content = y.Ryads.Count();
                int avgr = 0;
                foreach(Test r in y.Tests)
                {
                    avgr += r.Mark;
                }
                if(y.Tests.Count() != 0)
                avg.Content = (avgr / y.Tests.Count()).ToString();
                else
                avg.Content = "---";
                tests.Content = y.Tests.Count().ToString();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PasswordChange M = new PasswordChange();
            M.Show();
            this.Close();
        }
    }
}
