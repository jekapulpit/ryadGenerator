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
            AddUser.CommandText = "select USERNAME, PASSWORD_D from USERS";

            SqlDataReader R = AddUser.ExecuteReader();
            bool match = false;  
             
            while (R.Read())
            {
                if ((string)R.GetValue(0) == Log.Text && (string)R.GetValue(1) == Pass.Text.GetHashCode().ToString()) match = true;
            }
            if (match)
            {
                MainWindow newbee = new MainWindow();
                newbee.Show();
                this.Close();
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
