using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.SimpleLocalization
{
    public class ChoosingLanguage : MonoBehaviour
    {
        [SerializeField] string[] setLanguage;

        private int number = 0;

        public void Awake()
        {
            LocalizationManager.Read();

            switch (Application.systemLanguage)
            {
               
                case SystemLanguage.Russian:
                    LocalizationManager.Language = "Russian";
                    break;
                default:
                    LocalizationManager.Language = "English";
                    break;
            }
        }

        public void SetLocalization()
        {
            LocalizationManager.Language = setLanguage[number];
            if(number >= setLanguage.Length - 1)
            {
                number = 0;
            } else
            {
                number++;
            }
        }

    }
}
