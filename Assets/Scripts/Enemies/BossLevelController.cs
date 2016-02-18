using UnityEngine;
using System.Collections;

public class BossLevelController : MonoBehaviour {
    public GameObject Boss;
    public GameObject SecretItem;
    public Transform ItemStartPos;
    public Transform ItemEndPos;
    protected GameObject m_Item;
    protected bool m_PlayerDetected;
    protected bool m_SecretFound;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update()
    {   
        if (!m_SecretFound && Boss == null)
        {
            m_Item = GameObject.Instantiate(SecretItem, ItemStartPos.position, Quaternion.identity) as GameObject;
            iTween.MoveTo(m_Item, iTween.Hash("position", ItemEndPos.position, "time", 3.0f, "delay", 2.0f));
            m_SecretFound = true;
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(!m_PlayerDetected && hit.gameObject.tag == "Player")
        {
            Boss.GetComponent<BossLookTarget>().SetTarget();
            m_PlayerDetected = true;
        }
    }
}
