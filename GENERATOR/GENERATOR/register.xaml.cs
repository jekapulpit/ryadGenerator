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
       /// </summary>
    public partial class register : Window
    {

        public register()
        {
            InitializeComponent();
            Log.BorderBrush = null;
            Pass.BorderBrush = null;
            Conf.BorderBrush = null;
            Confirm.Opacity = 0;
            IsAlter_Checked(IsAlter, null);

        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            auth auth = new auth();
            auth.Show();
            this.Close();
        }


        private bool validate(object sender, TextCompositionEventArgs e)
        {
            if (Conf.Password != Pass.Password)
            {
                Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                Conf.BorderBrush = new SolidColorBrush(Colors.Red);
                Confirm.Opacity = 1;
                return false;
            }
            else if (Pass.Password.Length < 6)
            {
                Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                Log.BorderBrush = null;
                Pass.BorderBrush = null;
                Conf.BorderBrush = null;
                Confirm.Opacity = 0;
                return true;
            }
        }
        private void ButReg_Click(object sender, RoutedEventArgs e)
        {
            using (USERContext Users = new USERContext())
            {
                if (validate(this, null))
                {
                    IEnumerable<USER> t = from p in Users.Users
                            where p.username == Log.Text
                            select p;

                    if (t.Count() == 0)
                    {
                        USER T = new USER(Log.Text, (Pass.Password).GetHashCode().ToString(), (bool)IsAlter.IsChecked ? 1 : 0);
                        Users.Users.Add(T);
                        Users.SaveChanges();
                        auth auth = new auth();
                        auth.Show();
                        this.Close();
                    }
                    else
                    {
                        LoginCheck.Opacity = 1;
                        Log.BorderBrush = new SolidColorBrush(Colors.Red);
                    }
                }
            }
        }

        private void IsAlter_Checked(object sender, RoutedEventArgs e)
        {
            if((bool)((CheckBox)sender).IsChecked == false)
            {
                Help.Text = "У вас практически отсутствуют знания по теме 'Ряды' и вы хотели бы ознакомиться с теорией и начать с малого"; 
            }
            else
            {
                Help.Text = "У вас неплохой или даже уверенный уровень знаний по теме 'ряды', вы хотите сразу приступить к генерации, изначально ознакомившись с функционалом программы";

            }
        }
    }
}
