/**** 
 * Created by: Anuapm Terkonda
 * Date Created: 3/30/22
 * 
 * Last Edited by: Anupam Terkonda
 * Last Edited: March 30, 2022
 * 
 * Description: Projectile Behaviour
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //VARIABLES
    private BoundsCheck bndCheck; //reference to the bounds check

    // Start is called before the first frame update
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();

    } //end Awake()

    // Update is called once per frame
    void Update()
    {
        //if off screen up
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }
    }//end Update()
}
