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
using System;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace GENERATOR
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Generator CurrentRYAD = new Generator();
        BitmapImage bi3 = new BitmapImage();
        Label shod = new Label();
        Label sum1 = new Label();
        Label N = new Label();
        Label sum = new Label();
        Label nchlen = new Label();
        Label nchlen1 = new Label();
        TextBox m = new TextBox();
        TextBox m1 = new TextBox();
        Button S = new Button();
        Button S1 = new Button();
        

        public MainWindow()
        {
            InitializeComponent();
           
               
            lolls.Children.Add(shod);
            lolls.Children.Add(sum1);
            lolls.Children.Add(sum);
            lolls.Children.Add(nchlen);
            shod.Margin = new Thickness(10, 19, 416, 140);
            lolls.Children.Add(m);
            lolls.Children.Add(N);
            lolls.Children.Add(S);
            lolls.Children.Add(m1);
            lolls.Children.Add(nchlen1);

            m1.Visibility = Visibility.Hidden;
            lolls.Children.Add(S1);
            S.Visibility = Visibility.Hidden;
            S1.Visibility = Visibility.Hidden;
            S.Style = (Style)Resources["reg"];
            using (GeneratorContext ryads = new GeneratorContext())
            {
                picdownloader.index = ryads.Ryads.Count();
            }
        }

      

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            auth T = new auth();
            T.Show();
            this.Close();
        }
       
        private void GenerateR(object sender, RoutedEventArgs e)
        {
            try
            {
                using (GeneratorContext db = new GeneratorContext())
                {
                    GetRyad();
                    picdownloader.getpic(CurrentRYAD.path);




                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\pics\\ryad" + (CurrentRYAD.Id+1) + ".gif", UriKind.Absolute);
                    bi3.EndInit();

                    Rimage.Source = bi3;


                    Koeffs1.BorderBrush = null;
                    Koeffs1.BorderBrush = null;
                    //R.Close();

                    shod.Margin = new Thickness(10, 19, 416, 140);
                    sum1.Margin = new Thickness(10, 34, 416, 125);
                    N.Margin = new Thickness(10, 55, 740, 105);
                    m.Margin = new Thickness(40, 57, 710, 113);
                    S.Margin = new Thickness(10, 85, 693, 80);
                    sum.Margin = new Thickness(10, 110, 416, 45);
                    nchlen.Margin = new Thickness(10, 130, 416, 30);
                    nchlen1.Margin = new Thickness(10, 155, 740, 5);
                    S1.Margin = new Thickness(100, 157, 600, 13);
                    m1.Margin = new Thickness(40, 157, 710, 13);
                    m1.Visibility = Visibility.Visible;
                    S1.Visibility = Visibility.Visible;
                    S.Content = "Рассчитать";
                    S.Style = (Style)Resources["reg"];
                    S.Click += setsum;
                    S1.Content = "Рассчитать";
                    S1.Click += setn;
                    S.Visibility = Visibility.Visible;
                    shod.Content = "Сходимость: " + (CurrentRYAD.IsConverge ? "Сходится" : "Не сходится");
                    sum1.Content = "Рассчитать сумму первых N членов: ";
                    sum.Content = "Полная сумма ряда: ";
                    sum.Content += CurrentRYAD.CountFullSum() == null ? "бесконечность" : CurrentRYAD.CountFullSum().ToString().Substring(0, 7);
                    N.Content = "N =";
                    nchlen.Content = "Рассчитать n-ый член ряда: ";
                    nchlen1.Content = "n = ";
                    CurrentRYAD.USERusername = App.CurrentUser.username;
                    db.Ryads.Add(CurrentRYAD);
                    db.SaveChanges();
                }
                 
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка! Отсутствует подключение к интернету!");
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
             
                result += picdownloader.start;
                result += picdownloader.drob;
                result += picdownloader.start;
                bool flag = true;
                for(int i = (int)chisl.Value; i >= 0; i--)
                {
                   
                        if (Convert.ToDouble(ch[i]) > 0  && Convert.ToDouble(ch[i])   !=  1)
                        {

                            result += ((flag ? "" : "&plus;") + ch[i] + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                            flag = false;

                        } else if (Convert.ToDouble(ch[i]) < 0)
                        {
                            result += ((flag ? "" : "-") + ch[i] + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                            flag = false;
                        } else if (Convert.ToDouble(ch[i]) == 1)
                        {
                            result += ((flag ? "" : "&plus;") + (i == 0 ? "1" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                            flag = false;
                        } else if (Convert.ToDouble(ch[i]) == -1)
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
                    if (Convert.ToDouble(zn[i]) > 0 && Convert.ToDouble(zn[i]) != 1)
                    {

                        result += ((flag ? "" : "&plus;") + Convert.ToDouble(zn[i]) + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;

                    }
                    else if (Convert.ToDouble(zn[i]) < 0)
                    {
                        result += ((flag ? "" : "-") + zn[i] + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;
                    }
                    else if (Convert.ToDouble(zn[i]) == 1)
                    {
                        result += ((flag ? "" : "&plus;") + (i == 0 ? "1" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;
                    }
                    else if (Convert.ToDouble(zn[i]) == -1)
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
                CurrentRYAD = new Generator((int)chisl.Value, (int)znam.Value, Koeffs1.Text, Koeffs2.Text, result);
               
               

            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка! Неверный формат заполнения!");
                Koeffs1.BorderBrush = new SolidColorBrush(Colors.Red);
                Koeffs2.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
        }
        public static void setryad(Generator Curr, int id)
        {
            CurrentRYAD = Curr;

            App.newbee.bi3 = new BitmapImage();
            App.newbee.bi3.BeginInit();
            App.newbee.bi3.UriSource = new Uri("E:\\ЛАБОРАТОРНЫЕ И КОМПЛЕКТУЮЩИЕ\\Курсач\\GENERATOR\\GENERATOR\\bin\\Debug\\pics\\ryad" + id + ".gif", UriKind.Absolute);
            App.newbee.bi3.EndInit();
            App.newbee.Rimage.Source = App.newbee.bi3;
            App.newbee.shod.Content = "Сходимость: " + (CurrentRYAD.IsConverge ? "Сходится" : "Не сходится");
            App.newbee.sum1.Margin = new Thickness(10, 34, 416, 125);
            App.newbee.N.Margin = new Thickness(10, 55, 740, 105);
            App.newbee.m.Margin = new Thickness(40, 57, 710, 113);
            App.newbee.S.Margin = new Thickness(10, 90, 693, 80);
            App.newbee.sum.Margin = new Thickness(10, 110, 416, 45);
            App.newbee.nchlen.Content = "Рассчитать n-ый член ряда: ";
            App.newbee.nchlen.Margin = new Thickness(10, 130, 416, 30);

            App.newbee.S.Content = "Рассчитать";
            App.newbee.S1.Margin = new Thickness(100, 157, 600, 13);
            App.newbee.m1.Margin = new Thickness(40, 157, 710, 13);
            App.newbee.m1.Visibility = Visibility.Visible;
            App.newbee.S1.Visibility = Visibility.Visible;
            App.newbee.S1.Content = "Рассчитать";
            App.newbee.S1.Click += App.newbee.setn;
            App.newbee.nchlen1.Margin = new Thickness(10, 155, 740, 5);
            App.newbee.nchlen1.Content = "n = ";
            App.newbee.S.Click += App.newbee.setsum;
            App.newbee.S.Visibility = Visibility.Visible;
            App.newbee.sum1.Content = "Рассчитать сумму первых N членов: ";
            App.newbee.N.Content = "N =";
            App.newbee.sum.Content = "Полная сумма ряда: ";
            App.newbee.sum.Content += CurrentRYAD.CountFullSum() == null ? "бесконечность" : CurrentRYAD.CountFullSum().ToString().Substring(0, 7);


        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            history T = new history(Resources["CurrentUser"].ToString(), ((MenuItem)sender).Name.ToString());
            
            T.Show();

           
        }
        private void setsum(object sender, RoutedEventArgs e)
        {
            try { 
            double? res = CurrentRYAD.CountPartSum(Convert.ToInt32(m.Text)); 

            if(res == null) sum1.Content = "Рассчитать сумму первых N членов: бесконечность";
            else
            {
                sum1.Content = "Рассчитать сумму первых N членов: " + res.ToString();
            }
            }
            catch
            {
                MessageBox.Show("Ошибка! Неверный формат заполнения!");
            }
        }
        private void setn(object sender, RoutedEventArgs e)
        {
            try
            {
                double? res = CurrentRYAD.CountN(Convert.ToInt32(m1.Text));

                if (res == null) nchlen.Content = "Рассчитать n-ый член ряда: бесконечность";
                else
                {
                    nchlen.Content = "Рассчитать n-ый член ряда: " + res.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка! Неверный формат заполнения!");
            }

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            PassTest n = new PassTest();
            n.Show();
            this.Close();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                Process SysInfo = new Process();
                SysInfo.StartInfo.ErrorDialog = true;
                SysInfo.StartInfo.FileName = "Руководство.chm";
                SysInfo.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}

