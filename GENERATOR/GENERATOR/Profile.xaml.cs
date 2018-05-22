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
                IEnumerable<Generator> Ryads = from p in y.Ryads
                                          where p.USERusername == App.CurrentUser.username
                                          select p;
                nryads.Content = Ryads.Count().ToString();
                int avgr = 0;
                IEnumerable<Test> Tests = from p in y.Tests
                                          where p.USERusername == App.CurrentUser.username
                                          select p;
                foreach (Test r in Tests)
                {
                    avgr += r.Mark;
                }
                if(Tests.Count() != 0)
                avg.Content = (avgr / Tests.Count()).ToString();
                else
                avg.Content = "---";
                tests.Content = Tests.Count().ToString();
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
