using UnityEngine;
using System;
using System.Collections;

public class HealthController : MonoBehaviour {

    public int Health;
	public int CurrentHealth;
	public GameObject DestroyPrefab;
    public GameObject ItemPrefab;
    public float ItemChance;
	public bool IsPlayer;
	public string[] DangerTags;
	public Color HitColor = Color.white;
	public float HitTime;

	protected bool m_IsRecovered = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddHealth()
	{
		if (CurrentHealth + 1 <= Health)
			CurrentHealth++;
	}

	public void SetDamage(int damage, string tag)
	{
        if(!Utils.ExistStringInArray(DangerTags, tag))
			return;
	
		if(m_IsRecovered)
		{
			CurrentHealth = (CurrentHealth - damage > 0) ? CurrentHealth - damage : 0;
			this.StartCoroutine (OnDamageCoroutine());

			if(CurrentHealth == 0 && !IsPlayer)
			{
				DestroyObject(this.gameObject);

				if(DestroyPrefab != null)
					GameObject.Instantiate(DestroyPrefab, this.transform.position, this.transform.rotation);
                if (ItemPrefab != null && UnityEngine.Random.Range(0,100) < ItemChance * 100)
                    GameObject.Instantiate(ItemPrefab, this.transform.position, Quaternion.identity);
			}
		}
	}

    public void Kill(string tag)
    {
        this.SetDamage(CurrentHealth, tag);
    }

	IEnumerator OnDamageCoroutine()
	{
		m_IsRecovered = false;

		yield return new WaitForSeconds (HitTime);

		m_IsRecovered = true;
	}
}
