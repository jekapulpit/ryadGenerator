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
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            auth auth = new auth();
            auth.Show();
            this.Close();
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
            AddUser.CommandText = "insert into USERS (USERNAME, PASSWORD_D, USERTYPE) values('" + Log.Text + "', '" + Pass.Text.GetHashCode().ToString() + "', 'PRO')";

            SqlDataReader R = AddUser.ExecuteReader();
            auth auth = new auth();
            auth.Show();
            this.Close();
        }
    }
}
