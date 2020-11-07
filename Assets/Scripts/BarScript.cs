using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;


    [SerializeField]
    private Image content;

    [SerializeField]
    private Text ValueText;

    public float MaxValue { get; set; }


    public float Value
    {
        set
        {
            string[] tmp = ValueText.text.Split(':');
            ValueText.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue,0, 1);
            
        }
    }

 

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
   public void Update()
    {
        HandleBar();
    }
    public void HandleBar()
    {
        if(fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount,fillAmount,Time.deltaTime * lerpSpeed);

        }

        content.color = Color.Lerp(lowColor, fullColor, fillAmount);
    }

    private float Map(float value,float inMin,float inMax,float outMin,float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
