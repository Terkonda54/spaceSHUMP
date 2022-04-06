/**** 
 * Created by: Anupam Terkonda
 * Date Created: April 6, 2022
 * 
 * Last Edited by: 
 * Last Edited: 
 * 
 * Description: Pool return
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolReturn : MonoBehaviour
{
    private ObjectPool pool;


    // Start is called before the first frame update
    void Start()
    {
        pool = ObjectPool.POOL; //reference to pool
    }

    private void OnDisable()
    {
        if(pool != null)
        {
            pool.ReturnObjects(this.gameObject); //return this object to pool
        }
    }
}
