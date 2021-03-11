using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ChangeLocalization : MonoBehaviour
{
    AsyncOperationHandle m_InitializeOperation;
    List<UnityEngine.Localization.Locale> locales = new List<UnityEngine.Localization.Locale>();

    int number;

    private void Start()
    {
        m_InitializeOperation = LocalizationSettings.SelectedLocaleAsync;
        if (m_InitializeOperation.IsDone)
        {
            InitializeCompleted(m_InitializeOperation);
        }
        else
        {
            m_InitializeOperation.Completed += InitializeCompleted;
        }
    }

    void InitializeCompleted(AsyncOperationHandle obj)
    {
        locales = LocalizationSettings.AvailableLocales.Locales;

        for (int i = 0; i < locales.Count; ++i)
        {
            var locale = locales[i];
            if(LocalizationSettings.SelectedLocaleAsync.Result.name == locales[i].name)
            {
                number = i;
                PlayerPrefs.SetInt("Number", i);
                PlayerPrefs.SetString("Language", locales[number].name);
                Debug.Log(PlayerPrefs.GetString("Language"));
            }
        }
        
    }

    public void ChangeLanguage()
    {
        if (number >= locales.Count - 1)
        {
            number = 0;
        }
        else
        {
            number++;
        }
        LocalizationSettings.SelectedLocale = locales[number];
        PlayerPrefs.SetString("Language", locales[number].name);
       
        PlayerPrefs.SetInt("Number", number);
    }
}
