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
        Generator CurrentRyad;
         public history(string t)
        {
            InitializeComponent();
            currentuser = t;
            using (RYAD list = new RYAD()) { 
                var G = list.Ryads.GetAll(currentuser);
                picdownloader.index = list.Ryads.Count();
                foreach (Generator m in G)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(0, 1, 0, 1);
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    Grid Y = new Grid();
                    Y.Height = 70;
                    Y.Background = new SolidColorBrush(Colors.White);
                    Y.MouseEnter += backmark;
                    Y.MouseLeave += backmark1;
                    Y.MouseDown += backmark2;
                    Y.Name = "nn" + m.Id;
                    all.Children.Add(border);
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri("E:\\ЛАБОРАТОРНЫЕ И КОМПЛЕКТУЮЩИЕ\\Курсач\\GENERATOR\\GENERATOR\\bin\\Debug\\pics\\ryad" + m.Id + ".gif", UriKind.Absolute);
                    bi3.EndInit();
                    Image K = new Image();
                    K.Margin = new Thickness(0, 10, 300, 10);
                    K.Source = bi3;
                    Label Shod = new Label();
                    Shod.Content = "Дата создания: " + m.date.ToString().Substring(0, 10);
                    Shod.Margin = new Thickness(500, 5, 100, 0);
                    Label Shoshod = new Label();
                    Label Shotype = new Label();
                    string type;
                    if (!m.IsAlter && !m.IsFunctional && !m.IsPow && !m.IsRandom && !m.IsWithout9) type = "Обычный ";
                    else type = "";
                    if (m.IsConverge && !m.IsPow && !m.IsWithout9 && !m.IsRandom) type += "сходящийся ";
                    if (!m.IsConverge && !m.IsPow && !m.IsWithout9 && !m.IsRandom) type += "рассходящийся ";
                    if(m.IsAlter) type += "знакопеременный ";
                    if(m.IsPow) type += "степенной ";
                    if(m.IsWithout9) type += "истонченный ";
                    if(m.IsRandom) type += "случайный ";
                    
                    Shotype.Content = type + "ряд";
                    Shotype.Margin = new Thickness(500, 20, 100, 0);
                    border.Child = Y;
                    Y.Children.Add(K);
                    Y.Children.Add(Shod);
                    Y.Children.Add(Shotype);
                }
            }
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
        public void backmark2(object sender, MouseEventArgs e)
        {
            try
            {
               using (GeneratorContext t = new GeneratorContext()) {
                    var urrentRyad = from p in t.Ryads
                                  where p.Id.ToString() == ((Grid)sender).Name.ToString().Substring(2)
                                  select p;
                    if(App.CurrentUser.lvl == 1)
                        normal.setryad(urrentRyad.First<Generator>(), Convert.ToInt32(((Grid)sender).Name.ToString().Substring(2)));
                    else    
                    MainWindow.setryad(urrentRyad.First<Generator>(), Convert.ToInt32(((Grid)sender).Name.ToString().Substring(2)));

                }
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
