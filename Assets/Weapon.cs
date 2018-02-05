
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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


        /*
                if (InputAmmoSize[i].text == "")
                    AmmoSize[i] = 0;
                else
                    AmmoSize[i] = int.Parse(InputAmmoSize[i].text);
                    ПРОВЕРКА НА ПУСТЫЕ ПОЛЯ
             */

        public Weapon(InputField dmg, InputField cc, InputField cdmg, InputField speed, InputField ammosize, InputField reloadspeed)
        {

            this.dmg = double.Parse(dmg.text);
            this.cc = int.Parse(cc.text);
            this.cdmg = 1 + (double.Parse(cdmg.text) / 100);// перевод процентов в дробь для удобного умножения атаки.
            this.speed = double.Parse(speed.text);
            this.ammosize = int.Parse(ammosize.text);
            this.reloadspeed = double.Parse(reloadspeed.text);
        }
      




        

        public double Dps()
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
                        else for (int j = 0; j < speed; j++)
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
                return dps;
            


        }


    }
}
