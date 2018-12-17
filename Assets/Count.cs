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
        public Toggle[] statToggle = new Toggle[10];
        public InputField[] modifierField = new InputField[10];
        public Toggle[] tabToggle = new Toggle[3];
        int persona_1;
        int persona_2;

        public int Persona_1
        {
            set { persona_1 = value; }
        }

        public int Persona_2
        {
            set { persona_2 = value; }
        }

        // массив текстов с результатом расчета
        public Text[] Resultat = new Text[2];
        public Text[] Difference = new Text[2];
        // система евента для переключения табом.
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
                        next = Selectable.allSelectables[1];
                        system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
                   
                    // 11 - 1; 4 - 2; обойма - 0;
                }

            }
        }

        public void Copy()
        {
            for (int i = 0; i < statField.Length / 2; i++)
                statField[i + statField.Length / 2].text = statField[i].text;

            for (int i = 0; i < modifierField.Length / 2; i++)
                modifierField[i + modifierField.Length / 2].text = modifierField[i].text;

            //for (int i = 0; i < statToggle.Length - 5; i++)
            //{
            //    if (statToggle[i].isOn)
            //        statToggle[i + 5].isOn;
            //}
        }
        
        // функция расчета привязаная к кнопке "посчитать" .
        public void Result()
        {
            for (int i = 0; i < 2; i++)
            {
                Resultat[i].color = Color.black;
                Difference[i].text = "";
            } 
            

            if (tabToggle[0].isOn)
            { CreateRanged(); }
            else if (tabToggle[1].isOn)
            { CreateTrap(); }
            else if (tabToggle[2].isOn)
            { CreateLauncher();}
            else if (tabToggle[3].isOn)
            { CreateMelee(); }

            // если проводилось сравнение, выявляем больший урон и окрашиваем в красный цвет.
            if (Resultat[1].text != "Результат: " && double.Parse(Resultat[1].text) != 0 && Resultat[0].text != "Результат: " && double.Parse(Resultat[0].text) != 0 && double.Parse(Resultat[0].text) != double.Parse(Resultat[1].text))
               {
                   if (double.Parse(Resultat[0].text) > double.Parse(Resultat[1].text))
                {
                    Resultat[0].color = DarkRed;
                    if (Difference[0].text == Difference[1].text)
                        Difference[0].text += "(" + (int)(double.Parse(Resultat[0].text) / (double.Parse(Resultat[1].text) / 100) - 100 ) + "%)";
                }

                else
                {
                    Resultat[1].color = DarkRed;
                    if (Difference[0].text == Difference[1].text)
                        Difference[1].text += "(" + (int)(double.Parse(Resultat[1].text) / (double.Parse(Resultat[0].text) / 100) - 100) + "%)";
                }
                       
               }
        }



        private void CreateRanged()
        {
            if (statField[0].text != "")
            {
                Weapon weapon_0 = new Weapon(statField, statToggle, modifierField);
                Resultat[0].text = "" + weapon_0; // Считаем Дпс и присваеваем полученное значению текстовой надписи.
            }
            else
                Resultat[0].text = "Результат: ";


            if (statField[statField.Length / 2].text != "")
            {
                Weapon weapon_1 = new Weapon(statField, statToggle, modifierField);
                Resultat[1].text = "" + weapon_1;// Считаем Дпс и присваеваем полученное значению текстовой надписи.
            }
            else
                Resultat[1].text = "Результат: ";
        }

        private void CreateTrap()
        {
            if (statField[0].text != "")
            {
                Trap weapon_0 = new Trap(statField, statToggle, modifierField);
                Resultat[0].text = "" + weapon_0; // Считаем Дпс и присваеваем полученное значению текстовой надписи.
            }
            else
                Resultat[0].text = "Результат: ";


            if (statField[statField.Length / 2].text != "")
            {
                Trap weapon_1 = new Trap(statField, statToggle, modifierField);
                Resultat[1].text = "" + weapon_1;// Считаем Дпс и присваеваем полученное значению текстовой надписи.
            }
            else
                Resultat[1].text = "Результат: ";
        }

        private void CreateLauncher()
        {
            //int resources = 1;
            //int[] tempCost = new int[2];
            //double[] tempDps = new double[2];
            //double[] tempTime = new double[2];

            if (statField[0].text != "" || statField[6].text != "")
            {
                Launcher weapon_0 = new Launcher(statField, statToggle, modifierField);
                Resultat[0].text = "" + weapon_0; // Считаем Дпс и присваеваем полученное значению текстовой надписи.
                if (Resultat[0].text != "0")
                {
                    Difference[0].text = (weapon_0.Impact == 0) ? "Урон " : "Толчок ";
                    Difference[0].text += (weapon_0.DPS == 0) ? "за руду " : "в секунду";
                }
                else
                    Resultat[0].text = "Результат: ";
            }
            else
                Resultat[0].text = "Результат: ";


            if (statField[statField.Length / 2].text != "" || statField[statField.Length - 1].text != "")
            {
                Launcher weapon_1 = new Launcher(statField, statToggle, modifierField);
                Resultat[1].text = "" + weapon_1;// Считаем Дпс и присваеваем полученное значению текстовой надписи.
                if (Resultat[1].text != "0")
                {
                    Difference[1].text = (weapon_1.Impact == 0) ? "Урон " : "Толчок ";
                    Difference[1].text += (weapon_1.DPS == 0) ? "за руду " : "в секунду";
                }
                else
                    Resultat[1].text = "Результат: ";
            }
            else
                Resultat[1].text = "Результат: ";


            //if (tempCost[0] != tempCost[1] && tempCost[0] != 0 && tempCost[1] != 0)
            //{
            //    for (int i = tempCost[0] < tempCost[1] ? tempCost[0] * 2 : tempCost[1] * 2; i < 100; i++)
            //    {
            //        if (i % tempCost[0] == 0)
            //            if (i % tempCost[1] == 0)
            //            {
            //                resources = i;
            //                break;
            //            }         
            //    }

            //    for (int i = 0; i < tempTime.Length; i++)
            //    {
            //        int weaponCount = resources / tempCost[i];
            //        Resultat[i].text = "" + tempDps[i] * weaponCount; // Считаем Дпс и присваеваем полученное значению текстовой надписи.
            //        Difference[i].text = "" + tempTime[i] * weaponCount;
            //    } 
            //}
        }


        private void CreateMelee()
        {
            if (statField[0].text != "")
            {
                Melee weapon_0 = new Melee(statField, statToggle, modifierField, persona_1);
                Resultat[0].text = "" + weapon_0; // Считаем Дпс и присваеваем полученное значению текстовой надписи.
            }
            else
                Resultat[0].text = "Результат: ";


            if (statField[statField.Length / 2].text != "")
            {
                Melee weapon_1 = new Melee(statField, statToggle, modifierField, persona_2);
                Resultat[1].text = "" + weapon_1;// Считаем Дпс и присваеваем полученное значению текстовой надписи.
            }
            else
                Resultat[1].text = "Результат: ";
        }


    }
    }

