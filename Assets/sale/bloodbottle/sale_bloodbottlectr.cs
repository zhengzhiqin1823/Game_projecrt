using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class sale_bloodbottlectr : MonoBehaviour
{
    public  GameObject bloodbottle;

    public Canvas cans;

    public Text pricetext;

    public int price;
    // Start is called before the first frame update
    void Start()
    {
        bloodbottle.GetComponent<BoxCollider2D>().enabled = false;
        price = 5;
        string s = "X ";
        s += price;
        pricetext.text = s;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.transform.tag.Equals("hero"))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                heroctr ctr = g.GetComponent<heroctr>();
                if(ctr.goldnum>=this.price)
                {
                    bloodbottle.GetComponent<BoxCollider2D>().enabled =true;
                    bloodbottle.GetComponent<bloodbottlectr>().untakabletimer = 100;
                    cans.GetComponent<Canvas>().enabled = false;
                    ctr.goldChange(-1 * price);

                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject g = collision.gameObject;
        if (g.transform.tag.Equals("hero"))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                heroctr ctr = g.GetComponent<heroctr>();
                if (ctr.goldnum >= this.price)
                {
                    bloodbottle.GetComponent<BoxCollider2D>().enabled = true;
                    cans.GetComponent<Canvas>().enabled = false;
                    ctr.goldChange(-1 * price);
                }
            }
        }
    }
}
