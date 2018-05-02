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
        SqlConnection thisConnection;

        USER CurrentUser;
        public auth()
        {
            InitializeComponent();
           
        
        }
        private void But_Click(object sender, RoutedEventArgs e)
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



            SqlCommand AddUser = thisConnection.CreateCommand();
            AddUser.CommandText = "select * from USERS";

            SqlDataReader R = AddUser.ExecuteReader();
            bool match = false;  
             
            while (R.Read())
            {
                if ((string)R.GetValue(0) == Log.Text && (string)R.GetValue(1) == Pass.Password.GetHashCode().ToString()) { match = true;

                    int lvl;
                    switch (R.GetValue(2))
                    {
                        case "NEW": lvl = 1; break;
                        case "MEDIUM": lvl = 2; break;
                        case "PRO": lvl = 3; break;
                        default: lvl = 1;break;
                    }  
                   CurrentUser = new USER(Log.Text, Pass.Password.GetHashCode().ToString(), lvl);

                }
            }
            if (match)
            {
                App.newbee = new MainWindow();           
                 
                App.newbee.Resources.Add("CurrentUser", CurrentUser.username);
                App.newbee.Show();
                this.Close();
            }
            else
            {
                 Log.BorderBrush= new SolidColorBrush(Colors.Red);
                 Pass.BorderBrush = new SolidColorBrush(Colors.Red);
                 validate.Visibility = Visibility.Visible;
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
