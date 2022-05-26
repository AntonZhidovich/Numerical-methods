#include <iostream>
#include <iomanip>
#include <cmath>
using namespace std;
double eps = 0.0000001;
double f(double x) {
	return 2 * x + log(2 * x + 3) - 1;
}
double fi(double x) {
	return 0.5 - 0.5 * log(2 * x + 3);
}
int main() {
	setlocale(LC_ALL, "Russian");
	double x0 = 0.1, x1 = x0;
	int count = 0;
	do {
		x0 = x1;
		x1 = fi(x0);
		count++;
	} while (abs(x0 - x1) > eps);
	cout << setprecision(8);
	cout << "Найденный корень: " << x1 << endl;
	cout << "Корень уравнения: " << count << endl;
	cout << "Невязка: " << abs(f(x1)) << endl;
	system("pause");
	return 0;
}