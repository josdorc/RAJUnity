using UnityEngine;
using System;
using System.Collections;

public class CheckPointController : MonoBehaviour {

    public Action OnCheckPoint;
 
    bool isInCheckPoint = false;     

	// Use this for initialization
	void Start () {

        this.transform.FindChild("Light").GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(!isInCheckPoint && hit.gameObject.tag == "Player")
        {
            isInCheckPoint = true;

            this.StartCoroutine(OnTeleportCoroutine(hit.gameObject));
        }
    }

    void OnTriggerExit2D(Collider2D hit)
    {
        if (isInCheckPoint && hit.gameObject.tag == "Player")
            isInCheckPoint = false;
    }

    IEnumerator OnTeleportCoroutine(GameObject player)
    {
        this.transform.FindChild("Light").GetComponent<SpriteRenderer>().enabled = true;
        this.transform.FindChild("Light").GetComponent<Animator>().SetTrigger("Start");

        player.GetComponent<PlayerMotionController>().DisableMotion(true);
        player.transform.position = this.transform.position;
        
        yield return new WaitForSeconds(2);

        player.GetComponent<PlayerStatusController>().SetCheckPoint(Application.loadedLevelName);

        OnCheckPoint();

        player.GetComponent<PlayerMotionController>().EnableMotion();
        this.transform.FindChild("Light").GetComponent<SpriteRenderer>().enabled = false;
    }
}
