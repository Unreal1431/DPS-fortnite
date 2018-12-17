using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Нужно всё менять к херам, я всё похерил - считает не то что нужно. Пока что не работает.
public class Launcher  {

    double dmg;
    double cc;
    double cdmg;
    int drability;
    double reloadspeed;
    int cost;
    double impact;

    double speed = 0.75;
    double time;
    double dps = 0;
    double dpo = 0;

    public double DPS { get { return dps; }}
    public double Impact { get { return impact; } }


    static int count = 0;// начальный счетчик для итерации статов из массива.
    static int modCount = 0;


    public override string ToString()
    {
        if (dps != 0)
            return "" + dps;
        else 
            return "" + dpo;
    }



    public Launcher(InputField[] stat, Toggle[] toggle, InputField[] modifier) // конструктор через массивы инпутов с модификаторами
    {
        // увеличиваем начальный счетчик если считаем только оружие 2
        if (stat[0].text == "" && count == 0 && stat[6].readOnly)
        { count = stat.Length / 2; modCount = toggle.Length / 2; }

        dmg = (stat[count++].text == "" || stat[count - 1].readOnly) ? 0 : double.Parse(stat[count - 1].text);
        if (toggle[modCount].isOn && modifier[modCount].text != "") dmg *= (1 + (double.Parse(modifier[modCount].text)) / 100); // прибавляем процент
        modCount++;

        cc = (stat[count++].text == "") ? 0 : double.Parse(stat[count - 1].text) / 100; // переводим в дробь
        if (toggle[modCount].isOn && modifier[modCount].text != "") { cc += ((75 * double.Parse(modifier[modCount].text) / (50 + double.Parse(modifier[modCount].text)) + 0.5)) / 100; } // прибавляем модификатор уменьшающийся с увеличением значения
        modCount++;

        cdmg = (stat[count++].text == "") ? 0 : (double.Parse(stat[count - 1].text) / 100);//********** перевод процентов в дробь для удобного умножения атаки.********** 

        drability = (stat[count++].text == "") ? 0 : int.Parse(stat[count - 1].text);
        if (toggle[modCount].isOn && modifier[modCount].text != "") { drability *= (int)((1 + double.Parse(modifier[modCount].text) / 100) + 0.5); }
        modCount++;

        reloadspeed = (stat[count++].text == "") ? 0 : double.Parse(stat[count - 1].text);
        if (toggle[modCount].isOn && modifier[modCount].text != "") reloadspeed /= 1 + double.Parse(modifier[modCount].text) / 100; // отнимаем процент
        modCount++;

        cost = (stat[count++].text == "" || stat[count-1].readOnly) ? 0 : int.Parse(stat[count - 1].text);

        impact = (stat[count++].text == "" || stat[count - 1].readOnly) ? 0 : double.Parse(stat[count - 1].text);

        time = (speed + reloadspeed);

        // сбрасываем счетчик если он в конце массива или если считаем только оружие 1
        if ((stat[stat.Length / 2].text == "" && stat[stat.Length - 1].text == "") || count == stat.Length)
        { count = 0; modCount = 0; }

        Dps();
        //Forgotten_Dps();
    }

    public void Dps()
    {
        if (cost == 0 && impact == 0)
        {// урон в секунду
            dps = dmg;
            dps /= time;
            dps += dps * cc * cdmg;
        }
       else if (cost > 0 && impact == 0)    
        {// урон за руду
            dpo = dmg * drability;
            dpo /= cost;
            dpo += dpo * cc * cdmg;
        }

        else if (cost == 0 && impact > 0)
        {// отброс в секунду
            dps = impact;
            dps /= time;
        }

        else if (cost > 0 && impact > 0)
        {// отброс за руду
            dpo = impact * drability;
            dpo /= cost;
        }




    }

    public void Forgotten_Dps()
    {

        cdmg += 1;
        cc *= 100;

        // повторить атаки 100 раз для большей точности.
        for (int y = 0; y < 100; y++)
        {
            double total = 0;

            // атаковать в течении по количеству прочности оружия.
            for(int i = 0; i < 100; i++)
            {
                for (double j = 0; j < drability; j++)
                {
                    {// проверка на срабатывание крита, если крит сработал прибавить к урону критический урон, если не сработал - обычный урон.
                        total += (Random.Range(1, 101) <= cc && cc != 0) ? (dmg * cdmg) : dmg;
                    }
                }
            }
            // общий урон разделить на прочность , потому что атаки проводились по кол-ву прочности.
            dps += total / 100; // делим на 100 потому что было 100 базук.        
        }
        // вычисляем дпс путём деления на 100, так как проводили атаки 100 раз, и присваеваем переменной.
        dps /= 100;
        time = (speed + reloadspeed) * drability; // выясняем время затраченное на использование 1-ной базуки.


            
    }
}
