using UnityEngine;
using System.Collections;

public class PlayerStatusController : MonoBehaviour {

	public float DeathTime = 2;
    public bool ResetData;
	protected bool m_IsDeath = false;

	// Use this for initialization
	void Start () {

        if (ResetData)
            Data.ResetGame();

        if(Data.Game.HasWarp)
        {            
           this.transform.position = Data.Game.WarpPos;
           Data.Game.HasWarp = false;
         
            if (Data.Game.WarpFlip)
               this.GetComponent<PlayerMotionController>().Flip();
        }
        else if (Data.Game.CheckPointLevel != null && Data.Game.CheckPointLevel != string.Empty)
        {
            Debug.Assert(Data.Game.CheckPointLevel == Application.loadedLevelName);
            this.transform.position = GameObject.Find("CheckPoint").transform.position;
        }
        else
        {
            // set default start position
        }

        this.GetComponent<HealthController>().CurrentHealth = Data.Game.Player.Health;
        
        //Set current level visited
        Data.Game.SetLevelVisited(Application.loadedLevelName);
	}

	// Update is called once per frame
	void Update () {

		if(this.GetComponent<HealthController>().CurrentHealth == 0 && !m_IsDeath)
		{
			this.StartCoroutine(OnDeathCoroutine());
			m_IsDeath = true;
		}
	}

    IEnumerator OnDeathCoroutine()
    {
        this.GetComponent<PlayerMotionController>().DisableMotion(false);

        yield return new WaitForSeconds(DeathTime);

        Application.LoadLevel("GameOver");
    }

    public void SetItem(Transform item, EItemType type)
    {
        switch (type)
        {
            case EItemType.Star:
                if (Data.Game.Player.Stars < Constants.MAX_ENERGY * Constants.ENERGY_RATIO)
                     Data.Game.Player.Stars++;
                else
                    Data.Game.Player.Stars = Constants.MAX_ENERGY * Constants.ENERGY_RATIO;                
                break;
            case EItemType.Coin:
                if (Data.Game.Player.Coins < 999)
                    Data.Game.Player.Coins++;
                else
                    Data.Game.Player.Coins = 999;                
                break;
            case EItemType.Gem:
                    Data.Game.Player.Gems++;
                    Data.Game.CurrentLevel.GemFound = true;
                break;
            case EItemType.Heart:
                    this.GetComponent<HealthController>().AddHealth();
                    Data.Game.Player.Health = this.GetComponent<HealthController>().CurrentHealth;
                break;
            case EItemType.MapForest:
                Data.Game.SetZoneVisible("Forest");
                Data.Game.Player.HasForestMap = true;
                break;
            case EItemType.KeyYellow:
                Data.Game.Player.YellowKey = true;
                break;
            case EItemType.TenCoins:
                if (Data.Game.Player.Coins + 10 < 999)
                    Data.Game.Player.Coins += 10;
                else
                    Data.Game.Player.Coins = 999; 
                break;
            case EItemType.Shuriken:
                Data.Game.Player.CurrentAttack = EAttackType.Shuriken;
                break;
            case EItemType.Bomb:
                Data.Game.Player.CurrentAttack = EAttackType.Bomb;
                break;
            case EItemType.FireBeam:
                Data.Game.Player.CurrentAttack = EAttackType.FireBeam;
                break;
        }
    }

    public void SetCheckPoint(string level)
    {
        Data.Game.CheckPointLevel = level;
    }
}

