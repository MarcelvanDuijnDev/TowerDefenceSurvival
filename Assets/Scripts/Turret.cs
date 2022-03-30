using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour 
{
    public GameObject _Target;
    public float _DistanceMin;
    public float _DistanceMax;

    public float _MaxHealth;
    public float _Health;

    public float _FireRate;
    private float _FinalFireRate;
    public float _Damage;
    [SerializeField]private GameObject _ShootPart;
    private float _Timer;

    [SerializeField] private LineRenderer _LR;

	void Start () 
    {
        _FinalFireRate = 60 / _FireRate;
        _Health = _MaxHealth;
	}
	
	void Update () 
    {
        if (_Target != null)
        {
            _ShootPart.transform.LookAt(_Target.transform.position);
            _Timer += 1 * Time.deltaTime;
            if (_Timer >= _FinalFireRate)
            {
                _Target.GetComponent<Enemy>().DoDamage(_Damage);
                _Timer = 0;
            }
            Vector3[] lrpositions = new Vector3[2];
            lrpositions[0] = _ShootPart.transform.position;
            lrpositions[1] = _Target.transform.position;
            _LR.SetPositions(lrpositions);
        }
        else
        {
            _ShootPart.transform.rotation = Quaternion.Euler(90,0,0);
            Vector3[] lrpositions = new Vector3[2];
            lrpositions[0] = Vector3.zero;
            lrpositions[1] = Vector3.zero;
            _LR.SetPositions(lrpositions);
        }

        _Target = FindEnemy(_DistanceMin,_DistanceMax);
	}

    public GameObject FindEnemy(float min, float max)
    {
        GameObject[] m_Targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        min = min * min;
        max = max * max;
        foreach (GameObject target in m_Targets)
        {
            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && curDistance >= min && curDistance <= max)
            {
                closest = target;
                distance = curDistance;
            }
        }
        return closest;
    }
}
