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
        public string[] asks = { "Какого вида рядов не существует?",
                                 "При исследовании ряда на сходипость сначала проверяется...",
                                 "Когда сумма ряда стремится к определенному числу?",
                                 "Чему должен быть равен предел n-ного члена ряда при n стремящемся к бесконечности, чтобы считать необходимое условие соблюденным? ",
                                 "Если при проверке ряда на сходимость признаком Даламбера предел равен 1, какой вывод можно заключить?",
                                 "Установите сходимость знакопеременного гармонического ряда",
                                 "Установите сходимость случайного гармонического ряда",
                                 "Достаточное условие условной сходимости",
                                 "Что должно получиться в результате интегрирования согласно признаку Коши?",
                                 "Что называют истонченным гармоническим рядом?" };
        public string[] vars1 = { "Обобщенного гармонического", "Условие Коши-Римана", "Если ряд сходится", "+Бесконечности", "Ряд сходится", "Сходится условно", "Сходится условно", "Монотонное убывание", "Любое конечное число", "Гармонический ряд, в знаменателе которого пропускаются девятки" };
        public string[] vars2 = { "Функционального", "Необходимое условие", "Если соблюдается только необходимое условие", "-Бесконечности", "Ряд расходится", "Сходится абсолютно", "Расходится", "То же, что и необходимое", "0", "Любой расходящийся ряд" };
        public string[] vars3 = { "Знакопеременного", "Вид ряда", "Всегда", "0", "Ряд сходится условно", "Расходится", "Сходится абсолютно", "Его нет", "1", "Любой условно сходящийся ряд" };
        public string[] vars4 = { "Волнового", "Достаточное условие", "Никогда", "Любое конечное число", "Никакого", "Нельзя установить", "Нельзя установить", "Абсолютная сходимость", "Бесконечность", "Ряд без счетчика" };
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

