using System;

class Sample
{
    public static void Main()
    {
        Console.Title = "Калькулятор";
        
        Console.WriteLine(
                   "Введите выражение\n" +
                   "Пример ввода: √4*tan0+ctg3=\n" +
                   "Пример ввода: cos3%sin4=\n");
        Console.ForegroundColor = ConsoleColor.White;

        string str = Convert.ToString(Console.ReadLine());

        string[] subs = str.Split(new char[] { '*', '+', '-', '/', 's', 'i', 'n', 'c', 'o', 's', 't', 'g', 'a', '%', '√', '=' }, StringSplitOptions.RemoveEmptyEntries);

        int i = 0; 
        int f = 0; 
        double a = Convert.ToDouble(subs[i]); 
        double b = 0;
        int s = 0;
        double res = 0;
        for (int j = 0; j < str.Length; j++) 
        {
            if (str[j] == '√') 
            {
                subs[i] = Convert.ToString(Math.Sqrt(a));
                f = 0;
            }
            if (f == 0)
            {
                i++;
                if (i < subs.Length)
                {
                    a = Convert.ToDouble(subs[i]);
                }
                f = 1;
            }
            else if (str[j] == '=') 
            {
                i = 0;
                f = 0;
                break;
            }
        }
        for (int j = 0; j < str.Length; j++)
        {
            if (str[j] == 's')
            {
                if (str[j + 1] == 'i')
                {
                    subs[i] = Convert.ToString(Math.Sin(a));
                    f = 0;
                }
            }
            else if (str[j] == 'c')
            {
                if (str[j + 1] == 'o')
                {
                    subs[i] = Convert.ToString(Math.Cos(a));
                }

                else
                {
                    subs[i] = Convert.ToString((1 / Math.Tan(a)));
                }
                f = 0;
            }
            else if (str[j] == 't')
            {
                if (str[j + 1] != 'a')
                {
                    subs[i] = Convert.ToString(Math.Tan(a));
                    f = 0;
                }
            }
            else if (f == 0)
            {
                i++;
                if (i < subs.Length)
                {
                    a = Convert.ToDouble(subs[i]);
                }
                f = 1;
            }
            else if (str[j] == '=')
            {
                i = 0;
                f = 0;
                break;
            }
        }

        for (int j = 0; j < str.Length; j++)
        {
            if (str[j] == '*')
            {
                if (s == 0)
                {
                    a = a * b;
                    subs[i] = Convert.ToString(a);
                    subs[i - 1] = Convert.ToString(a);
                    res = a;
                }
                f = 0;
            }
            else if (str[j] == '+')
            {
                if (s == 1)
                {
                    a = a + b;
                    res = a;
                }
                f = 0;
            }
            else if (str[j] == '-')
            {
                if (s == 1)
                {
                    a = a - b;
                    res = a;
                }
                f = 0;
            }
            else if (str[j] == '/')
            {
                if (s == 0)
                {
                    a = a / b;
                    subs[i] = Convert.ToString(a);

                    subs[i - 1] = Convert.ToString(a);
                    res = a;
                }
                f = 0;
            }

            else if (str[j] == '%')
            {
                if (s == 0)
                {
                    a = a * b / 100;
                    res = a;
                }
                f = 0;
            }
            else if (f == 0)
            {
                i++;
                if (i < subs.Length)
                {
                    b = Convert.ToDouble(subs[i]);
                }
                if (i - 1 < subs.Length)
                {
                    a = Convert.ToDouble(subs[i - 1]);
                }
                f = 1;
            }
            else if (str[j] == '=')
            {
                if (s == 1)
                {
                    if (subs.Length == 1)
                    {
                        res = Convert.ToDouble(subs[0]);
                    }
                    break;
                }
                s = 1;
                j = -1;
                f = 0;
                i = 0;
            }
        }


        Console.WriteLine($"Результат: {res}");
        Console.ReadKey();
    }
}
