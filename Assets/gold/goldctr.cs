using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldctr : MonoBehaviour
{
    public int goldnum;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.transform.tag.Equals("hero"))
        {
            if (untakabletimer < maxtaketime) return;
            heroctr ctr = g.GetComponent<heroctr>();
            ctr.goldChange(goldnum);
            GameObject.Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.transform.tag.Equals("hero"))
        {
            if (untakabletimer < maxtaketime) return;
            heroctr ctr = g.GetComponent<heroctr>();
            ctr.goldChange(goldnum);
            GameObject.Destroy(this.gameObject);
        }
    }
    public void setnum(int amount)
    {
        this.goldnum = amount;
    }
}
