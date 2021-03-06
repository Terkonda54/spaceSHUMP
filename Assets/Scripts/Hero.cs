/**** 
 * Created by: Anupam Terkonda
 * Date Created: March 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 30, 2022
 * 
 * Description: Hero ship controller
****/

/** Using Namespaces **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //forces selection of parent object
public class Hero : MonoBehaviour
{
    /*** VARIABLES ***/

    #region PlayerShip Singleton
    static public Hero SHIP; //refence GameManager
   
    //Check to make sure only one gm of the GameManager is in the scene
    void CheckSHIPIsInScene()
    {

        //Check if instnace is null
        if (SHIP == null)
        {
            SHIP = this; //set SHIP to this game object
        }
        else //else if SHIP is not null send an error
        {
            Debug.LogError("Hero.Awake() - Attempeeted to assign second Hero.SHIP");
        }
    }//end CheckGameManagerIsInScene()
    #endregion
    public static Hero S; //Singleton



    GameManager gm; //reference to game manager
    ObjectPool pool; //reference to object pool

    [Header("Ship Movement")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    [Space(10)]
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;


    [Space(10)]

    private GameObject lastTriggerGo; //reference to the last triggering game object
    [Header("Shield Settings")]
    [SerializeField] //show in inspector
    private float _shieldLevel = 1; //level for shields
    public int maxShield = 4; //maximum shield level
    
    //method that acts as a field (property), if the property falls below zero the game object is desotryed
    public float shieldLevel
    {
        get { return (_shieldLevel); }
        set
        {
            _shieldLevel = Mathf.Min(value, maxShield); //Min returns the smallest of the values, therby making max sheilds 4

            //if the sheild is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);
                Debug.Log(gm.name);
                gm.LostLife();
                
            }

        }
    }

    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        S = this; //Set the Singleton
        CheckSHIPIsInScene(); //check for Hero SHIP
    }//end Awake()

    //Start is called once before the update
    private void Start()
    {
        gm = GameManager.GM; //find the game manager
        pool = ObjectPool.POOL; //find the object pool
    }//end Start()

        // Update is called once per frame (page 551)
        void Update()
    {
        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal"); 
        float yAxis = Input.GetAxis("Vertical"); 
        // Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
        // Rotate the ship to make it feel more dynamic 
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
        //player input


        //Allow the ship to fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        } //end space 

    }//end Update()


    //Taking Damage
    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        //Transform root returns the topmost transform in the heirarchy (ie parent)
        GameObject go = rootT.gameObject;

        if(go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go; //set the trigger to the last trigger
        if(go.tag == "Enemy")
        {
            Debug.Log("Triggered by enemy " + other.gameObject.name);
            shieldLevel--;
            Destroy(go);
        }
        else
        {
            Debug.Log("Triggered by non-Enemy " + other.gameObject.name);
        }

        


    }//end OnTriggerEnter()

     void FireProjectile()
    {
        //GameObject projGO = Instantiate<GameObject>(projectilePrefab);

        GameObject projGo = pool.GetObject();
        
        if(projGo != null)
        {
            projGo.transform.position = transform.position;
            Rigidbody rb = projGo.GetComponent<Rigidbody>();
            rb.velocity = Vector3.up * projectileSpeed;
        }

        
    }

    public void AddScore(int value)
    {
        gm.UpdateScore(value);
    }
}
