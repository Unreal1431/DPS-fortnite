using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication12
{
    class Program
    {
        static void Main(string[] args)
        {
            // создал стрингу которая читает файл, и рандомизатор чисел.
            StreamReader objReader = new StreamReader("e:\\dps.txt");
            string dpsLine = ""; ArrayList arrText = new ArrayList();
            Random rnd = new Random();

            // Описание считаных параметров с файла в консоль
            dpsLine = objReader.ReadLine();
            double dmg = double.Parse(dpsLine);
            Console.WriteLine("Урон оружия: {0}", dmg);

            dpsLine = objReader.ReadLine();
            int cc = int.Parse(dpsLine);
            Console.WriteLine("Шанс крита: {0}", cc);

            dpsLine = objReader.ReadLine();
            double cdmg = double.Parse(dpsLine);
            Console.WriteLine("Критический урон: {0}", cdmg);

            dpsLine = objReader.ReadLine();
            double speed = double.Parse(dpsLine);
            Console.WriteLine("Скорость атаки: {0} \n", speed);

            // умножение скорости атаки, потому что дробные числа это хуёво.
            speed = speed * 100;
            double mid_dps = 0;

            for (int y = 0; y < 100; y++)
            {
                double total = 0;
                double dps = 0;
                // атаковать в течении 100 секунд
                for (int i = 0; i < 100; i++)
                    // выполнить количество атак в секунду равное скорости атаки
                    for (int j = 0; j < speed; j++)
                    {// проверка на срабатывание крита, если крит сработал прибавить к урону критический урон
                        if (rnd.Next(0, 101) <= cc && cc != 0)
                            total = total + (dmg * cdmg);
                        // если крит не сработал - обычный урон который плюсуется в общий урон.
                        else
                            total = total + dmg;
                    }
                // общий урон разделить на 100 , потому что атаки проводились 100 секунд, и еще на 100, потому что умножали скорость.
                dps = (total / 100) / 100;
                mid_dps = mid_dps + dps;

            }
            mid_dps = mid_dps / 100;
            Console.WriteLine("ДПС = {0}",mid_dps);





        }
    }
}