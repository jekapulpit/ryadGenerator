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
using WpfMath;

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


}
