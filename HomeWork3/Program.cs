using System;
using System.Collections.Generic;

// Перечисление для определения уровня серьезности сообщения
enum Severity
{
    Warning,
    Error
}

class QuadraticEquationSolver
{
    static void Main(string[] args)
    {
        // Бесконечный цикл для повторного ввода данных после обработки ошибок
        while (true)
        {
            try
            {
                // Вывод формулы квадратного уравнения
                Console.WriteLine("a * x^2 + b * x + c = 0");

                // Получение значений a, b, c от пользователя
                int a = GetIntInput("a");
                int b = GetIntInput("b");
                int c = GetIntInput("c");

                // Вывод уравнения с введенными значениями
                Console.WriteLine($"{a} * x^2 + {b} * x + {c} = 0");

                // Расчет корней уравнения
                double[] roots = CalculateRoots(a, b, c);

                // Вывод результатов расчета
                if (roots.Length == 1)
                {
                    Console.WriteLine($"x = {roots[0]}");
                }
                else if (roots.Length == 2)
                {
                    Console.WriteLine($"x1 = {roots[0]}, x2 = {roots[1]}");
                }
            }
            catch (FormatException ex)
            {
                // Обработка исключения некорректного ввода данных
                FormatData(ex.Message, Severity.Error, new Dictionary<string, string> { { "Parameter", ex.Data["Parameter"]?.ToString() } });
            }
            catch (NoRealRootsException ex)
            {
                // Обработка исключения отсутствия вещественных корней
                FormatData(ex.Message, Severity.Warning, new Dictionary<string, string>());
            }
            catch (Exception ex)
            {
                // Обработка всех остальных исключений
                FormatData(ex.Message, Severity.Error, new Dictionary<string, string>());
            }
        }
    }

    static int GetIntInput(string parameter)
    {
        Console.Write($"Введите значение {parameter}: ");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int value))
        {
            throw new FormatException($"Ошибка: значение {parameter} должно быть целым числом.")
            {
                Data = { { "Parameter", parameter } }
            };
        }
        return value;
    }

    static double[] CalculateRoots(int a, int b, int c)
    {
        double discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            throw new NoRealRootsException("Вещественных значений не найдено.");
        }

        double sqrtDiscriminant = Math.Sqrt(discriminant);
        double x1 = (-b + sqrtDiscriminant) / (2 * a);
        double x2 = (-b - sqrtDiscriminant) / (2 * a);

        if (discriminant == 0)
        {
            return new double[] { x1 };
        }
        else
        {
            return new double[] { x1, x2 };
        }
    }

    static void FormatData(string message, Severity severity, IDictionary<string, string> data)
    {
        ConsoleColor originalForegroundColor = Console.ForegroundColor;
        ConsoleColor originalBackgroundColor = Console.BackgroundColor;

        if (severity == Severity.Error)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
        }
        else if (severity == Severity.Warning)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
        }

        Console.WriteLine(message);
        foreach (var item in data)
        {
            Console.WriteLine($"{item.Key} = {item.Value}");
        }
        Console.WriteLine(new string('-', 50));

        Console.ForegroundColor = originalForegroundColor;
        Console.BackgroundColor = originalBackgroundColor;
    }
}

class NoRealRootsException : Exception
{
    public NoRealRootsException(string message) : base(message) { }
}

//Метод GetIntInput запрашивает у пользователя целое число и возвращает его и выбрасывает исключение FormatException, 
//если введенное значение не является целым числом.

//Метод CalculateRoots рассчитывает корни квадратного уравнения и выбрасывает исключение NoRealRootsException, 
//если уравнение не имеет вещественных корней.

//Метод FormatData форматирует и выводит сообщение об ошибке или предупреждение и изменяет цвет текста и фона 
//в зависимости от уровня серьезности сообщения.

//Класс NoRealRootsException исключение, которое выбрасывается, если квадратное уравнение не имеет вещественных корней.