using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour 
{
    [SerializeField] private float _Health;
    [SerializeField] private float _CurrentHealth;
    [SerializeField] private float _HealthRegen;
    [SerializeField] private Text _HealthText;
	
    void Start()
    {
        _CurrentHealth = _Health;
    }

	void Update () 
    {
        _HealthText.text = _CurrentHealth.ToString("F0") + "/" + _Health.ToString("F0");
        if (_CurrentHealth < _Health)
            _CurrentHealth += _HealthRegen * Time.deltaTime;
        if (_CurrentHealth > _Health)
            _CurrentHealth = _Health;
	}

    public void DoDamage(float amount)
    {
        _CurrentHealth -= amount;
    }
}
