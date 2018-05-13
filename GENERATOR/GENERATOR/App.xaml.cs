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
        public static normal normal;

        public static USER CurrentUser;
        public static Test1 TestWindow;

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
        public Test(USER user, DateTime date, int[] ansss)
        {
            Random T = new Random();
            this.Id = T.Next(-1000000, 1000000).GetHashCode();
            N++;
            Mark = TRY(ansss);
            this.USERusername = user.username;
            this.date = date;
        } //Конструктор, в немвызывается метод TRY()
        public int TRY(int [] ansss)
        {
            int result = 0;
            int[] answrs = {1,2,1,3,4,1,3,1,1,1};
            int[] ans = ansss;
            for(int i = 0; i < 10; i++)
            {
                if (answrs[i] == ans[i])
                    result++;
            }


            return result;
        }     //Метод, осуществляющий само тестирование
    }
    public class Generator
    {
        [Key]
        public int Id { get; set; }
        public bool IsFunctional { get; set; }      //Является ли ряд функциональным
        public bool IsPow  { get; set; }      //Является ли ряд степенным
        public bool IsConverge  { get; set; }//Является ли ряд сходящимся 
        public bool IsAlter  { get; set; } //Является ли ряд знакопеременным
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

        public Generator(int st1,
                         int st2, 
                         string koeffs1,  
                         string koeffs2, 
                         string url)
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
        public Generator(int st1, 
                         int st2, 
                         string koeffs1, 
                         string koeffs2, 
                         string url, 
                         bool isalter, 
                         bool ispow, 
                         bool israndom, 
                         bool iswithout9)
        {
            date = DateTime.Today;
            PowOfDominator = st2;
            PowOfNumerator = st1;
            NumCoeffs = koeffs1;
            DomCoeffs = koeffs2;
            path = url;

            IsFunctional = false;
            IsPow = ispow;
            IsAlter = isalter;
      
            IsRandom = israndom;
            IsWithout9 = iswithout9;
            Id = picdownloader.index;

            IsConverge = CheckConverge();



        }   //Конструктор1
        public bool CheckConverge()
        {
            if (!IsFunctional && !IsPow)
            {
                if (IsRandom || IsWithout9) return true;
                if (IsAlter && (PowOfDominator > PowOfNumerator)) return true;
                if (PowOfDominator - PowOfNumerator > 1) return true;
                return false;
            }
            
            else return false;
        }   //Проверка, является ли ряд сходящимся
        public double? CountN(int n)
        {
            if (IsRandom)
            {
                Random T = new Random();
                return (double?)((int)T.Next(0, 1) < 0.5 ? -1.0/n : 1.0/n);
            }
            if (IsWithout9)
            {
                return (double?)(1.0 / (n + n / 9));
            }
            double num = 0;
            double dom = 0;
            string[] Coefs1 = this.NumCoeffs.Split(' ');
            double[] coefs1 = new double[Coefs1.Count()];
            string[] Coefs2 = this.DomCoeffs.Split(' ');
            double[] coefs2 = new double[Coefs2.Count()]; ;

            for (int j = 0; j < Coefs1.Count(); j++)
                coefs1[j] = Convert.ToDouble(Coefs1[j]);
            for (int j = 0; j < Coefs2.Count(); j++)
                coefs2[j] = Convert.ToDouble(Coefs2[j]);
            try
            {
               
              
                  
                    for (int j = 0; j <= PowOfNumerator; j++)
                    {
                        num += coefs1[j] * Math.Pow(n, j);
                    }
                    for (int j = 0; j <= PowOfDominator; j++)
                    {
                        dom += coefs2[j] * Math.Pow(n, j);
                    }
                   
                    
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
            if (IsAlter && (n % 2 != 0))
            {
                return -(num / dom);
            }
            else
            return (num/dom);
        } //Подсчет n-ного члена ряда
        public double? CountFullSum()
        {
            Random T = new Random();
            if (IsWithout9) return 80;
          
            if (!IsConverge) return null;
            double result = 0;
            double num = 0;
            double dom = 0;
            string[] Coefs1 = this.NumCoeffs.Split(' ');
            double[] coefs1 = new double[Coefs1.Count()];
            string[] Coefs2 = this.DomCoeffs.Split(' ');
            double[] coefs2 = new double[Coefs2.Count()]; ;

            for (int j = 0; j < Coefs1.Count(); j++)
                coefs1[j] = Convert.ToDouble(Coefs1[j]);
            for (int j = 0; j < Coefs2.Count(); j++)
                coefs2[j] = Convert.ToDouble(Coefs2[j]);
            try
            {
                int i = 1;
                do
                {
                    num = dom = 0;
                    for (int j = 0; j <= PowOfNumerator; j++)
                    {

                        num += coefs1[j] * Math.Pow(i, j);
                        if (IsRandom) num = (int)T.Next(0, 1) == 0 ? -1 : 1;

                    }
                    for (int j = 0; j <= PowOfDominator; j++)
                    {
                        dom += coefs2[j] * Math.Pow(i, j);
                    }
                    if(IsAlter)
                    result += (Math.Pow(-1,i)*(num / dom));
                    else result += (num / dom);

                    i++;
                } while (Math.Abs(num / dom) > 0.0001);
            }
            catch (NullReferenceException ex)
            {
                return null;
            }
            return result;
        }//Подсчет полной суммы ряда
        public double? CountPartSum(int n)
        {

                double result = 0;
                Random T = new Random();
                double num = 0;
                double dom = 0;
                string[] Coefs1 = this.NumCoeffs.Split(' ');
                double[] coefs1 = new double[Coefs1.Count()];
                string[] Coefs2 = this.DomCoeffs.Split(' ');
                double[] coefs2 = new double[Coefs2.Count()]; ;

                for (int j = 0; j < Coefs1.Count(); j++)
                    coefs1[j] = Convert.ToDouble(Coefs1[j]);
                for (int j = 0; j < Coefs2.Count(); j++)
                    coefs2[j] = Convert.ToDouble(Coefs2[j]);
            try
            {
                for (int i = 1; i <= n; i++)
                {
                    num = dom = 0;
                    for (int j = 0; j <= PowOfNumerator; j++)
                    {

                        num += coefs1[j] * Math.Pow(i, j);
                        if(IsRandom)    num = (int)T.Next(0, 1) == 0 ? -1 : 1;
                    }
                    for (int j = 0; j <= PowOfDominator; j++)
                    {
                        if (IsWithout9)
                            dom += i + i / 9;
                        else
                            dom += coefs2[j] * Math.Pow(i, j);
                    }
                    if (IsAlter)
                        result += (Math.Pow(-1, i) * (num / dom));
                    else result += (num / dom);
                }
            } catch(NullReferenceException ex)
            {
                return null; 
            }
            return result;
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
        public static string Alter = "%28-1%29%5En";
        public static string Pow = "%28x-a%29%5En";
        public static string skobka1 = "%28";
        public static string skobka2 = "%29";
        public static string random = "%7B%5Cfrac%7Bs%7D%7Bn%7D%7D";
        public static string without9 = "%7B%5Cfrac%7B1%7D%7Bn_%7B1%7D%7D%7D";
        

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
        public  static  readonly DependencyProperty usernameProperty = DependencyProperty.Register("username", 
                                                                                                    typeof(string), 
                                                                                                    typeof(USER));
        public ICollection<Generator> Ryads { get; set; }
        public ICollection<Test> Tests { get; set; }

        [Key]
            public string username
        {
            get { return (string)GetValue(usernameProperty); }
            set {SetValue(usernameProperty, value); }
        }

        public string password { get; set; }
       
       
        public int lvl { get; set; }                    //"урвень" пользователя, 1 - новичок, 2 - продвинутый
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



    public class USERContext : DbContext
    {
        public USERContext()
        : base("connect")
        { }

        public DbSet<USER> Users { get; set; }
        public DbSet<Generator> Ryads { get; set; }
        public DbSet<Test> Tests { get; set; }

    }
    public class GeneratorContext : DbContext
    {
        public GeneratorContext()
        : base("connect")
        { }

        public DbSet<USER> Users { get; set; }
        public DbSet<Generator> Ryads { get; set; }

    }
    public class TestContext : DbContext
    {
        public TestContext()
        : base("connect")
        { }
        public DbSet<Test> Tests { get; set; }
        public DbSet<USER> Users { get; set; }
    }
    public interface IRepository<T> where T:  class
    {
        IEnumerable<T> GetAll(string t);
        void Create(T item);
        T Check(string a, string b); 
    }

}
