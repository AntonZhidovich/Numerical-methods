using System;
using static System.Math;
namespace Gaussian_Seidel_method
{
    class Program
    {
        const double eps = 0.0000001;
        const double t1 = 0.09;
        const double t2 = 0.09;
        static void Main(string[] args)
        {
            double x, y;
            double x0 = 2.6, y0 = 1.6;
            double x1 = x0, y1 = y0;
            int count = 0;
            do
            {
                count++;
                x = x1; y = y1;
                do
                {
                    x0 = x1;
                    x1 = fi1(x0, y1);
                }
                while (Abs(x1 - x0) >= eps);

                do
                {
                    y0 = y1;
                    y1 = fi2(x1, y0);
                }
                while (Abs(y1 - y0) >= eps);
            } while (norm(Abs(x - x1), Abs(y - y1)) >= eps);
            Console.WriteLine($"Количество итераций: {count}");
            Console.WriteLine($"Найденное решение: \nx = {x1}\ny = {y1}");
            double r = norm(f1(x1, y1), f2(x1, y1));
            Console.WriteLine($"Невязка решения: {r}");
            Console.ReadKey();
        }
        static double f1(double x, double y) => x * x + y * y / 9 - 9;
        static double f2(double x, double y) => x * y - 4;
        static double fi1(double x, double y) => x - t1 * f1(x, y);
        static double fi2(double x, double y) => y - t2 * f2(x, y);
        static double norm(double x, double y) => Abs(Max(x, y));
    }
}
