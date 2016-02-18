using UnityEngine;

public class EnemyObstacleDetector : MonoBehaviour
{
    public bool CanDetectHoles = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(DetectObstacle())
            Flip();

        if (CanDetectHoles && DetectHoles())
            Flip();
    }

    bool DetectObstacle()
    {
        float distance = this.GetComponent<Collider2D>().bounds.size.x;
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, -this.transform.right * this.transform.localScale.x, distance);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag != "Player" && hits[i].transform.gameObject != this.gameObject && !hits[i].collider.isTrigger)
                return true;
        }

        return false;
    }

    bool DetectHoles()
    {
        float width = this.GetComponent<Collider2D>().bounds.size.x;
        float height = this.GetComponent<Collider2D>().bounds.size.y;

        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position - this.transform.right * this.transform.localScale.x * width, -this.transform.up, height);

        return (hits.Length == 0);        
    }


    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}