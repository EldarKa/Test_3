using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string A = "";
            string B = "";
            string C = "";
            int selectedIndex = 0;

            ConsoleKey key;

            do
            {
                Console.Clear();
                GetEquation(A, B, C);
                MyParams(A, B, C, selectedIndex);

                key = Console.ReadKey(intercept: true).Key;


                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = Math.Abs((selectedIndex-1) % 3);
                        break;

                    case ConsoleKey.DownArrow:
                        selectedIndex = Math.Abs((selectedIndex + 1) % 3);
                        break;

                    case ConsoleKey.D0:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                        int value = key - ConsoleKey.D0;
                        if (selectedIndex == 0) A += value;
                        else if (selectedIndex == 1) B += value;
                        else if (selectedIndex == 2) C += value;
                        break;

                    case ConsoleKey.Backspace:
                        if (selectedIndex == 0 && A !="")       A = A.Substring(0, A.Length - 1);
                        else if (selectedIndex == 1 && B != "") B = B.Substring(0, B.Length - 1);
                        else if (selectedIndex == 2 && C != "") C = C.Substring(0, C.Length - 1);
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        try
                        {
                            Calc(A, B, C); 
                        }
                        catch (Exception ex) { Console.WriteLine($"Исключение: {ex.Message}"); }
                        Console.WriteLine("Тыкни куда-нибуть чего-нибуть");
                        Console.ReadLine();
                        A = "";
                        B = "";
                        C = "";
                        break;
                }

            } while (key != ConsoleKey.Escape);
        }

        static void GetEquation(string A, string B, string C)
        {
            String equation = "";
            equation += A == "" ? "A*x^2 + " : $"{A}*x^2 + ";
            equation += B == "" ? "B*x + " : $"{B}*x + ";
            equation += C == "" ? "C = 0" : $"{C} = 0";
            Console.WriteLine(equation);
        }

        static void MyParams(string A, string B, string C, int selectedIndex)
        {
            // Отображение параметров с указателем
            Console.WriteLine(selectedIndex == 0 ? "> a: " + A : "  a: " + A);
            Console.WriteLine(selectedIndex == 1 ? "> b: " + B : "  b: " + B);
            Console.WriteLine(selectedIndex == 2 ? "> c: " + C : "  c: " + C);
        }

        static void Calc(string A, string B, string C)
        {
            int a, b, c;
            a = checErrorTipe(A);
            b = checErrorTipe(B);
            c = checErrorTipe(C);
            double discriminant = b * b - 4 * a * c;

            if (a == 0 && b == 0)
            { throw new Exception("Решений нет или уравнение сводится к решению неопределенности 0/0"); }
            if (a == 0 && b == 0 && c == 0)
            { throw new Exception("Решений бесконечномного"); }
            if (discriminant < 0)
            { throw new Exception("Корни уравнения иррациональны"); }

            if (discriminant > 0)
            {
                double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                Console.WriteLine($"Дискриминант: {discriminant}");
                Console.WriteLine($"x1 = {x1}, x2 = {x2}");
            }
            else if (discriminant == 0)
            {
                double x = -b / (2.0 * a);
                Console.WriteLine($"Дискриминант: {discriminant}");
                Console.WriteLine($"x = {x}");
            }
            else
            {
                Console.WriteLine($"Дискриминант: {discriminant}");
                Console.WriteLine("Вещественных корней нет.");
            }
        }

        static int checErrorTipe(string s)
        {
            try
            {
                int namber = Convert.ToInt32(s);
                return namber;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Writemessage(ex);
                return checErrorTipe(s);
            }
            catch (ArgumentException ex)
            {
                Writemessage(ex);
                return checErrorTipe(s);
            }
            catch (InvalidCastException ex)
            {
                Writemessage(ex);
                return checErrorTipe(s);
            }
            catch (Exception ex)
            {
                Writemessage(ex);
                return checErrorTipe(s);
            }
        }

        static void Writemessage(Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
            Console.WriteLine($"Метод: {ex.TargetSite}");
            Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
        }
    }


}
