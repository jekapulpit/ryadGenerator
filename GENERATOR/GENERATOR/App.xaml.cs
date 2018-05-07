using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace GENERATOR
{
    public partial class App : Application
    {
        public static Generator Current { get; set; }
        public static MainWindow newbee;

        public static USER CurrentUser;

    }

    public class Test
    {
        [Key]
        public int Id { get; set; }
        public int Mark { get; set; }               //Оценка
        [Column(TypeName = "Date")]
        public DateTime date { get; set; }          //дата прохождения
        public string USERusername { get; set; }
        public USER USER { get; set; }            //пользователь, проходящий тест
        static int N = 0;       //количество попыток
        public Test()
        {

        }
        public Test(USER user, DateTime date)
        {
            N++;
            Mark = TRY();
            this.USER = user;
            this.date = date;
        } //Конструктор, в немвызывается метод TRY()
        public int TRY()
        {
            return 0;
        }     //Метод, осуществляющий само тестирование
    }
    public class Generator
    {
        [Key]
        public int Id { get; set; }
        public bool IsFunctional { get; set; }      //Является ли ряд функциональным
        public bool IsPow       { get; set; }      //Является ли ряд степенным
        public bool IsConverge        { get; set; }//Является ли ряд сходящимся 
        public bool IsAlter          { get; set; } //Является ли ряд знакопеременным
        public bool IsRandom { get; set; }          //Является ли ряд СЛУЧАЙНЫМ
        public bool IsWithout9 { get; set; }        //Является ли ряд истонченным
        public string NumCoeffs { get; set; }     //Коэффициенты многочлена в числителе
        public string DomCoeffs { get; set; }     //Коэффициенты многочлена в знаменателе
        public string USERusername { get; set; }
       
        public USER Creator  { get; set; }     //создатель ряда

        public double PowOfNumerator { get; set; }  //Степень многочлена в числителе
        public double PowOfDominator { get; set; }  //Степень многочлена в знаменателе
        public double FullSum { get; set; }         //Полная сумма ряда 
        public double PartSum { get; set; }         //Частичная сумма ряда
        public string path { get; set; }           //Ссылка на изображение ряда (будет представлен в виде картикнки)
        [Column(TypeName = "Date")]
        public DateTime date { get; set; }          //Дата создания ряда
        public Generator()
        {
           
     
        }   //Конструктор

        public Generator(int st1, int st2, string koeffs1,  string koeffs2, string url)
        {
            date = DateTime.Today;
            PowOfDominator = st2;
            PowOfNumerator = st1;
            NumCoeffs = koeffs1;
            DomCoeffs = koeffs2;
            path = url;
                
            IsFunctional = false;
            IsPow = false;
            IsAlter=false; 
            IsAlter=false; 
            IsRandom=false; 
            IsWithout9= false;
            Id = picdownloader.index;
         
            IsConverge = CheckConverge();
             
             
             
        }   //Конструктор1
        public Generator(string url,string  type)
        {
           
            path = url;

            if (type == "usual")
            {
                IsFunctional = false;
                IsPow = false;
                IsAlter = false;
                
                IsRandom = false;
                IsWithout9 = false;
            }
            else
            {
                string[] Type = type.Split('+');
                foreach(string  t   in Type)
                {
                    IsAlter = (t == "alter" ? true : false);
                    IsPow = (t == "pow" ? true : false);
                    IsFunctional = (t == "func" ? true : false);
                    IsRandom = (t == "rand" ? true : false);
                    IsWithout9 = (t == "ukr" ? true : false);
                }

            }

            IsConverge = CheckConverge();



        }   //Конструктор2
        public bool CheckConverge()
        {
            if (!IsFunctional && !IsPow)
            {
                if (IsRandom || IsWithout9) return true;
                if (IsAlter && PowOfDominator > PowOfNumerator) return true;
                if (PowOfDominator - PowOfNumerator > 1) return true;
                return false;
            }
            else return false;
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
                client.DownloadFile(url, AppDomain.CurrentDomain.BaseDirectory + "/pics/ryad" + (index+1) + ".gif");
                index++;
            }
        } // преобразование введенных пользователем 
                                                 // данных в ссылку и скачивание изображения
    }
    public class USER : DependencyObject
    {
        public  static  readonly DependencyProperty usernameProperty = DependencyProperty.Register("username", typeof(string), typeof(USER));
        public ICollection<Generator> Ryads { get; set; }
        public ICollection<Test> Tests { get; set; }

        [Key]
            public string username
        {
            get { return (string)GetValue(usernameProperty); }
            set {SetValue(usernameProperty, value); }
        }

        public string password { get; set; }
       
       
        int lvl { get; set; }                    //"урвень" пользователя, 1 - новичок, 2 - продвинутый, 3 - эксперт
        public USER(string Username, string Password, int Lvl)
        {
            username = Username;
            password = Password;
            lvl = Lvl;
            Ryads = new List<Generator>();
            Tests = new List<Test>();

        }
        public USER()
        {
            Ryads = new List<Generator>();
            Tests = new List<Test>();


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
   


    class USERContext : DbContext
    {
        public USERContext()
        : base("connect")
        { }

        public DbSet<USER> Users { get; set; }
        public DbSet<Generator> Ryads { get; set; }
        public DbSet<Test> Tests { get; set; }

    }
    class GeneratorContext : DbContext
    {
        public GeneratorContext()
        : base("connect")
        { }

        public DbSet<USER> Users { get; set; }
        public DbSet<Generator> Ryads { get; set; }

    }
    class TestContext : DbContext
    {
        public TestContext()
        : base("connect")
        { }
        public DbSet<Test> Tests { get; set; }
        public DbSet<USER> Users { get; set; }
    }
}
