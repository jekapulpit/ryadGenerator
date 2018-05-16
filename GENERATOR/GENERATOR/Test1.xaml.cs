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
    /// Логика взаимодействия для Test1.xaml
    /// </summary>
    public partial class Test1 : Window
    {
        public List<CheckBox> T = new List<CheckBox>();
        public int stage = 0;
        public int[] ansss = new int[10]; 
        public string[] asks = { "Вопрос 1", "Вопрос 2", "Вопрос 3", "Вопрос 4", "Вопрос 5", "Вопрос 6", "Вопрос 7", "Вопрос 8", "Вопрос 9", "Вопрос 10" };
        public string[] vars1 = { "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1" };
        public string[] vars2 = { "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1" };
        public string[] vars3 = { "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1" };
        public string[] vars4 = { "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1", "вариант 1" };
        public Test1()
        {
            InitializeComponent();
            T.Add(Ch1);
            T.Add(Ch2);
            T.Add(Ch3);
            T.Add(Ch4);
            asktext.Text = asks[0];
            var1.Content = vars1[0]; 
            var2.Content = vars2[0]; 
            var3.Content = vars3[0]; 
            var4.Content = vars4[0];
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            T.Remove((CheckBox)(sender));
            foreach(CheckBox m in T)
            {
                m.IsChecked = false;
            }
            T.Add((CheckBox)(sender));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool pass = false;
            foreach (CheckBox m in T)
            {
                if ((bool)m.IsChecked)
                {
                    ansss[stage] = Convert.ToInt32(m.Name.ToString().Substring(2,1));
                    pass = true;
                }
                m.IsChecked = false;

            }
            if (pass)
            {
                if (stage == 9) {

                    Test t = new Test(App.CurrentUser, DateTime.Today, ansss);
                    using (TestContext T = new TestContext())
                    {
                        T.Tests.Add(t);
                        T.SaveChanges();
                    }
                    if(t.Mark >= 8)
                    {
                        MessageBox.Show("Поздравляем, вы набрали " + t.Mark + " баллов из 10!");
                        App.normal = new normal();

                        App.normal.Resources.Add("CurrentUser", App.CurrentUser.username);
                        using (USERContext m = new USERContext())
                        {
                            m.Users.Find(App.CurrentUser.username).lvl = 1;
                            App.CurrentUser.lvl = 1; 
                            m.SaveChanges();

                        }
                        App.normal.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("К сожалению, вы набрали " + t.Mark + " баллов из 10 и не смогли повысить свой уровень :(");
                        App.newbee = new MainWindow();
                        App.newbee.Resources.Add("CurrentUser", App.CurrentUser.username);

                        App.newbee.Show();
                        this.Close();
                    }
                    this.Close();
                    return;

                }
                stage++;
                asktext.Text = asks[stage];
                var1.Content = vars1[stage];
                var2.Content = vars2[stage];
                var3.Content = vars3[stage];
                var4.Content = vars4[stage];
            }
            else
            {
                MessageBox.Show("Вы должны принять решение! ");
            }
            }
        }
    }

