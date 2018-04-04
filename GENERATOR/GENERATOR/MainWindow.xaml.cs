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

namespace GENERATOR
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://latex.codecogs.com/gif.latex?%5Csum_%7B%5Cinfty%7D%5E%7B0%7D%5Cfrac%7B1%7D%7Bn%5E2%7D";
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, AppDomain.CurrentDomain.BaseDirectory + "/test.gif");
            }
           
        }
    }

    class Generator
    {
        bool IsFunctional;      //Является ли ряд функциональным
        bool IsPow;             //Является ли ряд степенным
        bool IsConverge;        //Является ли ряд сходящимся 
        bool IsAlter;           //Является ли ряд знакопеременным
        bool IsRandom;          //Является ли ряд СЛУЧАЙНЫМ
        bool IsWithout9;        //Является ли ряд истонченным
        double[] NumCoeffs;     //Коэффициенты многочлена в числителе
        double[] DomCoeffs;     //Коэффициенты многочлена в знаменателе
        double PowOfNumerator;  //Степень многочлена в числителе
        double PowOfDominator;  //Степень многочлена в знаменателе
        double FullSum;         //Полная сумма ряда 
        double PartSum;         //Частичная сумма ряда
        string path;            //Ссылка на изображение ряда (будет представлен в виде картикнки)
        public Generator()
        {

        }   //Конструктор
        public bool CheckConverge() {
            return true;
        }   //Проверка, является ли ряд сходящимся
        public double CountN(int n)
        {
            return 1;
        } //Подсчет n-ного члена ряда
        public double CountFullSum() {
            return 1;
        }//Подсчет полной суммы ряда
        public double CountPartSum(int n)
        {
            return 1;
        } //Подсчет суммы первых n элементов
        public double[] FindConvergeField()
        {
            return null;
        } //Нахождение области сходимости
    }
    static class picdownloader
    {
        public static int index;  //количество картинок (или количество всех рядов)   
        public static void getpic(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, AppDomain.CurrentDomain.BaseDirectory + "/test.gif");
            }
        } // преобразование введенных пользователем 
                                                 // данных в ссылку и скачивание изображения
    }
}

