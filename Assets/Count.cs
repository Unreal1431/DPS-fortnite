using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Assets
{
    public class Count : MonoBehaviour
    {



        private Color DarkRed = new Color(0.5F, 0F, 0F, 1F);
        // создал цвет.

        public InputField[] inputDmg = new InputField[1];
        public InputField[] inputCC = new InputField[1];
        public InputField[] inputCdmg = new InputField[1];
        public InputField[] inputSpeed = new InputField[1];
        public InputField[] inputAmmoSize = new InputField[1];
        public InputField[] inputReloadSpeed = new InputField[1];
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




        // функция расчета привязаная к кнопке "посчитать" .
        public void Result(string new_text)
        {
            Resultat[0].color = Color.black;
            Resultat[1].color = Color.black;


            if (inputDmg[0].text != "")
            {
                Weapon weapon_0 = new Weapon(inputDmg[0], inputCC[0], inputCdmg[0], inputSpeed[0], inputAmmoSize[0], inputReloadSpeed[0]);
                Resultat[0].text = "" + weapon_0.Dps(); ;// Считаем Дпс и присваеваем полученное значению текстовой надписи.
            }
            else
                Resultat[0].text = "Результат: ";

           if (inputDmg[1].text != "")
            {
                Weapon weapon_1 = new Weapon(inputDmg[1], inputCC[1], inputCdmg[1], inputSpeed[1], inputAmmoSize[1], inputReloadSpeed[1]);
                Resultat[1].text = "" + weapon_1.Dps();// Считаем Дпс и присваеваем полученное значению текстовой надписи.
            }
            else
                Resultat[1].text = "Результат: ";



            // если проводилось сравнение, выявляем больший урон и окрашиваем в красный цвет.
              if (Resultat[1].text != "Результат: " && Resultat[0].text != "Результат: ")
               {
                   if (double.Parse(Resultat[0].text) > double.Parse(Resultat[1].text))
                       Resultat[0].color = DarkRed;
                   else
                       Resultat[1].color = DarkRed;
               }



        }
    }
    }

