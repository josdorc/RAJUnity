using UnityEngine;
using System.Collections;

public class MagnetController : MonoBehaviour {

    public Sprite MagnetMad;
    public Sprite MagnetIdle;
    public float Interval;
    protected Vector3[] m_Points;
    protected float m_CurrentTime;
    

	// Use this for initialization
	void Start () {
	    
        m_Points = new Vector3[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            m_Points[i] = this.transform.GetChild(i).position;
            this.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            hit.transform.GetComponent<PlayerAttackController>().LostAttack(this.transform.position);
            this.GetComponent<SpriteRenderer>().sprite = MagnetMad;
        }
    }

    void OnTriggerStay2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Player")
            AtractEffect();
    }

    void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            this.GetComponent<SpriteRenderer>().sprite = MagnetIdle;
        }
    }

    void AtractEffect()
    {
        if (m_CurrentTime == 0 || m_CurrentTime > Interval)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                GameObject point = GameObject.Instantiate(this.transform.GetChild(i).gameObject, m_Points[i], Quaternion.identity) as GameObject;
                point.GetComponent<SpriteRenderer>().enabled = true;
                iTween.MoveTo(point, iTween.Hash("position", m_Points[i] + Vector3.up, "time", Interval));
                DestroyObject(point, Interval);
            }

            m_CurrentTime = 0;
        }

        m_CurrentTime += Time.deltaTime;
    }
}
