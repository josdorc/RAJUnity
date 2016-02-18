using UnityEngine;
using System.Collections;

public class BossLookTarget : MonoBehaviour {
    protected GameObject m_Player;
    protected bool m_FacingRight;
    protected bool m_PreviousDir;

	// Use this for initialization
	void Start () {
       
        m_FacingRight = false;
        m_PreviousDir = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (m_Player != null)
        {
            m_FacingRight = this.transform.position.x < m_Player.transform.position.x;

            if (m_PreviousDir != m_FacingRight)
            {
                Flip();
                m_PreviousDir = m_FacingRight;
            }
        }
  
	}

    public void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void SetTarget()
    {
        m_Player = GameObject.FindWithTag("Player");
    }

    public bool HasTarget()
    {
        return m_Player != null;
    }
}
