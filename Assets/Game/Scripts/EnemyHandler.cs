using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour 
{
    [Header("Spawn/Enemy")]
    public ObjectPool[] m_Enemys;
    public GameObject[] m_Spawns;

    [Header("Info")]
    [SerializeField]private float m_Time;

    [Header("Wave")]
    public Wave[] m_Wave;

    void Start()
    {
        for (int i = 0; i < m_Enemys.Length; i++)
        {
            m_Enemys[i] = (ObjectPool)m_Enemys[i].GetComponent(typeof(ObjectPool));
        }
    }

	void Update () 
    {
        m_Time += 1 * Time.deltaTime;

        for (int i = 0; i < m_Wave.Length; i++)
        {
            if(m_Wave[i].time < m_Time && !m_Wave[i].active)
            {
                for (int o = 0; o < m_Wave[i].amount; o++)
                {
                    Spawn(m_Wave[i].enemyID, m_Wave[i].spawnID);
                }
                m_Wave[i].active = true;
            }
        }
	}

    void Spawn(int enemyID, int spawnID)
    {
        for (int i = 0; i < m_Enemys[enemyID].objects.Count; i++)
        {
            if (!m_Enemys[enemyID].objects[i].activeInHierarchy)
            {
                m_Enemys[enemyID].objects[i].transform.position = m_Spawns[spawnID].transform.position;
                m_Enemys[enemyID].objects[i].transform.rotation = transform.rotation;
                m_Enemys[enemyID].objects[i].SetActive(true);
                break;
            }
        }
    }
}

[System.Serializable]
public struct Wave
{
    public string waveInfo;
    public float time;
    public int enemyID;
    public int spawnID;
    public int amount;
    [HideInInspector]
    public bool active;
}