using System;
namespace Lagrange_polynomial
{
    class Program
    {
        public const int n = 11;
        public const double M = 0.9214;
        static void Main(string[] args)
        {
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
                x[i] = 0.25 + 0.1 * i;
            Transformation(x);
            Array.Sort(x);
            double x1 = 19.0 / 61, x2 = 0.8, x3 = 73.0 / 61;
            Console.WriteLine($"Pn(x*) = {Pn(x1, x)}");
            Console.WriteLine($"Pn(x**) = {Pn(x2, x)}");
            Console.WriteLine($"Pn(x***) = {Pn(x3, x)}");
            //Console.WriteLine(Cresidual(x));
            Console.WriteLine("Остатки интерполирования:");
            Console.WriteLine($"r(x*) = {residual(x1, x)}");
            Console.WriteLine($"r(x**) = {residual(x2, x)}");
            Console.WriteLine($"r(x***) = {residual(x3, x)}");
            Console.WriteLine("Истинные невязки:");
            Console.WriteLine($"r(x*) = {Math.Abs(F(x1) - Pn(x1, x))}");
            Console.WriteLine($"r(x**) = {Math.Abs(F(x2) - Pn(x2, x))}");
            Console.WriteLine($"r(x***) = {Math.Abs(F(x3) - Pn(x3, x))}");
            Console.ReadLine();

        }
        
        public static double residual(double x, double[] xk)
        {
            double res = M;
            for(int i = 0; i < n; i++)
            {
                res *= Math.Abs(x - xk[i]);
            }
            return res / Factorial(n);
        }
        public static int Factorial(int n) => (n == 1) ? 1 : n * Factorial(n - 1);
        public static double F(double x) => 0.25 * Math.Exp(-x) + 0.75 * Math.Sin(x);
        public static double Pn(double x, double[] xk)
        {
            double res = 0;
            for(int i = 0; i < n; i++)
            {
                double currMul = 1;
                for(int k = 0; k < n; k++)
                {
                    if (k == i)
                        continue;
                    currMul *= (x - xk[k]);
                    currMul /= (xk[i] - xk[k]);
                }
                res += currMul * F(xk[i]);
            }
            return res;
        }
        public static double Cresidual(double[] x, double a = 0.25, double b = 1.25) 
            => M * Math.Pow(b - a, n) / (Factorial(n) * Math.Pow(2, 2*(n-1)+1));
        public static void Transformation(double[] x, double a = 0.25, double b = 1.25)
        {
            for(int k = 0; k < n; k++)
            {
                x[k] = (a + b) / 2 + ((b - a) / 2) * Math.Cos((2 * k + 1) * Math.PI / (2 * (n + 1)));
            }
        }

    }
}
