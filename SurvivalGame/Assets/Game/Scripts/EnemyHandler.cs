using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour 
{
    public List<GameObject> m_Enemys;
    public List<GameObject> m_EnemySpawns;
    public Vector2 m_RandomSpawnTime;

    private float m_CalcTime;
    private float m_Timer;

    void Start()
    {
        m_CalcTime = Random.Range(m_RandomSpawnTime.x,m_RandomSpawnTime.y);
    }

	void Update () 
    {
        m_Timer += 1 * Time.deltaTime;

        if (m_Timer >= m_CalcTime)
        {
            GameObject m_Enemy = Instantiate(m_Enemys[0], m_EnemySpawns[0].transform.position, Quaternion.identity);
            m_CalcTime = Random.Range(m_RandomSpawnTime.x,m_RandomSpawnTime.y);
            m_Timer = 0;
        }
	}
}