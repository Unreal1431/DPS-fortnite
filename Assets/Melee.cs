using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Пока считает только конструктора, позже доработать.
public class Melee {

    readonly double dmg;
    double cc;
    double cdmg;
    readonly double speed;
    readonly double skill;
    readonly int persona;

    double dps;
    double total;
    readonly double fractionalSpeed = 1;

    static int count = 0;// начальный счетчик для итерации статов из массива.
    static int modCount = 0;


    public override string ToString()
    {
        return "" + dps;
    }



    public Melee(InputField[] stat, Toggle[] toggle, InputField[] modifier, int persona) // конструктор через массивы инпутов с модификаторами
    {
        // увеличиваем начальный счетчик если считаем только оружие 2
        if (stat[0].text == "" && count == 0)
        { count = stat.Length / 2; modCount = toggle.Length / 2; }

        dmg = double.Parse(stat[count++].text);
        if (toggle[modCount].isOn && modifier[modCount].text != "") dmg *= (1 + (double.Parse(modifier[modCount].text)) / 100); // прибавляем процент
        modCount++;

        cc = (stat[count++].text == "") ? 0 : double.Parse(stat[count - 1].text) / 100;
        if (toggle[modCount].isOn && modifier[modCount].text != "") { cc += ((75 * double.Parse(modifier[modCount].text) / (50 + double.Parse(modifier[modCount].text)) + 0.5)) / 100; }
        modCount++;

        cdmg = (stat[count++].text == "") ? 0 : (double.Parse(stat[count - 1].text) / 100);//********** перевод процентов в дробь для удобного умножения атаки.********** 

        speed = (stat[count++].text == "") ? 0 : double.Parse(stat[count - 1].text);
        if (toggle[modCount].isOn && modifier[modCount].text != "") speed *= (1 + (double.Parse(modifier[modCount].text)) / 100); // прибавляем процент
        modCount++;

        skill = (stat[count++].text == "") ? 0 : double.Parse(stat[count - 1].text);

        this.persona = persona;

        if (speed % 1 != 0)
        { fractionalSpeed = speed % 1; } // приравниваем переменной дробную часть от скорости.

        // сбрасываем счетчик если он в конце массива или если считаем только оружие 1
        if (stat[stat.Length / 2].text == "" || count == stat.Length)
        { count = 0; modCount = 0; }

        Dps();
        //Forgotten_Dps();

    }

    public void Dps()
    {
        dps = dmg / speed;
        dps += dps * cc * cdmg;
        if (persona != 2)           
            dps += (skill * cc / speed);
        else
            dps += (skill / 5 / speed);
    }

    public void Forgotten_Dps()
    {
        cdmg += 1;
        cc *= 100;

        // повторить атаки в течении 1000 секунд 100 раз для большей точности.
        for (int y = 0; y < 100; y++)
        {
            total = 0;

            // атаковать в течении 1000 секунд
            if (persona == 2)
                AttackNinja();
            else
                AttackConstructor();
            // общий урон разделить на 1000 , потому что атаки проводились 1000 секунд.
            dps += (total / 1000);
        }
        // вычисляем дпс путём деления на 100, так как проводили атаки 100 раз, и присваеваем переменной.
        dps /= 100;
    }



    private void AttackConstructor()
    {
        for (double i = 0; i < 1000; i++)
        {
            for (int j = 0; j < speed - 1; j++)// выполнить количество атак в секунду равное скорости атаки
            {
                total += (Random.Range(1, 101) <= cc && cc != 0) ? (dmg * cdmg + skill) : dmg; // проверка на срабатывание крита, если крит сработал прибавить к урону критический урон, если не сработал - обычный урон.
            }

            total += (Random.Range(1, 101) <= cc && cc != 0) ? (dmg * cdmg + skill) * fractionalSpeed : dmg * fractionalSpeed;

        }
    }

    private void AttackNinja()
    {
        double ninjsStack = 0;
        for (double i = 0; i < 1000; i++)
        {
            while (ninjsStack >= 5)
            {
                total += skill;
                ninjsStack -= 5;
            }
            for (int j = 0; j < speed - 1; j++)// выполнить количество атак в секунду равное скорости атаки
                {
                total += (Random.Range(1, 101) <= cc && cc != 0) ? (dmg * cdmg) : dmg; // проверка на срабатывание крита, если крит сработал прибавить к урону критический урон, если не сработал - обычный урон.
                ninjsStack++;
                }

            total += (Random.Range(1, 101) <= cc && cc != 0) ? (dmg * cdmg) * fractionalSpeed : dmg * fractionalSpeed;
            ninjsStack += fractionalSpeed;
        }
    }


}
