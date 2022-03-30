using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _ImpactEffectObj = null;
    private float _Timer = 10;
	
	void Update () 
    {
        _Timer -= 1 * Time.deltaTime;
        if (_Timer <= 0)
        {
            this.gameObject.SetActive(false);
        }
	}

    void OnEnable()
    {
        _Timer = 10;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Untagged")
        {
            this.gameObject.SetActive(false);
            GameObject obj = (GameObject)Instantiate(_ImpactEffectObj);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        }
    }
}
