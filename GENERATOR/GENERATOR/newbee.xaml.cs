﻿using System.Collections.Generic;
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
using System;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace GENERATOR
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public USER CurrentUser;
        public Generator CurrentRYAD;
        SqlConnection thisConnection;

        public MainWindow()
        {
            InitializeComponent();
            thisConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);//открыть соединение
            thisConnection.Open();
            SqlCommand AddRyad = thisConnection.CreateCommand();
            AddRyad.CommandText = "select count(*) from RYADS";
            SqlDataReader R = AddRyad.ExecuteReader();
            R.Read();
            picdownloader.index = Convert.ToInt32(R.GetValue(0));
            R.Close();
        }

      

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser = null;
            auth T = new auth();
            T.Show();
            this.Close();
        }

        private void GenerateR(object sender, RoutedEventArgs e)
        {
            try
            {
                GetRyad();
                picdownloader.getpic(CurrentRYAD.path);
                thisConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString);//открыть соединение
                thisConnection.Open();
                SqlCommand AddRyad =new SqlCommand("Addinnewbee", thisConnection);

                AddRyad.CommandType = CommandType.StoredProcedure;
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@id";
                param1.SqlDbType = SqlDbType.Int;
                param1.Value = picdownloader.index;

                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@us";
                param2.SqlDbType = SqlDbType.NVarChar;
                param2.Size = 20;
                param2.Value = Resources["CurrentUser"];

                SqlParameter param3 = new SqlParameter();
                param3.ParameterName = "@picture";
                param3.SqlDbType = SqlDbType.NVarChar;
                param3.Size = 100;
                param3.Value = CurrentRYAD.path.Substring(68);

                SqlParameter param4 = new SqlParameter();
                param4.ParameterName = "@type";
                param4.SqlDbType = SqlDbType.Char;
                param4.Size = 10;
                param4.Value = "usual";
                AddRyad.Parameters.Add(param1);
                AddRyad.Parameters.Add(param4);
                AddRyad.Parameters.Add(param2);
                AddRyad.Parameters.Add(param3);
                AddRyad.ExecuteNonQuery();


                //AddRyad.CommandText = "insert into RYADS (RYADID, RYADTYPE, CREATOR, IMGURL, CTEATED) values(" + picdownloader.index + ", 'usual', '" + Resources["CurrentUser"] + "','" + CurrentRYAD.path.Substring(68) + "', default )";
                //SqlDataReader R = AddRyad.ExecuteReader();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("E:\\ЛАБОРАТОРНЫЕ И КОМПЛЕКТУЮЩИЕ\\Курсач\\GENERATOR\\GENERATOR\\bin\\Debug\\pics\\ryad" + picdownloader.index + ".gif", UriKind.Absolute);
                bi3.EndInit();
              
                Rimage.Source = bi3;
               

                Koeffs1.BorderBrush = null;
                Koeffs1.BorderBrush = null;
                //R.Close();

                Label shod = new Label();
                shod.Margin = new Thickness(10, 19, 416, 140);
                shod.Content = "Сходимость: " + (CurrentRYAD.IsConverge ? "Сходится":"Не сходится");
                 
                 

                lolls.Children.Add(shod);
                 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void GetRyad()
        {
            try
            {             
                string result = picdownloader.starturl;
                string[] ch = this.Koeffs1.Text.Split(' ');
                string[] zn = this.Koeffs2.Text.Split(' ');
                if (ch.Length != chisl.Value+1 || zn.Length != znam.Value+1)
                    throw new Exception();
                double[] coeffs1 = new double[ch.Length];
                double[] coeffs2 = new double[zn.Length];
                for (int i = 0; i < ch.Length; i++)
                {
                    coeffs1[i] = Convert.ToDouble(ch[i]);
                }
                for (int i = 0; i < zn.Length; i++)
                {
                    coeffs2[i] = Convert.ToDouble(zn[i]);
                }
                result += picdownloader.start;
                result += picdownloader.drob;
                result += picdownloader.start;
                bool flag = true;
                for(int i = (int)chisl.Value; i >= 0; i--)
                {
                   
                        if (coeffs1[i] > 0)
                        {

                            result += ((flag ? "" : "&plus;") + coeffs1[i].ToString() + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                            flag = false;

                        } else if (coeffs1[i] < 0)
                        {
                            result += ((flag ? "" : "-") + coeffs1[i].ToString() + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                            flag = false;
                        } else if (coeffs1[i] == 1)
                        {
                            result += ((flag ? "" : "&plus;") + (i == 0 ? "1" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                            flag = false;
                        } else if (coeffs1[i] == -1)
                        {
                            result += ((flag ? "" : "-") + (i == 0 ? "1" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                            flag = false;
                        } else
                        {

                        }
                    
                }
                result += picdownloader.end;
                result += picdownloader.start;
                flag = true;
                for (int i = (int)znam.Value; i >= 0; i--)
                {
                    if (coeffs2[i] > 0)
                    {

                        result += ((flag ? "" : "&plus;") + coeffs2[i].ToString() + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;

                    }
                    else if (coeffs2[i] < 0)
                    {
                        result += ((flag ? "" : "-") + coeffs2[i].ToString() + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;
                    }
                    else if (coeffs2[i] == 1)
                    {
                        result += ((flag ? "" : "&plus;") + (i == 0 ? "1" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;
                    }
                    else if (coeffs2[i] == -1)
                    {
                        result += ((flag ? "" : "-") + (i == 0 ? "1" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;
                    }
                    else
                    {

                    }

                }
                result += picdownloader.end;
                result += picdownloader.end;
                picdownloader.index++;
                CurrentRYAD = new Generator((int)chisl.Value, (int)znam.Value, coeffs1, coeffs2, result);
            }
            catch(Exception e)
            {
                Koeffs1.BorderBrush = new SolidColorBrush(Colors.Red);
                Koeffs1.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            history T = new history(Resources["CurrentUser"].ToString());
            
            T.Show();

           
      }
    }

}

