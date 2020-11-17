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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public class Fraction
        {
            public Fraction(int numerator, int denominator)
            {
                Numerator = numerator;
                Denominator = denominator;
            }

            public int Numerator { get; set; }
            public int Denominator { get; set; }

            public static string ToStringNum(Fraction a)//cтроковое представление числителя
            {
                return a.Numerator.ToString();
            }

            public static string ToStringDenom(Fraction a)//cтроковое представление знаменателя
            {
                return a.Denominator.ToString();
            }

            public static Fraction Plus (Fraction a, Fraction b)//сложение дробей
            {
                Fraction t = new Fraction(1, 1);
                t.Numerator = (a.Numerator * b.Denominator + a.Denominator * b.Numerator);
                t.Denominator = a.Denominator * b.Denominator;
                Fraction.SetFormat(t);
                return t;

            }
            public static Fraction Minus (Fraction a, Fraction b)//вычитание дробей
            {
                Fraction t = new Fraction(1, 1);
                t.Numerator = (a.Numerator * b.Denominator - a.Denominator * b.Numerator);
                t.Denominator = a.Denominator * b.Denominator;
                Fraction.SetFormat(t);
                return t;

            }
            public static Fraction Mult (Fraction a, Fraction b)//умножение дробей
            {
                Fraction t = new Fraction(1, 1);
                t.Numerator = (a.Numerator * b.Numerator);
                t.Denominator = a.Denominator * b.Denominator;
                Fraction.SetFormat(t);
                return t;

            }
            public static Fraction Div (Fraction a, Fraction b)//деление дробей
            {
                Fraction t = new Fraction(1, 1);
                t.Numerator = (a.Numerator / b.Numerator);
                t.Denominator = a.Denominator / b.Denominator;
                Fraction.SetFormat(t);
                return t;
            }

            //Сокращение дроби
            public static Fraction SetFormat(Fraction a)
            {

                int max = 0;

                //выбираем что больше числитель или знаменатель
                if (a.Numerator > a.Denominator)
                    max = Math.Abs(a.Denominator);
                else
                    max = Math.Abs(a.Numerator);

                for (int i = max; i >= 2; i--)
                {             
                    if ((a.Numerator % i == 0) & (a.Denominator % i == 0))
                    {
                        a.Numerator = a.Numerator / i;
                        a.Denominator = a.Denominator / i;
                    }
                }

                //если в знаменателе минус, поднимаем его наверх
                if ((a.Denominator < 0))
                {
                    a.Numerator = -1 * (a.Numerator);
                    a.Denominator = Math.Abs(a.Denominator);
                }
                return (a);
            }
        }

        Fraction a = new Fraction(1,1);
        Fraction b = new Fraction(1,1);
        Fraction c;

        private void divident1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (divident1.Text != "")
                {
                    a.Numerator = int.Parse(divident1.Text);
                }
            }
            catch { MessageBox.Show("Допустимы только целые числа!"); divident1.Text = ""; }
        }

        private void divident2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (divident2.Text != "")
                {
                    b.Numerator = int.Parse(divident2.Text);
                }
            }
            catch { MessageBox.Show("Допустимы только целые числа!"); divident1.Text = ""; }
        }

        private void divider1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try 
            { 
                if (divider1.Text != "")
                {
                    if (int.Parse(divider1.Text) == 0) 
                    { 
                        MessageBox.Show("Делить на ноль запрещено, введите другое число!"); 
                        divider1.Text = ""; 
                    }
                    else { a.Denominator = int.Parse(divider1.Text); }
                }
            }
            catch { MessageBox.Show("Допустимы только целые числа!"); divider1.Text = ""; }
        }

        private void divider2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (divider2.Text != "")
                {
                    if (int.Parse(divider2.Text) == 0)
                    {
                        MessageBox.Show("Делить на ноль запрещено, введите другое число!");
                        divider2.Text = "";
                    }
                    else { b.Denominator = int.Parse(divider2.Text); }
                }
            }
            catch { MessageBox.Show("Допустимы только целые числа!"); divider2.Text = ""; }
        }

        private void sign_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sign.Text != "")
            {
                if ((sign.Text != "+") && (sign.Text != "-") && (sign.Text != "*") && (sign.Text != "/"))
                {
                    MessageBox.Show("Допустимы только знаки + - * /");
                    sign.Text = "";
                }
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if ((divident1.Text == "")||(divident2.Text == "")||(divider1.Text == "")||(divider2.Text == "")||(sign.Text == "")) 
            { 
                MessageBox.Show("Введите достаточное количество данных!"); 
            }
            else
            {
                if (sign.Text == "+") { MessageBox.Show($"{a.Numerator} {a.Denominator} {b.Numerator} {b.Denominator}"); c = Fraction.Plus(a, b); }
                else if (sign.Text == "-") { c = Fraction.Minus(a, b); }
                else if (sign.Text == "*") { c = Fraction.Mult(a, b); }
                else if (sign.Text == "/") { c = Fraction.Div(a, b); }
                ans1.Text = Fraction.ToStringNum(c);
                ans2.Text = Fraction.ToStringDenom(c);

            }       
        }
    }
}
