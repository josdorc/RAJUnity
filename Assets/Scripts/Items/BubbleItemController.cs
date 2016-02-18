using UnityEngine;
using System;
using System.Collections;

public class BubbleItemController : MonoBehaviour {
	public float FlySpeed;
	public float Amplitude;
	public GameObject DestroyPrefab;
	public ItemDefinition[] Items;
	protected ItemDefinition m_Item;

	protected bool m_ReadyToFly = false;

	// Use this for initialization
	void Start()
	{
		this.transform.localScale = Vector3.one * 0.1f;
		iTween.ScaleTo (this.gameObject, iTween.Hash (
			"scale", Vector3.one, 
			"time", 1.0f,
			"oncomplete",
			"OnReadyToFly"));
	}

	// Update is called once per frame
	void Update () {

		if(m_ReadyToFly)
			this.transform.position += Vector3.up * FlySpeed * Time.deltaTime + Amplitude * Vector3.right * Mathf.Cos (Time.time);
	}

	void OnReadyToFly()
	{
		iTween.ScaleTo (this.gameObject, iTween.Hash (
			"y", 0.9f, 
			"speed", 0.2f,
			"looptype", iTween.LoopType.pingPong));

		m_ReadyToFly = true;
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if(m_Item.ItemPrefab != null)
		{
			GameObject.Instantiate (DestroyPrefab, this.transform.position, Quaternion.identity);
			GameObject.Instantiate (m_Item.ItemPrefab, this.transform.position, Quaternion.identity);
		}

		DestroyObject (this.gameObject);
	}
	
	public void SetItem(EItemType itemType) {

		for(int i=0; i < Items.Length; i++)
		{
			if(Items[i].Type == itemType)
			{
				m_Item = Items[i];
				this.transform.FindChild("Item").GetComponent<SpriteRenderer>().sprite = m_Item.ItemSprite;

                if (itemType == EItemType.Gem && Data.Game.CurrentLevel.GemFound)
                    this.transform.FindChild("Item").GetComponent<SpriteRenderer>().color = Color.white * 0.5f;
				break;
			}
		}
	}
}


