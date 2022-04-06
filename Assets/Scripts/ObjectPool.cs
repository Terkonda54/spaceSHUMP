/**** 
 * Created by: Anupam Terkonda
 * Date Created: April 6, 2022
 * 
 * Last Edited by: 
 * Last Edited: 
 * 
 * Description: Pool of objects for reuse
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    //Variables
    #region POOL Singleton
    static public ObjectPool POOL;

    void CheckPOOLIsInScene()
    {
        if(POOL == null)
        {
            POOL = this;
        }
        else
        {
            Debug.LogError("POOL.Awake() - Attempted to assign a second ObjectPool.POOL");
        }
    }
    #endregion
    private Queue<GameObject> projectiles = new Queue<GameObject>(); //queue of game objects to be added to the pool

    [Header("Pool Settings")]
    public GameObject projectilePrefab;
    public int poolStartSize = 5;

    //Methods
    private void Awake()
    {
        CheckPOOLIsInScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolStartSize; i++)
        {
            GameObject gObject = Instantiate(projectilePrefab);
            projectiles.Enqueue(gObject); //add to queue
            gObject.SetActive(false); //disable projectile in scene
        }
    }


    public GameObject GetObject()
    {
        if (projectiles.Count > 0)
        {
            GameObject gObject = projectiles.Dequeue();
            gObject.SetActive(true);
            return gObject;
        }
        else
        {
            Debug.LogWarning("Out of projectiles, reloading...");
                return null;
        }
    }

    public void ReturnObjects(GameObject gObject)
    {
        projectiles.Enqueue(gObject);
        gObject.SetActive(false);
    }

}
