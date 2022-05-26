using System;
using static System.Linq.Enumerable;
namespace Equidistant_nodes
{
    class Program
    {
        public const int n = 11;
        public const int k = 3;
        public static double t;
        public const double h = 0.1;
        public const double M = 0.5363;
        static void Main(string[] args)
        {
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
                x[i] = 0.25 + 0.1 * i;
            double[,] y = differenceMatr(x);
            double x1 = (19.0 / 61);
            t  = (x1 - x[0]) / h;
            Console.WriteLine($"P3(x*) = {P(y)}");
            Console.WriteLine("Остаток интерполирования:");
            Console.WriteLine($"rn(x*) = {residual(x1, x)}");
            Console.WriteLine("Истинная погрешность:");
            Console.WriteLine($"r(x*) = {Math.Abs(F(x1) - P(y))}");
            Console.ReadLine();
        }
        static double[,] differenceMatr(double[] x)
        {
            double[,] y = new double[k + 1, k + 1];
            foreach (var i in Range(0, k + 1))
                y[i, 0] = F(x[i]);
            foreach (var j in Range(1, k))
                foreach (var i in Range(0, k + 1 - j))
                    y[i, j] = y[i + 1, j - 1] - y[i, j - 1];
            return y;
        }
        public static double P(double[,] y)
        {
            double res = 0;
            foreach (var i in Range(0, k + 1))
            {
                double temp = 1;
                foreach (var j in Range(0, i))
                    temp *= t - j;
                res += y[0, i] * temp/Factorial(i);
            }
            return res;

        }
        public static double F(double x) => 0.25 * Math.Exp(-x) + 0.75 * Math.Sin(x);
        public static int Factorial(int n) => (n <= 1) ? 1 : n * Factorial(n - 1);
        public static double residual(double x, double[] xk)
        {
            double res = M*Math.Pow(h, k+1);
            double temp = 1;
            foreach (var i in Range(0, k + 1))
                temp *= t - i;
            res *= temp / Factorial(k + 1);
            return Math.Abs(res);
        }
    }
}
