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

        SqlConnection thisConnection;
        public register()
        {
            InitializeComponent();
           
            
            Log.BorderBrush = null;
            Pass.BorderBrush = null;
            Conf.BorderBrush = null;
            Confirm.Opacity = 0;
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
            try
            {
                thisConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);//открыть соединение
                thisConnection.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Коннектион инпосебле" + e);
            }
            if (validate(this, null))
            {

                SqlCommand CheckUser = thisConnection.CreateCommand();
                CheckUser.CommandText = "Select username from USERS";
                SqlDataReader R1 = CheckUser.ExecuteReader();
                bool match = false;
                while (R1.Read())
                {
                    if (Log.Text == (string)R1.GetValue(0))
                        match = true;
                }
                R1.Close();

                if (!match)
                {

                    SqlCommand AddUser = thisConnection.CreateCommand();
                    AddUser.CommandText = "insert into USERS (USERNAME, PASSWORD_D, USERTYPE) values('" + Log.Text + "', '" + Pass.Password.GetHashCode().ToString() + "', 'PRO')";
                    SqlDataReader R = AddUser.ExecuteReader();
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
}
