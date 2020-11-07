using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput ;

public class Wormy : MonoBehaviour
{
    int h;   
    public GameObject deathAnim;
    public GameObject splash;
    public GameObject Explosion;

    public GameObject Explosion1 ;

    public GameObject character;

    public GameObject matchOver ;

    WeaponPanel WeaponPanel ;

    public string res;
     

    [SerializeField]
    private Stat health;

    private bool immortal = false;
    public Animator animator;

    public Animator waponAnim;
    public Rigidbody2D bulletPrefab;
    public Rigidbody2D bulletPrefab1;

    Rigidbody2D projectile;
    public Transform currentGun;

    public Transform secondGun ;

    public Transform thirdGun ;
    
    public Transform fourthdGun ;

    private Rigidbody2D rbd2;
    public float upForce = 100000f;

    public float wormySpeed = 4;
    private bool isDead = false  ;
    public float maxRelativeVelocity;
    public float misileForce = 5; 

    public bool IsTurn { get { return WormyManager.singleton.IsMyTurn(wormId); } }

    public int wormId;
    
    SpriteRenderer ren;
    private float targetOrtho;
    Animator anim ;

    private void Start()
    {
        h=5;
       matchOver.SetActive(false);
        rbd2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ren = GetComponent<SpriteRenderer>();
        bulletPrefab.GetComponent<CircleCollider2D>().radius = 0.1f;
        health.Initialize();
        secondGun.gameObject.SetActive(false);
        thirdGun.gameObject.SetActive(false);
        targetOrtho = Camera.main.orthographicSize;
        Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, 1 * Time.deltaTime);
        
    }


    private void Update()
    {
        
         if (isDead==true){
          
            animator.SetTrigger("die") ;

          return;
        }


        if (!IsTurn)
            return;

            


           /*  if (Input.GetMouseButtonDown(1)  )							//Weapon selection and info
			{

                /*WeaponPanel = GameObject.Find("WeaponPanel").GetComponent<WeaponPanel>();

			    WeaponPanel.GetOnScreen();
                waponAnim.SetTrigger("appear");

	
			} */
          if (currentGun==secondGun)
          {
               bulletPrefab.GetComponent<CircleCollider2D>().radius = 0.2f;
               h=7;
          }
            else if (currentGun==thirdGun)
          {
               bulletPrefab.GetComponent<CircleCollider2D>().radius = 0.25f;
               h=10;
          }
           else if (currentGun==fourthdGun)
          {
               bulletPrefab.GetComponent<CircleCollider2D>().radius = 0.3f;
               h=15;
          }
        RotateGun();

        var hor = CrossPlatformInputManager.GetAxis("Horizontal");
        
        animator.SetFloat("Horizontal",Input.GetAxis("Horizontal"));
        if (hor == 0)
        {
            currentGun.gameObject.SetActive(true);

            ren.flipX = currentGun.eulerAngles.z < 180;

            if(Input.GetKeyDown(KeyCode.X))
            {
                rbd2.velocity = Vector2.zero;
                rbd2.AddForce(Vector2.up * upForce);
                animator.SetTrigger("jump");
            }
            

         /*   if (Input.GetMouseButtonDown(0)  )	
            {             

                var p = Instantiate(bulletPrefab,
                                   currentGun.position - currentGun.right,
                                   currentGun.rotation);

                p.AddForce(-currentGun.right * misileForce, ForceMode2D.Impulse);
                if  (IsTurn)
                    WormyManager.singleton.NextWorm();
                               
            } */
        }
        else
        {
            currentGun.gameObject.SetActive(false);
            transform.position += Vector3.right *
                                hor *
                                Time.deltaTime *
                                wormySpeed;            
             ren.flipX = Input.GetAxis("Horizontal") > 0;
             
        }


    }

   
    void RotateGun()
    {
        var diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        currentGun.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("water")){
            health.CurrentVal = 0 ;
             character.GetComponent<BarScript>();
             GameObject e1 = Instantiate(deathAnim) as GameObject;
            e1.transform.position = this.transform.position;
            GameObject e3 = Instantiate(splash) as GameObject;
            e3.transform.position = this.transform.position;
            isDead = true;
            matchOver.SetActive(true);
            //this.gameObject.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<Aim>().crosshair.SetActive(false);

        }
        if (collision.relativeVelocity.magnitude > maxRelativeVelocity)
        {
        
            if (IsTurn)
                WormyManager.singleton.NextWorm();
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        res = "colission";
        GameObject e = Instantiate(Explosion) as GameObject;
        e.transform.position = transform.position;
        if ((res == "colission"))
        {
            health.CurrentVal -= h;
        }
        character.GetComponent<BarScript>();
        if (health.CurrentVal <= 0 ){
           GameObject e1 = Instantiate(deathAnim) as GameObject;
            e1.transform.position = this.transform.position;
            isDead = true;
            health.CurrentVal = 0 ;
            matchOver.SetActive(true);
            //this.gameObject.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<Aim>().crosshair.SetActive(false);
            
        }

        if (collision.CompareTag("Explosion"))
        {
           
            if (IsTurn)
                WormyManager.singleton.NextWorm();
        } 
            
    }

 public void SetFirstWeapon()
    {
        
        currentGun.gameObject.SetActive(false);
        currentGun = secondGun;
        secondGun.gameObject.SetActive(true);
        misileForce = 15 ;
        
        
    }


    public void SetSecondWeapon()
    {
        currentGun.gameObject.SetActive(false);
        currentGun = thirdGun;
        thirdGun.gameObject.SetActive(true);
        misileForce = 17 ;          
        
        
        
        

    }
    
     public void SetThirdWeapon()
    {
        currentGun.gameObject.SetActive(false);
        currentGun = fourthdGun;
        fourthdGun.gameObject.SetActive(true);
        misileForce = 20 ;
       
    }

    

    public void jump (){

                rbd2.velocity = Vector2.zero;
                rbd2.AddForce(Vector2.up * upForce);
                animator.SetTrigger("jump");
    }

    public void fire (){
        
                var p = Instantiate(bulletPrefab,
                                   currentGun.position - currentGun.right,
                                   currentGun.rotation);

                p.AddForce(-currentGun.right * misileForce, ForceMode2D.Impulse);
                if  (IsTurn)
                    WormyManager.singleton.NextWorm();
                
    }

    public void move(){

         var hor = Input.GetAxis("Horizontal");
        
        
        if (hor == 0)
        {
            currentGun.gameObject.SetActive(true);

            ren.flipX = currentGun.eulerAngles.z < 180;
        }
        else
        {
            currentGun.gameObject.SetActive(false);
            transform.position += Vector3.right *
                                hor *
                                Time.deltaTime *
                                wormySpeed;            
             ren.flipX = Input.GetAxis("Horizontal") > 0;

    }
    }

}
