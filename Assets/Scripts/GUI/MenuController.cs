using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public Sprite KeyYellow;
    protected GameObject m_Player;
    protected GameObject m_CheckPoint;


    // Use this for initialization  
    void Start()
    {

        this.GetComponent<RectTransform>().localScale = Vector3.zero;

        m_Player = GameObject.FindWithTag("Player");
        m_CheckPoint = GameObject.Find("CheckPoint");

        if (m_CheckPoint != null)
            m_CheckPoint.GetComponent<CheckPointController>().OnCheckPoint += OnCheckPoint;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnStatusButton()
    {
        OnStatusButton(false);
    }

    public void OnStatusButton(bool IsCheckPoint)
    {
        Time.timeScale = 0;
        this.GetComponent<RectTransform>().localScale = Vector3.one;
        transform.parent.FindChild("Input").GetComponent<RectTransform>().localScale = Vector3.zero;
        transform.parent.FindChild("Status").GetComponent<RectTransform>().localScale = Vector3.zero;

        transform.FindChild("Map").GetComponent<MapController>().SetupMap();

        Transform statusTop = transform.FindChild("StatusTop");

        //string healthStatus = string.Format("{0}/{1}", Data.Game.Player.Health, Constants.MAX_HEALTH);
        //statusTop.FindChild("HealthStatus").GetComponent<Text>().text = healthStatus;

        //string energyStatus = string.Format("{0}/{1}", Data.Game.Player.Stars, Constants.MAX_ENERGY);
        //statusTop.FindChild("EnergyStatus").GetComponent<Text>().text = energyStatus;

        //int coins = Data.Game.Player.Coins;
        //statusTop.FindChild("CoinStatus").GetComponent<Text>().text = coins.ToString();

        string gemStatus = string.Format("{0}/{1}", Data.Game.Player.Gems, Constants.MAX_GEMS);
        statusTop.FindChild("GemStatus").GetComponent<Text>().text = gemStatus;

        if (Data.Game.Player.YellowKey)
            statusTop.FindChild("KeyYellow").GetComponent<Image>().sprite = KeyYellow;

        if (IsCheckPoint)
        {
            transform.FindChild("Teleport").GetComponent<RectTransform>().localScale = Vector3.one * 0.3f;
            transform.FindChild("Teleport").GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            transform.FindChild("Save").GetComponent<RectTransform>().localScale = Vector3.one * 0.3f;            
        }
        else
        {
            transform.FindChild("Teleport").GetComponent<RectTransform>().localScale = Vector3.zero;
            transform.FindChild("Save").GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }

    public void OnCheckPoint()
    {
        OnStatusButton(true);
    }

    public void OnCloseEvent()
    {
        Time.timeScale = 1;
        this.GetComponent<RectTransform>().localScale = Vector3.zero;
        transform.parent.FindChild("Input").GetComponent<RectTransform>().localScale = Vector3.one;
        transform.parent.FindChild("Status").GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void OnSaveButton()
    {
        Data.SaveGame();
        OnCloseEvent();
    }

    public void OnTeleportButton()
    {
        Time.timeScale = 1;
        string level = this.GetComponentInChildren<MapController>().SelectedLevel;
        if (level != null && level != string.Empty)
        {
            Data.Game.CheckPointLevel = level;
            Application.LoadLevel(level);
        }
    }
}
