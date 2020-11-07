using UnityEngine;
using System.Collections;

public class CamShakeSimple : MonoBehaviour 
{
#region variables
    public bool camShakeAcive = true; //on or off
    [Range(0, 1)]
     [SerializeField] float trauma;
    [SerializeField] float traumaMult = 16; //the power of the shake
    [SerializeField] float traumaMag = 0.8f; //the range of movment
    [SerializeField] float traumaRotMag = 17f; //the rotational power
    [SerializeField] float traumaDepthMag = 0.6f; //the depth multiplier
    [SerializeField] float traumaDecay = 1.3f; //how quickly the shake falls off
 
    float timeCounter = 0; //counter stored for smooth transition
    #endregion
 
    #region accessors
    public float Trauma //accessor is used to keep trauma within 0 to 1 range
    {
        get
        {
            return trauma;
        }
        set
        {
            trauma = Mathf.Clamp01(value);
        }
    }
    #endregion
 
    #region methods
    //Get a perlin float between -1 & 1, based off the time counter.
    float GetFloat(float seed)
    {
        return (Mathf.PerlinNoise(seed, timeCounter) - 0.5f) * 2f;
    }
 
    //use the above function to generate a Vector3, different seeds are used to ensure different numbers
    Vector3 GetVec3()
    {
        return new Vector3(
            GetFloat(1),
            GetFloat(10),
            //deapth modifier applied here
            GetFloat(100) * traumaDepthMag
            );
    }
    
    private void Update ()
    {
        if (camShakeAcive && Trauma > 0)
        {
            //increase the time counter (how fast the position changes) based off the traumaMult and some root of the Trauma
            timeCounter += Time.deltaTime * Mathf.Pow(Trauma,0.3f) * traumaMult;
            //Bind the movement to the desired range
            Vector3 newPos = GetVec3() * traumaMag * Trauma;;
            transform.localPosition = newPos;
            //rotation modifier applied here
            transform.localRotation = Quaternion.Euler(newPos * traumaRotMag);
            //decay faster at higher values
            Trauma -= Time.deltaTime * traumaDecay * (Trauma + 0.3f);
        }
        else
        {
            //lerp back towards default position and rotation once shake is done
            Vector3 newPos = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
            transform.localPosition = newPos;
            transform.localRotation = Quaternion.Euler(newPos * traumaRotMag);
        }
    }
    #endregion

}