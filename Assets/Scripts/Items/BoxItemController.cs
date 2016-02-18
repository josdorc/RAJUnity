using UnityEngine;
using System.Collections;

public class BoxItemController : MonoBehaviour {

	public float MotionEffect;
	public float MotionTime;
	public Sprite OnSprite;
	public Sprite OffSprite;
	public int NumItems;
    public bool Infinite;
	public EItemType ItemType;
    public GameObject StarPrefab;
    public GameObject CoinPrefab;
	public GameObject BubblePrefab;

	protected bool m_IsClosed;
	protected Vector3 m_StartPos;

	// Use this for initialization
	void Start () 
	{
		m_StartPos = this.transform.position;
		this.GetComponent<SpriteRenderer> ().sprite = OnSprite;
	}
	

	void OnTriggerEnter2D(Collider2D hit)
	{
		if(hit.gameObject.tag == "Player")
            Open();
	}

    public void Open()
    {
        if (!m_IsClosed)
        {
            iTween.MoveFrom(this.gameObject, m_StartPos + Vector3.up * MotionEffect, MotionTime);
            this.StartCoroutine(CloseCoroutine());
        }
    }

	IEnumerator CloseCoroutine()
	{
        if (!Infinite)
        {
            NumItems--;
            m_IsClosed = true;
        }

		yield return new WaitForSeconds (MotionTime);
		this.GetComponent<SpriteRenderer> ().sprite = NumItems > 0 ? OnSprite : OffSprite;

		GameObject item = null;
		switch(ItemType)
		{
			case EItemType.Star:
				item = GameObject.Instantiate(StarPrefab, this.transform.position + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
			break;
            case EItemType.Coin:
                item = GameObject.Instantiate(CoinPrefab, this.transform.position + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
            break;
			default:
            	item = GameObject.Instantiate (BubblePrefab, this.transform.position + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
				item.GetComponentInChildren<BubbleItemController>().SetItem(ItemType);
			break;
		}

         m_IsClosed = (NumItems == 0);

         this.transform.position = m_StartPos;
	}
}
