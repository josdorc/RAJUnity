using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {

    public GameObject LostPrefab;
    public Sprite ShurikenSprite;
    public GameObject ShurikenPrefab;
    public Sprite BombSprite;
    public GameObject BombPrefab;
    public Sprite FireBeamSprite;
    public GameObject FireBeamPrefab;

    protected int m_PreviousHealth = 0;

	// Use this for initialization
	void Start () {

   	}
	
	// Update is called once per frame
	void Update () 
    {
        if(m_PreviousHealth == 0)
            m_PreviousHealth = this.GetComponent<HealthController>().CurrentHealth;

        int health = this.GetComponent<HealthController>().CurrentHealth;        
        if(m_PreviousHealth > health)
        {
            LostAttack(this.transform.position + Vector3.up);
            m_PreviousHealth = health;
        }

	    if(Input.GetButtonDown("Grab"))
	        ThrowAttack();        
	}

    public void ThrowAttack()
    {
        GameObject attack = null;

        switch(Data.Game.Player.CurrentAttack)
        {          
            case EAttackType.Shuriken:
                attack = GameObject.Instantiate(ShurikenPrefab, this.transform.position, Quaternion.identity) as GameObject;
                attack.GetComponent<BulletController>().Shoot(Vector3.right * this.transform.localScale.x * 10);
                attack.transform.localScale = new Vector3(this.transform.localScale.x, 1, 1);
            break;
            case EAttackType.Bomb:
                Vector3 pos = this.transform.position + Vector3.right * this.transform.localScale.x * 0.5f;
                attack = GameObject.Instantiate(BombPrefab, pos, Quaternion.identity) as GameObject;                    
            break;
            case EAttackType.FireBeam:
                attack = GameObject.Instantiate(FireBeamPrefab, this.transform.position, Quaternion.identity) as GameObject;
                attack.GetComponent<BulletController>().Shoot(Vector3.right * this.transform.localScale.x * 10);
                attack.transform.localScale = new Vector3(this.transform.localScale.x, 1, 1);
            break;
        }
    }

    public Sprite GetAttackSprite(EAttackType type)
    {
        Sprite sprite = null;
        switch (type)
        {
            case EAttackType.Shuriken: 
                sprite = ShurikenSprite; 
                break;
            case EAttackType.Bomb:
                sprite = BombSprite;
                break;
            case EAttackType.FireBeam:
                sprite = FireBeamSprite;
                break;
        }

        return sprite;
    }

    public void LostAttack(Vector3 position)
    {
        if (Data.Game.Player.CurrentAttack != EAttackType.None)
        {
            GameObject lost = GameObject.Instantiate(LostPrefab, this.transform.position, Quaternion.identity) as GameObject;
            lost.GetComponent<SpriteRenderer>().sprite = GetAttackSprite(Data.Game.Player.CurrentAttack);
            iTween.MoveTo(lost, position, 1.0f);
            iTween.ColorTo(lost, Color.black, 1.0f);

            Data.Game.Player.CurrentAttack = EAttackType.None;
        }
    }
}
