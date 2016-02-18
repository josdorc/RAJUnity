using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {
    public GameObject[] Enemies;
    public float DetectionRadio;
    public int Delay;
    public bool IsRandom;
    public int MaxEnemies;
    public Vector2 Direction;

    protected bool m_PlayerDetected = false;
    protected float m_Time = 0;
    protected int m_EnemyIndex = 0;
    protected GameObject m_Player;

	// Use this for initialization
	void Start () {
        m_Player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (!m_PlayerDetected)
        {
            m_PlayerDetected = Vector3.Distance(m_Player.transform.position, this.transform.position) < DetectionRadio;
            if (m_PlayerDetected) 
                SpawnEnemy();
        }

        if(m_PlayerDetected)
        {
            m_Time += Time.deltaTime;
            if (m_Time > Delay)
            {
                SpawnEnemy();
                m_Time = 0;
            }
        }
	}

    void SpawnEnemy()
    {
        if (this.transform.childCount < MaxEnemies)
        {
            GameObject enemy = GameObject.Instantiate(Enemies[m_EnemyIndex], this.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = this.transform;
            enemy.transform.localScale = new Vector3(Direction.x, 1, 1);

            if (enemy.GetComponent<SlimeController>() != null)
                enemy.GetComponent<SlimeController>().DetectionRadio = DetectionRadio;

            if (m_EnemyIndex < Enemies.Length - 1)
                m_EnemyIndex++;
            else
                m_EnemyIndex = 0;
        }
    }
}
