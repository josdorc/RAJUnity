using UnityEngine;
using System.Collections;

public class SwitchButtonController : MonoBehaviour {
    public Sprite SwitchOff;
    public Sprite SwitchOn;
    public GameObject SwitchBlock;
    public GameObject DestroyPrefab;
    public SwitchButtonController MirrorSwitch;

    public float ActivationTime;
    
    protected float m_CurrentTime;
    protected bool m_IsPressed;

	// Use this for initialization
	void Start () {

        this.GetComponents<BoxCollider2D>()[0].enabled = true;
        this.GetComponents<BoxCollider2D>()[1].enabled = false;        
	}
	
	// Update is called once per frame
	void Update () {
	    if(m_IsPressed)
        {
            m_CurrentTime += Time.deltaTime;

            if (m_CurrentTime > ActivationTime)
            {
                for (int i = 0; i < SwitchBlock.transform.childCount; i++)
                {
                    Transform block = SwitchBlock.transform.GetChild(i);
                    block.GetComponent<SpriteRenderer>().enabled = true;
                    block.GetComponent<BoxCollider2D>().enabled = true;
                }

                this.SetOff();   
                if(MirrorSwitch != null)
                    MirrorSwitch.SetOff();
            }
        }
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Player" && !m_IsPressed)
        {
            this.SetOn();

            if (MirrorSwitch != null)
                MirrorSwitch.SetOn();

            this.StartCoroutine(OnSwitchCoroutine(hit.gameObject));
        }
    }

    IEnumerator OnSwitchCoroutine(GameObject player)
    {
        player.GetComponent<PlayerMotionController>().DisableMotion(true);
        GameObject camera = GameObject.FindWithTag("MainCamera");
        
        camera.GetComponent<FollowTarget>().enabled = false;

        Vector3 switchPos = new Vector3(SwitchBlock.transform.position.x, SwitchBlock.transform.position.y, camera.transform.position.z);
        iTween.MoveTo(camera, switchPos, 2.0f);

        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < SwitchBlock.transform.childCount; i++)
        {
            Transform block = SwitchBlock.transform.GetChild(i);
            block.GetComponent<SpriteRenderer>().enabled = false;
            block.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Instantiate(DestroyPrefab, block.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(1.0f);

        player.GetComponent<PlayerMotionController>().EnableMotion();
        camera.GetComponent<FollowTarget>().enabled = true;

        m_CurrentTime = 0;
    }

    public void SetOn()
    {
        this.GetComponent<SpriteRenderer>().sprite = SwitchOn;
        this.GetComponents<BoxCollider2D>()[0].enabled = false;
        this.GetComponents<BoxCollider2D>()[1].enabled = true;

        m_IsPressed = true;
    }

    public void SetOff()
    {
        this.GetComponent<SpriteRenderer>().sprite = SwitchOff;
        this.GetComponents<BoxCollider2D>()[0].enabled = true;
        this.GetComponents<BoxCollider2D>()[1].enabled = false;

        m_IsPressed = false;
        m_CurrentTime = 0;
    }
}
