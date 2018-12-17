using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets
{
    public class ButtonView : MonoBehaviour
    {

        public Image[] Image = new Image[2];
        public GameObject[] Button = new GameObject[2];
        Color transparent = new Color(1F, 1F, 1F, 0.5F);
        public InputField[] InputField = new InputField[4];

        int iterator = 0;
        bool active = false;

        public void Set_Button1()
        {
            Image[0].color = Color.white;
            Image[1].color = transparent;
        }

        public void Set_Button2()
        {
            Image[0].color = transparent;
            Image[1].color = Color.white;
        }

        public void ActivateButtons()
        {
            for (int i = 0; i < Image.Length; i++)
            {
                Image[i].color = Color.white;
            }
            Switcher();
        }

        public void DeactivateButtons()
        {
            for(int i = 0; i < Image.Length; i++)
            {
                Image[i].color = transparent;
            }
            Switcher();
        }

        void Switcher()
        {
            Button[iterator].SetActive(false);
            if (iterator + 1 != Button.Length)
                iterator++;
            else
                iterator = 0;

            Button[iterator].SetActive(true);
        }

        public void ImpactButton()
        {
            if (active == false)
            {
                for (int i = 0; i < Button.Length; i = i + 2)
                {
                    Button[i].SetActive(true);
                    InputField[i].readOnly = false;
                }

                for (int i = 1; i < Button.Length; i = i + 2)
                {
                    InputField[i].readOnly = true;
                    Button[i].SetActive(false);    
                }

                active = true;
                Image[0].color = Color.white;
            }

            else
            {
                for (int i = 0; i < Button.Length; i = i + 2)
                {
                    InputField[i].readOnly = true;
                    Button[i].SetActive(false); 
                }
                   

                for (int i = 1; i < Button.Length; i = i + 2)
                {
                    Button[i].SetActive(true);
                    InputField[i].readOnly = false;
                }
                    

                active = false;
                Image[0].color = transparent;
            }

        }
    }
}
