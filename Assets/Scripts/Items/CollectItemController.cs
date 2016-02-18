using UnityEngine;
using System;
using System.Collections;

public class CollectItemController : MonoBehaviour {

	public GameObject CollectedPrefab;
	public EItemType ItemType;
    public bool AutoCollect;
    public string Message;

	protected bool m_IsCollected = false;
    protected float m_AngularSpeed = 45;
	protected float m_LinealSpeed = 2;	
	protected float m_CollectedDistance = 0.5f;    
	
	// Use this for initialization
	void Start () {       

        switch(ItemType)
        {
            case EItemType.Gem:
                m_IsCollected = Data.Game.CurrentLevel.GemFound;
                break;
            case EItemType.MapForest:
                m_IsCollected = Data.Game.Player.HasForestMap;
                break;
            case EItemType.KeyYellow:
                m_IsCollected = Data.Game.Player.YellowKey;
                break;
        }

        if (m_IsCollected)
            this.GetComponent<SpriteRenderer>().color = Color.white * 0.3f;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (AutoCollect)
        {
            GameObject player = GameObject.FindWithTag("Player");

            this.transform.Rotate(Vector3.forward, m_AngularSpeed * Time.deltaTime);

            float fraction = m_LinealSpeed * Time.deltaTime / Vector3.Distance(transform.position, player.transform.position);
            transform.position = Vector3.Lerp(transform.position, player.transform.position, fraction);

            m_AngularSpeed += m_AngularSpeed / 10;
            m_LinealSpeed += m_LinealSpeed / 10;

            if (Vector3.Distance(this.transform.position, player.transform.position) <= m_CollectedDistance)
            {
                if (!m_IsCollected)
                {
                    player.GetComponent<PlayerStatusController>().SetItem(this.transform, ItemType);

                    if (Message != string.Empty)
                        GameObject.Find("MessageBox").GetComponent<MessageBoxController>().ShowDialog("Congratulations", Message);
                }

                GameObject.Instantiate(CollectedPrefab, this.transform.position, Quaternion.identity);
                DestroyObject(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
            AutoCollect = true;
    }
}
