#include <iostream>
#include <iomanip>
using namespace std;
double eps = 0.0000001;
double f(double x) {
	return 2 * x + log(2 * x + 3) - 1;
}
double fi(double x) {
	return 0.5 - 0.5 * log(2 * x + 3);
}
double F(double xn) {
	double res = xn * fi(fi(xn)) - fi(xn) * fi(xn);
	res /= fi(fi(xn)) - 2 * fi(xn) + xn;
	return res;
}
int main() {
	setlocale(LC_ALL, "Russian");
	double x0 = 0.1, x1 = x0;
	int count = 0;
	do {
		count++;
		x0 = x1;
		x1 = F(x0);
	} while (abs(x0 - x1) > eps);
	cout << setprecision(13);
	cout << "Найденный корень: " << x1 << endl;
	cout << "Количество итераций: " << count << endl;
	cout << "Невязка: " << abs(f(x1)) << endl;
	system("pause");
}