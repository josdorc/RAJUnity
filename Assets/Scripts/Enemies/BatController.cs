using UnityEngine;
using System.Collections;

public class BatController : MonoBehaviour {

    public float Speed;
    public float DetectionRadio;
    public float RestTime;

    protected Vector3 m_StartPos;
    protected GameObject m_Player;
    protected EEnemyState m_State;
    protected Vector3[] m_Path;
    
	// Use this for initialization
	void Start () {
        m_Player = GameObject.FindWithTag("Player");

        float dir = this.transform.position.x > m_Player.transform.position.x ? 1 : -1;
        this.transform.localScale = new Vector3(dir, 1, 1);

        m_State = EEnemyState.Idle;
        m_StartPos = this.transform.position;
        m_Path = new Vector3[4];
	}
	


	// Update is called once per frame
    void Update()
    {
        if (m_State == EEnemyState.Idle)
        {
            float distance = Vector3.Distance(m_Player.transform.position, this.transform.position);

            if (distance < DetectionRadio)
            {
                m_State = EEnemyState.Attack;
                Vector3 delta = m_Player.transform.position - this.transform.position;

                m_Path[0] = this.transform.position;
                m_Path[1] = new Vector3(this.transform.position.x + delta.x / 4, this.transform.position.y + delta.y / 2, 0);
                m_Path[2] = new Vector3(this.transform.position.x + delta.x / 2, this.transform.position.y + 3 * delta.y / 4, 0);
                m_Path[3] = m_Player.transform.position;

                float dir = this.transform.position.x > m_Player.transform.position.x ? 1 : -1;
                this.transform.localScale = new Vector3(dir, 1, 1);

                iTween.MoveTo(this.gameObject,
                iTween.Hash(
                "path", m_Path,
                "time", Speed,
                "oncomplete", "OnAttackEnd")
                );
            }

            this.GetComponent<Animator>().SetTrigger("Rest");
        }
        else
        {
            this.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    void OnAttackEnd()
    {
        m_State = EEnemyState.Rest;

        iTween.MoveTo(this.gameObject,
            iTween.Hash(
            "position", m_StartPos,
            "time", Speed,
            "delay", RestTime,
            "oncomplete", "OnRestEnd"));
    }

    void OnRestEnd()
    {
        m_State = EEnemyState.Idle;
    }

}
