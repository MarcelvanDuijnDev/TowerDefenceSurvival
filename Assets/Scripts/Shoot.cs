using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    private void Update()
    {
        
    }

    void Fire()
    {
        GameObject bullet = ObjectPool.POOL.GetObject("Bullet",false);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
    }
}
