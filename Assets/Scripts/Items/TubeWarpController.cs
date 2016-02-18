using UnityEngine;
using System.Collections;

public class TubeWarpController : MonoBehaviour
{
    public string Scene;
    public Vector3 Position;
    public float WarpTime;
    public Sprite[] ProgressSprites;

    protected bool m_Active;
    protected float m_CurrentTime;
    protected GameObject m_Player;
    protected Transform m_ProgressBar;

    // Use this for initialization
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player");
        m_ProgressBar = m_Player.transform.FindChild("ProgressBar");

        m_CurrentTime = 0;
        m_Active = false;
    }

    // Update is called once per frame
    void Update()
    {     
        if (m_Active)
        {
            m_CurrentTime += Time.deltaTime;

            float stepTime = WarpTime / ProgressSprites.Length;           
            int index = (int)(m_CurrentTime / stepTime);

            if (index < ProgressSprites.Length)
            {
                m_ProgressBar.GetComponent<SpriteRenderer>().sprite = ProgressSprites[index];
            }
            else
            {
                m_Player.GetComponent<PlayerWarpController>().Warp(this.transform.position,this.transform.up,Scene, Position);
                m_Active = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            if (this.transform.up != Vector3.up)
            {
                m_Player.GetComponent<PlayerWarpController>().Warp(this.transform.position, this.transform.up, Scene, Position);
            }
            else if (!m_Active)
            {
                m_Active = true;
                m_CurrentTime = 0;
                m_ProgressBar.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            m_Active = false;
            m_CurrentTime = 0;
            m_ProgressBar.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

