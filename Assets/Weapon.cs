
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Assets
{
    public class Weapon
    {
        double dmg;
        int cc;
        double cdmg;
        double speed;
        int ammosize;
        double reloadspeed;
        double dps;

        static int count = 0;// начальный счетчик для итерации статов из массива.
        static int modCount = 0;


        public override string ToString()
        {
            return "" + dps;
        }



        public Weapon(InputField[] stat, Toggle[] toggle, InputField[] modifier) // конструктор через массивы инпутов с модификаторами
        {
            // увеличиваем начальный счетчик если считаем только оружие 2
            if (stat[0].text == "" && count == 0)
            { count = 6; modCount = 5; }

            dmg = double.Parse(stat[count++].text);
            if (toggle[modCount].isOn && modifier[modCount].text != "") dmg *= (1 + (double.Parse(modifier[modCount].text)) / 100); // прибавляем процент
            modCount++;

            cc = (stat[count++].text == "") ? 0 : int.Parse(stat[count - 1].text);
            if (toggle[modCount].isOn && modifier[modCount].text != "") { double temp = cc; temp += (75 * double.Parse(modifier[modCount].text) / (50 + double.Parse(modifier[modCount].text)) + 0.5); cc = (int)temp; }
            

            cdmg = (stat[count++].text == "") ? 0 : 1 + (double.Parse(stat[count - 1].text) / 100);//********** перевод процентов в дробь для удобного умножения атаки.********** 

            speed = (stat[count++].text == "") ? 0 : double.Parse(stat[count - 1].text);
            if (toggle[modCount].isOn && modifier[modCount].text != "") speed *= (1 + (double.Parse(modifier[modCount].text)) / 100); // прибавляем процент
            modCount++;

            ammosize = (stat[count++].text == "") ? 0 : int.Parse(stat[count - 1].text);
            if (toggle[modCount].isOn && modifier[modCount].text != "") { double temp = ammosize; temp *= (1 + double.Parse(modifier[modCount].text) / 100) + 0.5; ammosize = (int)temp; } // Прибавляем процент и конвертируем в Int
            modCount++;
           

            reloadspeed = (stat[count++].text == "") ? 0 : double.Parse(stat[count - 1].text);
            if (toggle[modCount].isOn && modifier[modCount].text != "") reloadspeed -= reloadspeed / 100 * double.Parse(modifier[modCount].text); // отнимаем процент
            modCount++;

            // сбрасываем счетчик если он в конце массива или если считаем только оружие 1
            if (stat[6].text == "" || count == stat.Length)
            { count = 0; modCount = 0; }

            Dps();

        }



        public void Dps()
        {

            
                double mid_dps = 0;
                int ammospend = 0;

                // повторить атаки в течении 1000 секунд 100 раз для большей точности.
                for (int y = 0; y < 100; y++)
                {
                    double total = 0;
                    double dps = 0;
                    // атаковать в течении 1000 секунд
                    for (double i = 0; i < 1000; i++)
                    {
                    // если кол-во потраченых пуль превышает обойму - прибавить к времени атаки, время перезарядки с нулевым уроном.
                        if (ammosize < ammospend)
                        {
                            i += reloadspeed;
                            ammospend -= ammosize;
                        }
                        // выполнить количество атак в секунду равное скорости атаки
                        for (int j = 0; j < speed; j++) // после того как убрал иначе прога разок зависла, не знаю в этом ли причина.
                            {// проверка на срабатывание крита, если крит сработал прибавить к урону критический урон, если не сработал - обычный урон.
                                total += (Random.Range(0, 101) <= cc && cc != 0) ? (dmg * cdmg) : dmg;
                                // прибавить кол-во потраченых пуль.
                                ammospend++;
                            }
                    }
                    // общий урон разделить на 1000 , потому что атаки проводились 1000 секунд, и еще на 100, потому что умножали скорость и обойму.
                    dps = (total / 1000);
                    mid_dps += dps;
                }
                // вычисляем дпс путём деления на 100, так как проводили атаки 100 раз, и присваеваем переменной.
                this.dps = mid_dps / 100;
            


        }


    }
}
