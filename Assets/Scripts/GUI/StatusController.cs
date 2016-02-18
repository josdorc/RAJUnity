using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusController : MonoBehaviour {

	public Sprite HeartFull;
	public Sprite HeartMiddle;
	public Sprite HeartEmpty;
    public Sprite EnergyBarOff;
    public Sprite EnergyBarOn;
    public Sprite EnergyBarEnd;
    public Sprite NoAttack;    
    public Sprite Shuriken;
    public Sprite Bomb;
    public Sprite FireBeam;

	protected const int MAX_HEARTS = 3;
	protected int m_PreviousHealth = 0;
    protected GameObject m_Player;


	// Use this for initialization
	void Start () {

        m_Player = GameObject.FindWithTag("Player");

		for(int i=0; i < MAX_HEARTS; i++)
			transform.FindChild("Heart" + i.ToString()).GetComponent<Image>().enabled = false;

	}
	
	// Update is called once per frame
	void OnGUI () 
	{
		this.SetHearts ();
		this.SetItems ();
        this.SetEnergy();
	}

	public void SetHearts()
	{
		int health = m_Player.GetComponent<HealthController>().CurrentHealth;

		if(m_PreviousHealth != health)
		{
            int hearts = m_Player.GetComponent<HealthController>().Health / 2;
		    
			for (int i=0; i < hearts; i++)
			{
				transform.FindChild("Heart" + i.ToString()).GetComponent<Image>().enabled = true;
				transform.FindChild ("Heart" + i.ToString ()).GetComponent<Image> ().sprite = HeartEmpty;
			}

			if(health > 0)
			{
				for(int i = 1; i <= health; i++)
				{
					int heart = (i-1) / 2;
					if(i % 2 == 0)
						transform.FindChild ("Heart" + heart.ToString ()).GetComponent<Image> ().sprite = HeartFull;
					else
						transform.FindChild ("Heart" + heart.ToString ()).GetComponent<Image> ().sprite = HeartMiddle;
				}
			}

			m_PreviousHealth = health;
		}
	}

    public void SetEnergy()
    {
        int energy = Constants.MAX_ENERGY;
        int stars = Data.Game.Player.Stars;
        int ratio = Constants.ENERGY_RATIO;

        int bars = (stars % ratio == 0 ) ? stars / ratio : stars / ratio + 1;

        Transform energyBar = transform.FindChild("Battery");

        for(int i=0; i <= 10; i++)
        {
            if (i <= energy)
                energyBar.FindChild(string.Format("Bar{0}", i)).GetComponent<Image>().enabled = true;
            else
                energyBar.FindChild(string.Format("Bar{0}", i)).GetComponent<Image>().enabled = false;               
            
            if(i < bars)
                energyBar.FindChild(string.Format("Bar{0}", i)).GetComponent<Image>().sprite = EnergyBarOn;
            else
                energyBar.FindChild(string.Format("Bar{0}", i)).GetComponent<Image>().sprite = EnergyBarOff;

            if(i == energy)
                energyBar.FindChild(string.Format("Bar{0}", energy)).GetComponent<Image>().sprite = EnergyBarEnd;
        }

    }

    public void SetItems()
    {
        int coins = Data.Game.Player.Coins;
        transform.FindChild("CoinsValue").GetComponent<Text>().text = coins.ToString();

        switch (Data.Game.Player.CurrentAttack)
        {
            case EAttackType.None:
                transform.FindChild("AttackButton").GetComponent<Image>().sprite = NoAttack;
                break;
            case EAttackType.Shuriken:
                transform.FindChild("AttackButton").GetComponent<Image>().sprite = Shuriken;
                break;
            case EAttackType.Bomb:
                transform.FindChild("AttackButton").GetComponent<Image>().sprite = Bomb;
                break;
            case EAttackType.FireBeam:
                transform.FindChild("AttackButton").GetComponent<Image>().sprite = FireBeam;
                break;
        }        
    }
           
}
