using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    public float _Damage;
    public GameObject _ShootPos;
    private Vector3 _Rot;
	
	void Update () 
    {
        _Rot.x = _ShootPos.transform.rotation.x;
        _Rot.y = _ShootPos.transform.rotation.y;
        _Rot.z = _ShootPos.transform.rotation.z;
        _Rot = Vector3.forward;

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_ShootPos.transform.position, _ShootPos.transform.TransformDirection(_Rot), out hit, 1000))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().DoDamage(_Damage);
                    }
                }
            }
        }
    }
}