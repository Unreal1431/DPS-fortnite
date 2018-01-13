using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Count : MonoBehaviour
{




    public double dmg;
    public int cc;
    public double cdmg;
    public double speed;
    public int AmmoSize;
    public double ReloadSpeed;

    public InputField InputDmg;
    public InputField InputCC;
    public InputField InputCdmg;
    public InputField InputSpeed;
    public InputField InputAmmoSize;
    public InputField InputReloadSpeed;

    public Text Resultat;


    EventSystem system;

    void Start()
    {
        system = EventSystem.current;// EventSystemManager.currentSystem;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null)
                    inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
            //else Debug.Log("next nagivation element not found");
            else
            {
                next = Selectable.allSelectables[5];
                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }

        }
    }





    public void Get_dmg()
    {
        dmg = double.Parse(InputDmg.text);
    }
    public void Get_cc()
    {
        cc = int.Parse(InputCC.text);
    }
    public void Get_Cdmg()
    {
        cdmg = double.Parse(InputCdmg.text);
    }
    public void Get_speed()
    {
        speed = double.Parse(InputSpeed.text);
    }
    public void Get_AmmoSize()
    {
        AmmoSize = int.Parse(InputAmmoSize.text);
    }
    public void Get_ReloadSpeed()
    {
        ReloadSpeed = double.Parse(InputReloadSpeed.text);
    }



    public void Result(string new_text)
    {


        // создал  рандомизатор чисел.



        // умножение скорости атаки и обоймы, потому что дробные числа это хуёво.
        speed *= 100;
        AmmoSize *= 100;
        double mid_dps = 0;
        int AmmoSpend = 0;

        // повторить атаки в течении 1000 секунд 100 раз для большей точности.
        for (int y = 0; y < 100; y++)
        {
            double total = 0;
            double dps = 0;
            // атаковать в течении 1000 секунд
            for (double i = 0; i < 1000; i++)
            {
                // если кол-во потраченых пуль превышает обойму - прибавить к времени атаки, время перезарядки с нулевым уроном.
                if (AmmoSize < AmmoSpend)
                {
                    i += ReloadSpeed;
                    AmmoSpend -= AmmoSize;
                }
                // выполнить количество атак в секунду равное скорости атаки
                else for (int j = 0; j < speed; j++)
                    {// проверка на срабатывание крита, если крит сработал прибавить к урону критический урон, если не сработал - обычный урон.
                        total += (Random.Range(0, 101) <= cc && cc != 0) ? (dmg * cdmg) : dmg;
                        // прибавить кол-во потраченых пуль.
                        AmmoSpend++;
                    }
            }
            // общий урон разделить на 1000 , потому что атаки проводились 1000 секунд, и еще на 100, потому что умножали скорость.
            dps = (total / 1000) / 100;
            mid_dps += dps;
        }
        // вычисляем и выводим на экран средний дпс путём деления на 100, так как проводили атаки 100 раз.
        mid_dps /= 100;
        speed /= 100;
        AmmoSize /= 100;


        Resultat.text = "" + mid_dps;



    }
}
