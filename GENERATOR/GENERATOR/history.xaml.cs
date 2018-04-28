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
using System.Data;

namespace GENERATOR
{
    /// <summary>
    /// Логика взаимодействия для history.xaml
    /// </summary>
    public partial class history : Window
    {
        public string currentuser;
        public history(string t)
        {
            InitializeComponent();
            currentuser = t;

            SqlConnection thisConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);//открыть соединение
            thisConnection.Open();
            SqlCommand AddRyad = thisConnection.CreateCommand();
            AddRyad.CommandText = "select count(*) from RYADS";
            SqlDataReader R = AddRyad.ExecuteReader();
            R.Read();
            picdownloader.index = Convert.ToInt32(R.GetValue(0));
            R.Close();
            AddRyad.CommandText = "select * from RYADS where CREATOR = '" + currentuser +"'";
            R = AddRyad.ExecuteReader();
           
            while (R.Read())
            {

                Border border = new Border();
              


                border.BorderThickness = new Thickness(0,1,0,1);

                border.BorderBrush = new SolidColorBrush(Colors.Black);
                
                

               
                Grid Y = new Grid();
                Y.Height = 70;
                Y.Background = new SolidColorBrush(Colors.White);
                Y.MouseEnter += backmark;
                Y.MouseLeave += backmark1;
                all.Children.Add(border);
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("E:\\ЛАБОРАТОРНЫЕ И КОМПЛЕКТУЮЩИЕ\\Курсач\\GENERATOR\\GENERATOR\\bin\\Debug\\pics\\ryad" +  R.GetValue(0) + ".gif", UriKind.Absolute);
                bi3.EndInit();
                Image K = new Image();
                 
                 
                K.Margin = new Thickness(0, 10, 300, 10);
                K.Source = bi3;
                Label Shod = new Label();
                // создать  экземпляр ряда из бд, добавив  полный конструктор 
                //  Shod.Content="Сходимость:" 
                border.Child = Y;
                Y.Children.Add(K);
            }


            R.Close();
        }
        public void backmark(object sender,MouseEventArgs e) {
            Grid temp = (Grid)sender;
            temp.Background = new SolidColorBrush(Colors.SkyBlue);
        }
        public void backmark1(object sender, MouseEventArgs e)
        {
            Grid temp = (Grid)sender;
            temp.Background = new SolidColorBrush(Colors.White);
        }
    }
}
