using UnityEngine;
using System.Collections;

public class BossWilberController : MonoBehaviour {
    public GameObject AttackPrefab;
    public GameObject ShieldPrefab;
    public GameObject LightPrefab;
    public float InitialSpeed;
    public float MadSpeedFactor;

    protected GameObject m_Player;
    protected GameObject m_Shield;
    protected int m_BeamShoots;
    protected float m_BeamDelay;
    protected int m_DefaultHealth;
    protected int m_PreviousHealth;
    protected bool m_PlayerDetected;
    protected bool m_IsShooting;
    protected bool m_IsAttacking;
    protected bool m_IsMoving;    

    // Use this for initialization
	void Start () {
        
        m_Player = GameObject.FindWithTag("Player");
        m_DefaultHealth = m_PreviousHealth = this.GetComponent<HealthController>().CurrentHealth;
        
         m_BeamShoots = 2;
        m_BeamDelay = 2;
	}

    void Update()
    {
        if(!m_PlayerDetected && this.GetComponent<BossLookTarget>().HasTarget())
        {
            m_IsAttacking = m_PlayerDetected = true;           
            this.StartCoroutine("AttackCoroutine");
        }

        if (m_PreviousHealth != this.GetComponent<HealthController>().CurrentHealth)
        {
            this.StopCoroutine("AttackCoroutine");
            this.StartCoroutine("HitCoroutine");

            m_PreviousHealth = this.GetComponent<HealthController>().CurrentHealth;
        }

        if(m_Shield != null)
            m_Shield.transform.position = this.transform.position;

        if (m_IsShooting)
        {
            GameObject shoot = GameObject.Instantiate(AttackPrefab, this.transform.position, Quaternion.identity) as GameObject;
            shoot.GetComponent<BulletController>().Shoot(Vector3.right * this.transform.localScale.x);
            shoot.transform.localScale = new Vector3(this.transform.localScale.x*0.5f, 1, 1);
            m_IsShooting = false;
        }
    }

	// Update is called once per frame
    void FixedUpdate()
    {
        float dir = Mathf.Sign(this.transform.localScale.x);

        if (m_IsMoving && Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.x) < InitialSpeed)
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.right * dir * 10, ForceMode2D.Force);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Player")
            this.GetComponent<Animator>().SetTrigger("Kick");
    }

    void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
            this.GetComponent<Animator>().SetTrigger("Idle");
    }

    IEnumerator AttackCoroutine()
    {
        while (m_IsAttacking)
        {            
            m_IsMoving = false;

            for (int i = 0; i < m_BeamShoots; i++)
            {
                m_IsShooting = true;
                this.GetComponent<Animator>().SetTrigger("Punch");
                yield return new WaitForSeconds(m_BeamDelay);
            }

            m_IsMoving = true;
            this.GetComponent<Animator>().SetTrigger("Move");            
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator HitCoroutine()
    {
        m_IsAttacking = m_IsMoving = m_IsShooting = false;
        
        // Animation
        this.GetComponent<Animator>().SetTrigger("Death");        
        yield return new WaitForSeconds(1.0f);

        // Dissappear effect
        GameObject.Instantiate(LightPrefab, this.transform.position - Vector3.up * 0.5f, Quaternion.identity);
        Color color = this.GetComponent<SpriteRenderer>().color;
        iTween.ColorTo(this.gameObject, new Color(color.r, color.g, color.b, 0), 1.0f);
        
        yield return new WaitForSeconds(1.0f);

        // Hide
        this.transform.position = Vector3.zero;
        Vector3 nextPos = GetSpawnPos();
        GameObject.Instantiate(LightPrefab, nextPos - Vector3.up * 0.5f, Quaternion.identity);
        
        yield return new WaitForSeconds(1.0f);

        //Appear
        this.transform.position = nextPos;
        iTween.ColorTo(this.gameObject, new Color(color.r, color.g, color.b, 1.0f), 1.0f);

        if (m_Shield != null)
            DestroyObject(m_Shield);

        m_Shield = GameObject.Instantiate(ShieldPrefab, this.transform.position, Quaternion.identity) as GameObject;
        for (int i = 0; i < m_Shield.transform.childCount; i++ )
            Utils.IgnoreCollisions(this.gameObject, m_Shield.transform.GetChild(i).GetChild(0).gameObject);

        m_IsAttacking = true;

        m_BeamShoots++;
        InitialSpeed = InitialSpeed * MadSpeedFactor;
        m_BeamDelay = m_BeamDelay / MadSpeedFactor;

        this.StartCoroutine("AttackCoroutine");
    }

    public Vector3 GetSpawnPos()
    {
        if (Vector3.Distance(GameObject.Find("Point0").transform.position, this.transform.position) > Vector3.Distance(GameObject.Find("Point1").transform.position, this.transform.position))
            return GameObject.Find("Point0").transform.position;
        else
            return GameObject.Find("Point1").transform.position;
    }
}
