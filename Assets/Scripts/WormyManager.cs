using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormyManager : MonoBehaviour
{
    Wormy[] wormies;

    public float zoomSpeed = 1;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.back;
     public float targetOrtho;
    public Animator waponAnim;
    
     WeaponPanel WeaponPanel ;
     public float smoothSpeed = 7.0f;
     public float minOrtho = 1.0f;
     public float maxOrtho = 20.0f;

    public Transform wormyCamera;

    public static WormyManager singleton;

    private int currentWormy;

    void Start()
    {

       // waponAnim = GetComponent<Animator>();

        if(singleton !=null)
        {


        }
      
        singleton = this;

        wormies = GameObject.FindObjectsOfType<Wormy>();
        wormyCamera = Camera.main.transform;

        

        for (int i = 0; i < wormies.Length; i++)
        {
            wormies[i].wormId = i;
        }
    }

    void Update () {
        
         
     }

    public void NextWorm()
    {
        
        StartCoroutine(NextWormCoroutine());
    }

    public IEnumerator NextWormCoroutine()
    {
        var nextWorm = currentWormy + 1;
        currentWormy = -1;
        targetOrtho = Camera.main.orthographicSize;

        yield return new WaitForSeconds(2);

        currentWormy = nextWorm;
        if (currentWormy >= wormies.Length)
        {
            currentWormy = 0;
        }
        
        
        wormyCamera.SetParent(wormies[currentWormy].transform);
        wormyCamera.localPosition = Vector3.zero + Vector3.back * 10;
       
        

    }


    public bool IsMyTurn(int i)
    {
        return i == currentWormy;
    }

   

    public void chooseWeapon (int i){

        if (i==0){
            wormies[currentWormy].SetFirstWeapon();
        }else if (i==1){
            wormies[currentWormy].SetSecondWeapon();
        }else if (i==2) {
            wormies[currentWormy].SetThirdWeapon();
        }            
                waponAnim.SetTrigger("fade");

    }

    public void menuWeapon (){
         /*WeaponPanel = GameObject.Find("WeaponPanel").GetComponent<WeaponPanel>();

			    WeaponPanel.GetOnScreen();*/
                waponAnim.SetTrigger("appear");
    }
    public void jump () {
       wormies[currentWormy].jump();
    }

    public void fire () {
        wormies[currentWormy].fire();
    }

   
    public void move () {
        wormies[currentWormy].move();
    }
}
