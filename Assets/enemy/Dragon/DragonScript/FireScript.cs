using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public  bool control = false;

    public Vector3 dest_pos;

    public Vector3 src_pos;

    private Vector3 direction;

    private float speed;

    public GameObject fireBallPos;
    // Start is called before the first frame update
    void Start()
    {

        speed = 7f;
    }
    private void Awake()
    {
        speed = 7f;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(fireBallPos!=null)
        {
            src_pos = fireBallPos.transform.position;
            dest_pos = GameObject.FindGameObjectWithTag("hero").transform.position;
            direction = (dest_pos - src_pos).normalized;
            if (this.transform.position != dest_pos)
                this.transform.position += direction * speed * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.transform.tag.Equals("envirment"))
        {
            GameObject.Destroy(this.gameObject);
        }
        if(col.transform.tag.Equals("hero"))
        {
            heroctr hctr = col.GetComponent<heroctr>();
            hctr.healthChange(-30);
            GameObject.Destroy(this.gameObject);
        }
    }
}
