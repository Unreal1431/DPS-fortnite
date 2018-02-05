using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Count : MonoBehaviour
{




    // создал массив параметров оружия и соответствующих полей ввода.
    public double[] dmg = new double[1];
    public int[] cc = new int[1];
    public double[] cdmg = new double[1];
    public double[] speed = new double[1];
    public int[] AmmoSize = new int[1];
    public double[] ReloadSpeed = new double[1];

    public InputField[] InputDmg = new InputField[1];
    public InputField[] InputCC = new InputField[1];
    public InputField[] InputCdmg = new InputField[1];
    public InputField[] InputSpeed = new InputField[1];
    public InputField[] InputAmmoSize = new InputField[1];
    public InputField[] InputReloadSpeed = new InputField[1];
    // массив текстов с результатом расчета
    public Text[] Resultat = new Text[1];

    // система евента для переключение табом.
    EventSystem system;

    void Start()
    {
        system = EventSystem.current;// EventSystemManager.currentSystem;

    }
    // переключение в инпут филде табом (нужно исправить)
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




    // конвертация и присваивание значения введённого в поле ввода к соответствующим числовым переменным.
    public void Get_dmg()
    {
        for (int i = 0; i < 2; i++)
        {
            if (InputDmg[i].text == "")
                dmg[i] = 0;
            else
                dmg[i] = double.Parse(InputDmg[i].text);
        }
    }
    public void Get_cc()
    {
        for (int i = 0; i < 2; i++)
        {
            if (InputCC[i].text == "")
                cc[i] = 0;
            else
                cc[i] = int.Parse(InputCC[i].text);
        }
    }
    public void Get_Cdmg()
    {
        for (int i = 0; i < 2; i++)
        {
            if (InputCdmg[i].text == "")
                cdmg[i] = 0;
            else // перевод процентов в дробь для удобного умножения атаки.
                cdmg[i] = 1 + (double.Parse(InputCdmg[i].text) / 100);
        }
    }
    public void Get_speed()
    {
        for (int i = 0; i < 2; i++)
        {
            if (InputSpeed[i].text == "")
                speed[i] = 0;
            else
                speed[i] = double.Parse(InputSpeed[i].text);
        }
    }
    public void Get_AmmoSize()
    {
        for (int i = 0; i < 2; i++)
        {
            if (InputAmmoSize[i].text == "")
                AmmoSize[i] = 0;
            else
                AmmoSize[i] = int.Parse(InputAmmoSize[i].text);
        }
    }
    public void Get_ReloadSpeed()
    {
        for (int i = 0; i < 2; i++)
        {

            if (InputReloadSpeed[i].text == "")
                ReloadSpeed[i] = 0;
            else
                ReloadSpeed[i] = double.Parse(InputReloadSpeed[i].text);
        }
    }


    // функция расчета привязаная к кнопке "посчитать" .
    public void Result(string new_text)
    { // проверка на сравнение двух пушек или расчет параметров одной из них, массив переменных для результата дпс.
        int weapon = (dmg[0] == 0) ? 1 : 0;
        int repeat = (dmg[1] == 0) ? 1 : 2;
        double[] mid_dps = { 0, 0 };
        // обновление цвета и текста надписей.
        for (int i = 0; i < 2; i++)
        {
            Resultat[i].color = Color.black;
            Resultat[i].text = "Результат:";
        }
        // провести расчет для одного из оружия по нужному полю ввода, или оба оружия.
        for (; weapon < repeat; weapon++)
        {
            // умножение скорости атаки и обоймы, потому что дробные числа это хуёво, создание переменной с потрачеными пулями.
            speed[weapon] *= 100;
            AmmoSize[weapon] *= 100;
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
                    if (AmmoSize[weapon] < AmmoSpend)
                    {
                        i += ReloadSpeed[weapon];
                        AmmoSpend -= AmmoSize[weapon];
                    }
                    // выполнить количество атак в секунду равное скорости атаки
                    else for (int j = 0; j < speed[weapon]; j++)
                        {// проверка на срабатывание крита, если крит сработал прибавить к урону критический урон, если не сработал - обычный урон.
                            total += (Random.Range(0, 101) <= cc[weapon] && cc[weapon] != 0) ? (dmg[weapon] * cdmg[weapon]) : dmg[weapon];
                            // прибавить кол-во потраченых пуль.
                            AmmoSpend++;
                        }
                }
                // общий урон разделить на 1000 , потому что атаки проводились 1000 секунд, и еще на 100, потому что умножали скорость и обойму.
                dps = (total / 1000) / 100;
                mid_dps[weapon] += dps;
            }
            // вычисляем и выводим на экран средний дпс путём деления на 100, так как проводили атаки 100 раз, обнуляем умножение обоймы и скорости для последующих расчетов.
            mid_dps[weapon] /= 100;
            speed[weapon] /= 100;
            AmmoSize[weapon] /= 100;

            // Присваиваем полученное значению текстовой надписи.
            Resultat[weapon].text = "" + mid_dps[weapon];
        } // если проводилось сравнение, выявляем больший урон и окрашиваем в красный цвет.
        if (mid_dps[0] != 0 && mid_dps[1] != 0)
        {
            if (mid_dps[0] > mid_dps[1])
                Resultat[0].color = Color.red;
            else
                Resultat[1].color = Color.red;
        }
    }
}
