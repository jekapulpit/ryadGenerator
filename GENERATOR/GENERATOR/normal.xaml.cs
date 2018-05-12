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
    /// Логика взаимодействия для normal.xaml
    /// </summary>
    public partial class normal : Window
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
        public normal()
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
                    bi3.UriSource = new Uri("E:\\ЛАБОРАТОРНЫЕ И КОМПЛЕКТУЮЩИЕ\\Курсач\\GENERATOR\\GENERATOR\\bin\\Debug\\pics\\ryad" + (CurrentRYAD.Id + 1) + ".gif", UriKind.Absolute);
                    bi3.EndInit();
                    Rimage.Source = bi3;
                    Koeffs1.BorderBrush = null;
                    Koeffs1.BorderBrush = null;
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
                    N.Visibility = Visibility.Visible;
                    nchlen.Visibility = Visibility.Visible;
                    nchlen1.Visibility = Visibility.Visible;
                   
                    sum.Visibility = Visibility.Visible;
                    S.Content = "Рассчитать";
                    S.Style = (Style)Resources["reg"];
                    S.Click += setsum;
                    S1.Content = "Рассчитать";
                    S1.Click += setn;
                    S.Visibility = Visibility.Visible;
                    shod.Content = "Сходимость: " + (CurrentRYAD.IsConverge ? "Сходится" : "Не сходится");
                    sum1.Content = "Рассчитать сумму первых N членов: ";
                    sum.Content = "Полная сумма ряда: ";
                    sum.Content += CurrentRYAD.CountFullSum() == null ? "бесконечность" : CurrentRYAD.CountFullSum().ToString();
                    N.Content = "N =";
                    nchlen.Content = "Рассчитать n-ый член ряда: ";
                    nchlen1.Content = "n = ";
                    if (CurrentRYAD.IsPow)
                    {
                        N.Visibility = Visibility.Hidden;
                        nchlen.Visibility = Visibility.Hidden;
                        nchlen1.Visibility = Visibility.Hidden;
                        S.Visibility = Visibility.Hidden;
                        m.Visibility = Visibility.Hidden;
                        m1.Visibility = Visibility.Hidden;
                        S1.Visibility = Visibility.Hidden;
                        sum.Visibility = Visibility.Hidden;
                        sum1.Content = "Радиус сходипости: -a<|x|<a";
                        shod.Content = "Область сходипости: |x-a|<1";
                    }
                    CurrentRYAD.USERusername = App.CurrentUser.username;
                    db.Ryads.Add(CurrentRYAD);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void GetRyad()
        {
            try
            {
               
                string result = picdownloader.starturl;
                if ((bool)IsRandom.IsChecked)
                {
                    result += picdownloader.random;
                    CurrentRYAD = new Generator(0, 1, "1", "0 1", result, false, false, true, false);
                    return;
                }
                if ((bool)IsWithout9.IsChecked)
                {
                    result += picdownloader.without9;
                    CurrentRYAD = new Generator(0, 1, "1", "0 1", result, false, false, false, true);
                    return;
                }
                string[] ch = this.Koeffs1.Text.Split(' ');
                string[] zn = this.Koeffs2.Text.Split(' ');
                if (ch.Length != chisl.Value + 1 || zn.Length != znam.Value + 1)
                    throw new Exception();

                result += picdownloader.start;
                if ((bool)IsAlter.IsChecked) result += picdownloader.Alter;
                result += picdownloader.drob;
                result += picdownloader.start;
                
                bool flag = true;
                for (int i = (int)chisl.Value; i >= 0; i--)
                {

                    if (Convert.ToDouble(ch[i]) > 0 && Convert.ToDouble(ch[i]) != 1)
                    {

                        result += ((flag ? "" : "&plus;") + ch[i] + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;

                    }
                    else if (Convert.ToDouble(ch[i]) < 0)
                    {
                        result += ((flag ? "" : "-") + ch[i] + (i == 0 ? "" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;
                    }
                    else if (Convert.ToDouble(ch[i]) == 1)
                    {
                        result += ((flag ? "" : "&plus;") + (i == 0 ? "1" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;
                    }
                    else if (Convert.ToDouble(ch[i]) == -1)
                    {
                        result += ((flag ? "" : "-") + (i == 0 ? "1" : ("n" + (i == 1 ? "" : (picdownloader.stepen + i.ToString())))));
                        flag = false;
                    }
                    else
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
                if ((bool)IsStep.IsChecked)
                    result += picdownloader.Pow;
                result += picdownloader.end;
                CurrentRYAD = new Generator((int)chisl.Value, (int)znam.Value, Koeffs1.Text, Koeffs2.Text, result, (bool)IsAlter.IsChecked, (bool)IsStep.IsChecked, (bool)IsRandom.IsChecked, (bool)IsWithout9.IsChecked);
                

            }
            catch (Exception e)
            {
                Koeffs1.BorderBrush = new SolidColorBrush(Colors.Red);
                Koeffs2.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
        }
        public static void setryad(Generator Curr, int id)
        {
            CurrentRYAD = Curr;
            App.normal.m1.Visibility = Visibility.Visible;
            App.normal.S1.Visibility = Visibility.Visible;
            App.normal.N.Visibility = Visibility.Visible;
            App.normal.m.Visibility = Visibility.Visible;
            App.normal.sum1.Visibility = Visibility.Visible;
            App.normal.sum.Visibility = Visibility.Visible;
            App.normal.nchlen.Visibility = Visibility.Visible;
            App.normal.nchlen1.Visibility = Visibility.Visible;
            App.normal.bi3 = new BitmapImage();
            App.normal.bi3.BeginInit();
            App.normal.bi3.UriSource = new Uri("E:\\ЛАБОРАТОРНЫЕ И КОМПЛЕКТУЮЩИЕ\\Курсач\\GENERATOR\\GENERATOR\\bin\\Debug\\pics\\ryad" + id + ".gif", UriKind.Absolute);
            App.normal.bi3.EndInit();
            App.normal.Rimage.Source = App.normal.bi3;
            App.normal.shod.Content = "Сходимость: " + (CurrentRYAD.IsConverge ? "Сходится" : "Не сходится");
            App.normal.sum1.Margin = new Thickness(10, 34, 416, 125);
            App.normal.N.Margin = new Thickness(10, 55, 740, 105);
            App.normal.m.Margin = new Thickness(40, 57, 710, 113);
            App.normal.S.Margin = new Thickness(10, 90, 693, 80);
            App.normal.sum.Margin = new Thickness(10, 110, 416, 45);
            App.normal.nchlen.Content = "Рассчитать n-ый член ряда: ";
            App.normal.nchlen.Margin = new Thickness(10, 130, 416, 30);
            App.normal.S.Content = "Рассчитать";
            App.normal.S1.Margin = new Thickness(100, 157, 600, 13);
            App.normal.m1.Margin = new Thickness(40, 157, 710, 13);
            App.normal.m1.Visibility = Visibility.Visible;
            App.normal.S1.Visibility = Visibility.Visible;
            App.normal.S1.Content = "Рассчитать";
            App.normal.S1.Click += App.normal.setn;
            App.normal.nchlen1.Margin = new Thickness(10, 155, 740, 5);
            App.normal.nchlen1.Content = "n = ";
            App.normal.S.Click += App.normal.setsum;
            App.normal.S.Visibility = Visibility.Visible;
            App.normal.sum1.Content = "Рассчитать сумму первых N членов: ";
            App.normal.N.Content = "N =";
            App.normal.sum.Content = "Полная сумма ряда: ";
            App.normal.sum.Content += CurrentRYAD.CountFullSum() == null ? "бесконечность" : CurrentRYAD.CountFullSum().ToString();
            if (CurrentRYAD.IsPow)
            {
                App.normal.N.Visibility = Visibility.Hidden;
                App.normal.nchlen.Visibility = Visibility.Hidden;
                App.normal.nchlen1.Visibility = Visibility.Hidden;
                App.normal.S.Visibility = Visibility.Hidden;
                App.normal.m.Visibility = Visibility.Hidden;
                App.normal.m1.Visibility = Visibility.Hidden;
                App.normal.S1.Visibility = Visibility.Hidden;
                App.normal.sum.Visibility = Visibility.Hidden;
                App.normal.sum1.Content = "Радиус сходипости: -a<|x|<a";
                App.normal.shod.Content = "Область сходипости: |x-a|<1";
            }

        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            history T = new history(Resources["CurrentUser"].ToString(), ((MenuItem)sender).Name.ToString());
            T.Show();
        }
        private void setsum(object sender, RoutedEventArgs e)
        {
            double? res = CurrentRYAD.CountPartSum(Convert.ToInt32(m.Text));

            if (res == null) sum1.Content = "Рассчитать сумму первых N членов: бесконечность";
            else
            {
                sum1.Content = "Рассчитать сумму первых N членов: " + res.ToString().Substring(0, 7);
            }
        }
        private void setn(object sender, RoutedEventArgs e)
        {
            double? res = CurrentRYAD.CountN(Convert.ToInt32(m1.Text));

            if (res == null) nchlen.Content = "Рассчитать n-ый член ряда: бесконечность";
            else
            {
                nchlen.Content = "Рассчитать n-ый член ряда: " + res.ToString();
            }
        }

        private void IsRandom_Checked(object sender, RoutedEventArgs e)
        {
            IsAlter.IsChecked = false;
            IsStep.IsChecked = false;
            IsWithout9.IsChecked = false;
        }

        private void IsWithout9_Checked(object sender, RoutedEventArgs e)
        {
            IsAlter.IsChecked = false;
            IsStep.IsChecked = false;
            IsRandom.IsChecked = false;
        }

        private void IsAlter_Checked(object sender, RoutedEventArgs e)
        {
            IsWithout9.IsChecked = false;
            IsRandom.IsChecked = false;
        }

        
    }
}
