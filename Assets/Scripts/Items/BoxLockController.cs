using UnityEngine;
using System.Collections;

public class BoxLockController : MonoBehaviour {
    public LockColor Color;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D hit)
    {
        bool destroy = false;
        if(hit.gameObject.tag == "Player")
        {
            switch(Color)
            {
                case LockColor.Yellow:
                    destroy = Data.Game.Player.YellowKey;
                    break;
            }
        }

        if(destroy)
        {
            iTween.ScaleTo(this.gameObject, Vector3.zero, 1.0f);
            DestroyObject(this.gameObject, 1.0f);                        
        }
    }

    
}
