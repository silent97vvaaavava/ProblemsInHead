using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;


/// <summary>
/// checking values ​​for equality
/// reset for start position pin
/// </summary>

public class Lock : MonoBehaviour
{
    [Header("Get value pin")]
    [SerializeField] Text[] pinValue;
    [SerializeField] Image[] imageValue;
    [SerializeField] Flowchart flowchart;
    [SerializeField] GameObject button;
    [SerializeField] AudioSource audioLock;


    [Tooltip("this array with start value pin")]
    float[] startValue = new float[3];

    private Animation animationLock;

    private void Start()
    {
        animationLock = gameObject.GetComponent<Animation>();
        GetValuePin();
        SetAllValue();
    }


    private void Update()
    {
        EqualityPin();
    }


    void EqualityPin()
    {
        for(int i = 0; i < pinValue.Length; i++)
        {
            if (!pinValue[i].text.Equals(pinValue[(pinValue.Length-1) - i].text))
            {
                break;
            } else
            if(pinValue[i].text == "0")
            {
                if (!audioLock.isPlaying)
                {
                    audioLock.Play(1);
                }
                else
                {
                    //Debug.Log("next");
                    Invoke("NextScene", 1.5f);
                }
                //NextScene();
                //button.SetActive(true);
                //Invoke("NextScene", 0.5f);
            }
        }
    }


    public void NextScene()
    {
        //if (!audioLock.isPlaying)
        //{
        //    audioLock.Play(1);
        //}
        //else
        //{
            //Debug.Log("next");
            flowchart.ExecuteBlock("AbtractCross");
        //}
    }


    public void Animation()
    {
        animationLock.Play();
    }

    public void Reset()
    {
        SetAllValue(); 
    }

    void SetAllValue()
    {
        for(int i = 0; i < pinValue.Length; i++)
        {
            pinValue[i].text = startValue[i].ToString();
            imageValue[i].fillAmount = startValue[i] / 10f;
        }
    }

    void GetValuePin()
    {
        for(int i = 0; i < pinValue.Length; i++)
        {
            startValue[i] = imageValue[i].fillAmount * 10;
        }
    }

}
