using System;

namespace Least_squares_method
{
    class Program
    {
        public const int n = 6;
        static void Main(string[] args)
        {
            double[] x = new double[11];
            for (int i = 0; i <= 10; i++)
                x[i] = 0.25 + 0.1 * i;
            double[,] a = new double[6, 11];
            double[] f = new double[6];
            for (int k = 0; k <= 5; k++)
            {
                for (int i = 0; i <= 10; i++)
                {
                    for (int j = 0; j <= 10; j++)
                    {
                        a[k, i] += Math.Pow(x[j], i + k);
                    }
                }

                for (int j = 0; j <= 10; j++)
                {
                    f[k] += F(x[j]) * Math.Pow(x[j], k);
                }
            }
            double[] c = gaussianAlgorythm(a, f);
            string res = "Коэффициенты Ф(х) (при степенях от 0 до 5):\n";
            foreach (double t in c)
            {
                res += String.Format("{0,10:f12}  ", t);
            }
            double x1 = 19.0 / 61, x2 = 0.8, x3 = 73.0 / 61;
            Console.WriteLine(res);
            Console.WriteLine($"\nФ(x*) = {Phi(c, 5, x1)}");
            Console.WriteLine($"Ф(x**) = {Phi(c, 5, x2)}");
            Console.WriteLine($"Ф(x***) = {Phi(c, 5, x3)}");
            double residualSum = 0;
            for (int i = 0; i <= 10; i++)
            {
                residualSum += Math.Pow(F(x[i]) - Phi(c, 5, x[i]), 2);
            }
            Console.WriteLine($"\nDf = {Math.Sqrt(residualSum)}");
            Console.WriteLine($"r(x*) = {F(x1) - Phi(c, 5, x1)}");
            Console.WriteLine($"r(x**) = {F(x2) - Phi(c, 5, x2)}");
            Console.WriteLine($"r(x***) = {F(x3) - Phi(c, 5, x3)}");
            Console.ReadKey();
        }

        public static double F(double x) => 0.25*Math.Exp(-x)+0.75*Math.Sin(x);
        public static double Phi(double[] c, int m, double x)
        {
            double res = 0;
            for (int i = 0; i <= m; i++)
                res += Math.Pow(x, i) * c[i];
            return res;
        }
        static double[] gaussianAlgorythm(double[,] a, double[] f)
        {
            double[] x = new double[n];
            forwardStroke(a, f);
            x = backwardStroke(a, f);
            return x;
        }

        static void forwardStroke(double[,] a, double[] f)
        {
            int swaps = 0;
            for (int k = 0; k < n; k++)
            {
                swaps += selectMainElement(a, f, k);
                for (int j = k + 1; j < n; j++)
                    a[k, j] /= a[k, k];

                f[k] /= a[k, k];

                a[k, k] = 1.0;

                for (int i = k + 1; i < n; i++)
                {
                    for (int j = k + 1; j < n; j++)
                        a[i, j] = a[i, j] - a[i, k] * a[k, j];

                    f[i] = f[i] - a[i, k] * f[k];

                    a[i, k] = 0;
                }
            }
        }

        static double[] backwardStroke(double[,] a, double[] f)
        {
            double[] x = new double[n];
            x[n - 1] = f[n - 1];
            for (int k = n - 2; k >= 0; k--)
            {
                x[k] = f[k];
                for (int j = k + 1; j < n; j++)
                    x[k] -= a[k, j] * x[j];
            }
            return x;
        }

        static int selectMainElement(double[,] a, double[] f, int k)
        {
            double tMax = Math.Abs(a[k, k]);
            int maxInd = k;
            for (int i = k + 1; i < n; i++)
            {
                if (Math.Abs(a[i, k]) > tMax)
                {
                    maxInd = i;
                    tMax = Math.Abs(a[i, k]);
                }
            }

            if (maxInd != k)
            {
                swapLines(a, k, maxInd, f);
                return 1;
            }
            return 0;
        }
        static void swapLines(double[,] a, int k, int i, double[] f = null)
        {
            double temp;
            for (int j = 0; j < n; j++)
            {
                temp = a[k, j];
                a[k, j] = a[i, j];
                a[i, j] = temp;
            }
            if (f != null)
            {
                temp = f[k];
                f[k] = f[i];
                f[i] = temp;
            }
        }
    }

}
