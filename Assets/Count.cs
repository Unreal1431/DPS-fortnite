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

        // Массив инпут филдов для статов.

        public InputField[] statField = new InputField[12];
        public Toggle[] statToggle = new Toggle[8];
        public InputField[] modifierField = new InputField[8];
     

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
                        next = Selectable.allSelectables[4];
                        system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
                   
                    // 11 - 1; 4 - 2; обойма - 0;
                }

            }
        }

        
        // функция расчета привязаная к кнопке "посчитать" .
        public void Result(string new_text)
        {
            Resultat[0].color = Color.black;
            Resultat[1].color = Color.black;

            



            if (statField[0].text != "")
            {
                Weapon weapon_0 = new Weapon(statField, statToggle, modifierField);
                Resultat[0].text = "" + weapon_0.Dps(); // Считаем Дпс и присваеваем полученное значению текстовой надписи.
            }
            else
                Resultat[0].text = "Результат: ";
            

            if (statField[6].text != "")
            {
                Weapon weapon_1 = new Weapon(statField, statToggle, modifierField);
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

