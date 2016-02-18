using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputController : MonoBehaviour {

	protected GameObject m_Player;

	// Use this for initialization
	void Start () {
		m_Player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnGUI()
    {
        Color c = transform.FindChild("Action").GetComponent<Image>().color;
        if (Data.Game.Player.CurrentAttack == EAttackType.None)
            transform.FindChild("Action").GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.5f);
        else
            transform.FindChild("Action").GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1.0f);      
    }

	public void OnLeftDown()
	{
		m_Player.GetComponent<PlayerMotionController> ().MoveLeft ();
	}

	public void OnRightDown()
	{
		m_Player.GetComponent<PlayerMotionController> ().MoveRight ();
	}

	public void OnMoveEnd()
	{
		m_Player.GetComponent<PlayerMotionController> ().Stop ();
	}

	public void OnJumpDown()
	{
		m_Player.GetComponent<PlayerMotionController> ().BeginJump ();
		m_Player.GetComponent<PlayerAnimationController> ().OnJump ();
	}

	public void OnJumpUp()
	{
		m_Player.GetComponent<PlayerMotionController> ().EndJump ();
	}

	public void OnAction()
	{
        m_Player.GetComponent<PlayerAttackController> ().ThrowAttack ();
		m_Player.GetComponent<PlayerAnimationController> ().OnAttack ();
	}
}
