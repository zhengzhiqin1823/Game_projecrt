using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestctr : MonoBehaviour
{
    public Animator ani;

    public int opentimer;

    public bool isopened;

    public int max_opentime;

    public GameObject Gold;

    public bool opened;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        opentimer = 0;
        max_opentime = 100;
        isopened = false;
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetBool("open", isopened);
        if(isopened)
        {
            opentimer++;
            if(opentimer==max_opentime)
            {
                ani.SetBool("opened", true);
                isopened = false;
                var gold = Instantiate(Gold);
                gold.transform.position = this.transform.position;
                goldctr ctr = gold.GetComponent<goldctr>();
                ctr.setnum(10);
                opened = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (opened) return;
        GameObject g = collision.gameObject;
        if (g.transform.tag.Equals("hero"))
        {
            isopened = true;
        }
    }
}
