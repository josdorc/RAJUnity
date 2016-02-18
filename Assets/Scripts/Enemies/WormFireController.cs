using UnityEngine;
using System.Collections;

public class WormFireController : MonoBehaviour {
    
    public GameObject FirePrefab;
    public int NumFires;
    public float Speed;
    public float Pause;
    public float MotionDelay;
    public float ShootLeftAngle;
    public float ShootRightAngle;
    public float ShootSpeed;
    public float ShootDelay;
    public Vector3 Motion;
    
    protected Vector3 m_StartPosition;
    protected GameObject m_Player;

    // Use this for initialization
    void Start()
    {
        m_StartPosition = this.transform.position;
        m_Player = GameObject.FindWithTag("Player");

        Invoke("OnMoveBegin", MotionDelay);        
   }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMoveBegin()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", m_StartPosition + Motion,
            "speed", Speed,
            "delay", Pause,
            "oncomplete",
            "OnShootBegin"));
    }

    void OnShootBegin()
    {
        this.GetComponent<Animator>().SetTrigger("Shoot");
        this.StartCoroutine(ShootCoroutine());
    }

    void OnMoveEnd()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", m_StartPosition,
            "speed", Speed,
            "delay", Pause,
            "oncomplete",
            "OnMoveBegin"));
    }

    IEnumerator ShootCoroutine()
    {
        for(int i=0; i < NumFires; i++)
        {
            yield return new WaitForSeconds(ShootDelay);

            GameObject fire = GameObject.Instantiate(FirePrefab, this.transform.FindChild("Mouth").position, Quaternion.identity) as GameObject;
            
            Vector3 dir = Vector3.zero;
            if (m_Player.transform.position.x < this.transform.position.x)
                dir = Quaternion.AngleAxis(ShootLeftAngle, Vector3.forward) * Vector3.right;
            else
                dir = Quaternion.AngleAxis(ShootRightAngle, Vector3.forward) * Vector3.right;

            fire.GetComponent<BulletController>().Shoot(dir * ShootSpeed);
            fire.GetComponent<BulletController>().Rotate(180.0f);
        }

        this.GetComponent<Animator>().SetTrigger("Move");
        OnMoveEnd();        
    }
}
