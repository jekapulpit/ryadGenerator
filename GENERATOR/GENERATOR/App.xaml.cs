using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Data.SqlClient;
namespace GENERATOR
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
      
    }

    public class Generator
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
        public string path;           //Ссылка на изображение ряда (будет представлен в виде картикнки)
        DateTime date;          //Дата создания ряда
        public Generator()
        {
           
     
        }   //Конструктор

        public Generator(int st1, int st2, double[] koeffs1, double[] koeffs2, string url)
        {
            PowOfDominator = st2;
            PowOfNumerator = st1;
            NumCoeffs = koeffs1;
            DomCoeffs = koeffs2;
            path = url;
        }   //Конструктор1

        public bool CheckConverge()
        {
            return true;
        }   //Проверка, является ли ряд сходящимся
        public double CountN(int n)
        {
            return 1;
        } //Подсчет n-ного члена ряда
        public double CountFullSum()
        {
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
        public static string starturl = "https://latex.codecogs.com/gif.latex?%5Csum_%7B1%7D%5E%7B%5Cinfty%7D";
        public static string drob = "%5Cfrac";

        public static string start = "%7B";
        public static string end = "%7D";
        public static string stepen = "%5E";
        public static int index = 0;  //количество картинок (или количество всех рядов)   
        
        public static void getpic(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, AppDomain.CurrentDomain.BaseDirectory + "/pics/ryad" + index + ".gif");
            }
        } // преобразование введенных пользователем 
                                                 // данных в ссылку и скачивание изображения
    }
    public class USER : DependencyObject
    {
        public  static  readonly DependencyProperty usernameProperty = DependencyProperty.Register("username", typeof(string), typeof(USER));
      
        public string username
        {
            get { return (string)GetValue(usernameProperty); }
            set {SetValue(usernameProperty, value); }
        }

        public string password { get; set; }
       
       
        int lvl;                    //"урвень" пользователя, 1 - новичок, 2 - продвинутый, 3 - эксперт
        public USER(string Username, string Password, int Lvl)
        {
            username = Username;
            password = Password;
            lvl = Lvl;
        }
        public int PassTest()
        {
            return 0;
        }
        private static bool PasswordValidate(object value)
        {
            string curr = (string)value;
            if (curr.Length < 6) return false;
            return true; 
        }
         


    }
    class Test
    {
        int Mark;               //Оценка
        DateTime date;          //дата прохождения
        string user;            //пользователь, проходящий тест
        static int N = 0;       //количество попыток
        public Test(string user, DateTime date)
        {
            N++;
            Mark = TRY();
            this.user = user;
            this.date = date;
        } //Конструктор, в немвызывается метод TRY()
        public int TRY()
        {
            return 0;
        }     //Метод, осуществляющий само тестирование
    }

}
