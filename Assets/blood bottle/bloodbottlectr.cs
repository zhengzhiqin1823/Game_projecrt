using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodbottlectr : MonoBehaviour
{
    public int untakabletimer;

    public int maxtaketime;
    private void Awake()
    {
        untakabletimer = 0;
        maxtaketime = 30;
    }
    private void Update()
    {
        untakabletimer++;
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject g = collision.gameObject;
        if(g.transform.tag.Equals("hero"))
        {
            if (untakabletimer < maxtaketime) return;
            heroctr ctr = g.GetComponent<heroctr>();
            ctr.healthChange(50);
            GameObject.Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.transform.tag.Equals("hero"))
        {
            if (untakabletimer < maxtaketime) return;
            heroctr ctr = g.GetComponent<heroctr>();
            if(ctr.currentHealth<ctr.maxHealth)
            {
                ctr.healthChange(50);
                GameObject.Destroy(this.gameObject);
            }
           
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.transform.tag.Equals("hero"))
        {
            if (untakabletimer < maxtaketime) return;
            heroctr ctr = g.GetComponent<heroctr>();
            if (ctr.currentHealth < ctr.maxHealth)
            {
                ctr.healthChange(50);
                GameObject.Destroy(this.gameObject);
            }

        }
    }
}
