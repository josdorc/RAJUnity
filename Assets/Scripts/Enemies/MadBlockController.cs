using UnityEngine;
using System.Collections;

public class MadBlockController : MonoBehaviour {

    public Sprite MadSprite;
    public Sprite SadSprite;
    public float MotionTime;
    public float DetectionRadio;
    public float Pause;
    public Vector2 Direction;    

    protected Vector2 m_EndPos = Vector2.zero;
    protected Vector2 m_StartPos = Vector3.zero;
    protected Vector2 m_Side = Vector2.zero;
   
    protected bool m_IsAttacking;
	
    // Use this for initialization
	void Start () {

        m_StartPos = new Vector2(this.transform.position.x, this.transform.position.y);
        float dim = this.GetComponent<BoxCollider2D>().size.x / 2;
        m_Side = new Vector3(Direction.y * dim, Direction.x * dim);
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!m_IsAttacking)
        {
            for (int i = -1; i <= 1; i++)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(m_StartPos + m_Side * i, Direction, DetectionRadio);

                if (m_EndPos == Vector2.zero && i == 0)
                    m_EndPos = hit[1].point - Direction * 0.37f;

                if (hit[1].transform.tag == "Player")
                {
                    OnAttackBegin();
                    break;
                }
            }
        }
    }

    void OnAttackBegin()
    {
        this.GetComponent<SpriteRenderer>().sprite = MadSprite;

        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", new Vector3(m_EndPos.x, m_EndPos.y, 0),
            "time", MotionTime,            
            "easetype", iTween.EaseType.easeInBack,
            "oncomplete", "OnAttackEnd"
            ));

        m_IsAttacking = true;
    }

    void OnAttackEnd()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", new Vector3(m_StartPos.x, m_StartPos.y, 0),
            "time", MotionTime,
            "delay", Pause,
            "easetype", iTween.EaseType.easeInBack,
            "oncomplete", "OnEndMotion"
            ));
    }

    void OnEndMotion()
    {
        this.GetComponent<SpriteRenderer>().sprite = SadSprite;
        m_IsAttacking = false;
    }
}
