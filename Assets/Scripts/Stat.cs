using System;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField]
    public BarScript bar;

    [SerializeField]
    public float maxVal;

    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {
            
            currentVal = value;
            bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            maxVal = value;
            bar.MaxValue = maxVal;
        }
    }



    public void Initialize()
    {
        MaxVal = MaxVal;
        CurrentVal = currentVal;
    }
}
