using System;
using static System.Math;
namespace Newton_s_method_for_systems
{
    class Program
    {
        const int n = 2;
        const double eps = 0.0000001;
        static void Main(string[] args)
        {
            double[] dx = new double[n];
            double[] f = new double[n];
            double[,] a = new double[n, n];
            double x0 = 2.9, y0 = 1.3;
            double x1 = x0, y1 = y0;
            int count = 0;
            do
            {
                count++;
                x0 = x1; y0 = y1;
                a[0, 0] = f11(x0, y0);
                a[0, 1] = f12(x0, y0);
                a[1, 0] = f21(x0, y0);
                a[1, 1] = f22(x0, y0);
                f[0] = -f1(x0, y0);
                f[1] = -f2(x0, y0);
                dx = gaussianAlgorythm(a, f);
                x1 = x0 + dx[0];
                y1 = y0 + dx[1];
            }
            while (norm(Abs(x1-x0), Abs(y1-y0)) >= eps);
            Console.WriteLine($"Количество итераций: {count}");
            Console.WriteLine($"Найденное решение: \nx = {x1}\ny = {y1}");
            double r = norm(f1(x1, y1), f2(x1, y1));
            Console.WriteLine($"Невязка решения: {r}");
            Console.ReadKey();
        }
        static double f1(double x, double y) => x * x + y * y / 9 - 9;
        static double f2(double x, double y) => x * y - 4;
        static double f11(double x, double y) => 2 * x;
        static double f12(double x, double y) => 2 / 9 * y;
        static double f21(double x, double y) => y;
        static double f22(double x, double y) => x;
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

        static double norm(double x, double y) => Abs(Max(x, y));
    }
}
