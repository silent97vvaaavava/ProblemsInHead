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

            if (!PlayerPrefs.HasKey("Language"))
            {
                PlayerPrefs.SetString("Language", "");
                switch (Application.systemLanguage)
                {

                    case SystemLanguage.Russian:
                        LocalizationManager.Language = "Russian";
                        PlayerPrefs.SetString("Language", "Russian");
                        break;
                    default:
                        LocalizationManager.Language = "English";
                        PlayerPrefs.SetString("Language", "English");
                        break;
                }
            } else
            {
                LocalizationManager.Language = PlayerPrefs.GetString("Language");
            }
        }

        public void SetLocalization()
        {
            LocalizationManager.Language = setLanguage[number];
            PlayerPrefs.SetString("Language", setLanguage[number]);
            if (number >= setLanguage.Length - 1)
            {
                number = 0;
            } else
            {
                number++;
            }

            Debug.Log(PlayerPrefs.GetString("Language"));
        }

    }
}
