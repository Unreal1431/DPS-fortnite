using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Trap {

    double dmg;
    int delay;
    double reloadspeed;
    int cc;
    double cdmg;
    int affliction = 1;
    
    double dps;

    static int count = 0;// начальный счетчик для итерации статов из массива.
    static int modCount = 0;


    public override string ToString()
    {
        return "" + dps;
    }



    public Trap (InputField[] stat, Toggle[] toggle, InputField[] modifier) // конструктор через массивы инпутов с модификаторами
    {
        // увеличиваем начальный счетчик если считаем только ловушку 2
        if (stat[0].text == "" && count == 0)
        { count = stat.Length / 2; modCount = toggle.Length / 2; }

        dmg = double.Parse(stat[count++].text);
        if (toggle[modCount].isOn && modifier[modCount].text != "") dmg *= (1 + (double.Parse(modifier[modCount].text)) / 100); // прибавляем процент
        modCount++;

        delay = int.Parse(stat[count++].text);

        reloadspeed = (stat[count++].text == "") ? 0 : double.Parse(stat[count - 1].text);
        if (toggle[modCount].isOn && modifier[modCount].text != "") reloadspeed /= 1 + double.Parse(modifier[modCount].text) / 100; // отнимаем процент
        modCount++;

        cc = (stat[count++].text == "") ? 0 : int.Parse(stat[count - 1].text);
        if (toggle[modCount].isOn && modifier[modCount].text != "") { cc += (int)((75 * double.Parse(modifier[modCount].text) / (50 + double.Parse(modifier[modCount].text)) + 0.5)); }
        modCount++;

        cdmg = (stat[count++].text == "") ? 0 : 1 + (double.Parse(stat[count - 1].text) / 100);//********** перевод процентов в дробь для удобного умножения атаки.********** 
        modCount++;


        // сбрасываем счетчик если он в конце массива или если считаем только ловушку 1
        if (stat[stat.Length / 2].text == "" || count == stat.Length)
        { count = 0; modCount = 0; }

        Dps();

    }



    public void Dps()
    {


        // повторить атаки в течении 1000 секунд 100 раз для большей точности.
        for (int y = 0; y < 100; y++)
        {
            double total = 0;
            // атаковать в течении 1000 секунд
            for (double i = 0; i < 1000; i++)
            {
               
                // выполнить количество атак в секунду равное в зависимости от типа ловушки
                for (int j = 0; j < affliction; j++) 
                {// проверка на срабатывание крита, если крит сработал прибавить к урону критический урон, если не сработал - обычный урон.
                    total += (Random.Range(0, 101) <= cc && cc != 0) ? (dmg * cdmg) : dmg;
                }
                i += delay + reloadspeed; // перезарядка ловушки и задержка.
            }
            // общий урон разделить на 1000 , потому что атаки проводились 1000 секунд.
            dps += (total / 1000);
        }
        // вычисляем дпс путём деления на 100, так как проводили атаки 100 раз, и присваеваем переменной.
        dps /= 100;



    }

}
