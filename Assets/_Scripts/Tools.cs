using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// работа инструментов
/// </summary>

public class Tools : MonoBehaviour, IPointerEnterHandler
{
    [Header("Set in Inspector")]
    [Tooltip("Get value text pin")]
    [SerializeField] Text[] pinValue;
    [Tooltip("Get value image pin")]
    [SerializeField] Image[] pinImage;
    [Tooltip("Set value pin")]
    [SerializeField] float[] toolValue;
    [Tooltip("Get animation joggle lock")]
    [SerializeField] Animation animationLock;

    [Header("Set component for description")]
    [SerializeField] Animation animationDescription;
    [SerializeField] Text[] valueDescription;
    //[SerializeField] Text currentNameTool;

    private Text nameTool;

    private void Awake()
    {
        //nameTool = this.gameObject.GetComponentInChildren<Text>();
        //Debug.Log(nameTool.text);
    }

    // change value pin
    public void ChangeValue()
    {
        if (animationLock.isPlaying)
        {
            animationLock.Stop();

            animationLock.Play();
        } else
        {
            animationLock.Play();
        }
        for (int i = 0; i < pinValue.Length; i++)
        {
            pinValue[i].text = (int.Parse(pinValue[i].text) - toolValue[i]).ToString();
            pinImage[i].fillAmount -= toolValue[i] / 10f; 
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptionValue();
        Invoke("ShowDescriptionPanel", 0.1f);
    }

    

    void DescriptionValue()
    {
        for(int i = 0; i < valueDescription.Length; i++)
        {
            valueDescription[i].text = toolValue[i].ToString();
        }

        //currentNameTool.text = nameTool.text;
    }

    void ShowDescriptionPanel()
    {
        if (animationDescription.isPlaying)
        {
            animationDescription.Stop();
            animationDescription.Play();
        } else
        {
            animationDescription.Play();
        }
    }

}
