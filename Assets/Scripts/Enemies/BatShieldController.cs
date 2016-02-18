using UnityEngine;
using System.Collections;

public class BatShieldController : MonoBehaviour {
    public float AngularSpeed;
    public float GrowSpeed;
    public float Amplitude;

    protected float m_Angle;
    protected float m_Amplitude;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).GetComponent<Animator>().SetTrigger("Attack");            
	}
	
	// Update is called once per frame
	void Update () {

        m_Angle += AngularSpeed * Time.deltaTime;
        
        if(m_Amplitude < Amplitude)
            m_Amplitude += GrowSpeed * Time.deltaTime;

        for (int i = 0; i < this.transform.childCount; i++)
            this.transform.GetChild(i).position = this.transform.position + m_Amplitude * new Vector3(Mathf.Cos((90 * i + m_Angle) * Mathf.Deg2Rad), Mathf.Sin((90 * i + m_Angle)*Mathf.Deg2Rad), 0);       
	}


}
