using UnityEngine;
using System.Collections;

public class PlayerWarpController : MonoBehaviour
{
    protected string m_NextScene;
    protected Vector3 m_NextPos;
    protected bool m_WarpFlip;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Warp(Vector3 warpPos, Vector3 warpDir, string scene, Vector3 pos)
    {
        this.GetComponent<PlayerMotionController>().DisableMotion(true);

        m_NextScene = scene;
        m_NextPos = pos;
        m_WarpFlip = this.transform.localScale.x < 0;

        this.transform.position = warpPos + warpDir;
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", warpPos,
            "time", 1.0f,
            "oncomplete", "OnWarp"
            ));
    }

    public void OnWarp()
    {
        Data.Game.HasWarp = true;
        Data.Game.WarpPos = m_NextPos;
        Data.Game.WarpFlip = m_WarpFlip;
        Data.Game.Player.Health = this.GetComponent<HealthController>().CurrentHealth;

        Application.LoadLevel(m_NextScene);
    }
}