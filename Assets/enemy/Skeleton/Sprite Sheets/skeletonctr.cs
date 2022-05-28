using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class skeletonctr : MonoBehaviour
{
    private Rigidbody2D rigid;

    private Animator ani;

    private float speed;

    public bool isattack;

    private int attacktime;

    public int maxattacktime;

    public int maxHealth = 100;//�������ֵ

    public int currentHealth;//��ǰ����

    private int power;//����ֵ��ֱ������Ӱ�칥����

    public int health { get { return currentHealth; } }

    private bool isfinded;//��������

    private GameObject hero;

    public int walkTimer;

    public int maxwalkTimer;

    public bool walkdir;//true�����ߣ�false������

    public bool wall;

    private bool isdead;

    public int deadtime;

    public int maxdeadtime;

    private float invincibleTime = 1f;//�޵�ʱ��

    private bool isinvincible;

    private float invicibleTimer;//�޵�ʱ���ʱ

    public Image bloodimg;

    private float ratio;

    public GameObject Gold;

    public GameObject bloodbottle;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        speed = 10f;
        isattack = false;
        maxattacktime = 100;
        attacktime = 0;
        currentHealth = maxHealth;
        isfinded = false;
        walkTimer = 0;
        maxwalkTimer = 200;
        walkdir = true;
        isdead = false;
        deadtime = 0;
        maxdeadtime = 75;
        invincibleTime = 1f;
        invicibleTimer = invincibleTime;
        ratio = 1;

    }

    // Update is called once per frame
    void Update()
    {
        changeHealthimg();
        hero = GameObject.Find("hero");
        if (isinvincible)
        {
            invicibleTimer -= Time.deltaTime;
            if (invicibleTimer < 0)
            {
                isinvincible = false;
            }
        }
        if (isdead)
        {
            deadtime++;
            if(deadtime>maxdeadtime)
            {
                ani.SetBool("isdead", true);
                if(deadtime>2*maxdeadtime)
                {
                    var gold = Instantiate(Gold);
                    goldctr gctr = gold.GetComponent<goldctr>();
                    int rand = Random.Range(2, 4);
                    gctr.setnum(rand);
                    int rand2 = Random.Range(1, 100);
                    if (rand2>75)
                    {
                        var bloodBottle = Instantiate(bloodbottle);
                        bloodBottle.transform.position = new Vector3(this.transform.position.x + 0.45f, this.transform.position.y-0.05f , this.transform.position.z);
                    }
                    gold.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.4f, this.transform.position.z);
                    GameObject.Destroy(this.gameObject);//�ݻ������Ϸ����
                }
            }
        }    
        ani.SetBool("movex", walkdir);
        if(Mathf.Abs(hero.transform.position.x-this.transform.position.x+hero.transform.position.y-this.transform.position.y)<5&& Mathf.Abs(hero.transform.position.y - this.transform.position.y) <2)//��������������hero
        {
            isfinded = true;
        }
        else
        {
            isfinded = false;
        }
        if (currentHealth == 0)
        {
            return;
        }
        if (isattack)
        {
            attacktime++;

            if (attacktime > maxattacktime)
            {
                isattack = false;
                ani.SetBool("attack", false);
                attacktime = 0;
            }
            return;
        }
        //else��Ѳ��
        else
        {
            Vector2 trans = new Vector2(this.transform.position.x, this.transform.position.y);
            if(!isfinded)//û�з�������ʱ
            {
                if(walkdir&&!wall)
                {
                    trans.x += 0.1f;
                    walkTimer++;
                    if(walkTimer>maxwalkTimer)
                    {
                        walkTimer = 0;
                        walkdir = false;
                    }
                }
                else if(!walkdir&&!wall)
                {
                    trans.x -= 0.1f;
                    walkTimer++;
                    if (walkTimer > maxwalkTimer)
                    {
                        walkTimer = 0;
                        walkdir = true;
                    }
                }
                
            }
            if(isfinded)
            {
                if(Mathf.Abs(hero.transform.position.y-this.transform.position.y)<1)
                {
                    if (hero.transform.position.x < this.transform.position.x)//С��˵�������
                    {
                        walkdir = false;
                        if(!wall)
                        trans.x -= 0.07f;
                    }
                    else
                    {
                        walkdir = true;
                        if(!wall)
                        trans.x += 0.07f;
                    }
                }
            }
            ani.SetBool("walk", true);
            ani.SetBool("movex", walkdir);
            rigid.MovePosition(trans);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.transform.tag.Equals("hero"))
        {
            isattack = true;
            if (hero.transform.position.x < this.transform.position.x)//С��˵�������
            {
                walkdir = false;
            }
            else
            {
                walkdir = true;
            }
            ani.SetBool("movex", walkdir);
            ani.SetBool("attack", true);
            ani.SetBool("walk", false);
        }
        if (col.transform.tag.Equals("envirment"))
        {
            wall = true;
            walkdir = !walkdir;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.transform.tag.Equals("hero"))
        {
            isattack = true;
            if (hero.transform.position.x < this.transform.position.x)//С��˵�������
            {
                walkdir = false;
            }
            else
            {
                walkdir = true;
            }
            ani.SetBool("movex", walkdir);
            ani.SetBool("attack", true);
            ani.SetBool("walk", false);
            heroctr ctr = col.GetComponent<heroctr>();
            ctr.healthChange(-10);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.transform.tag.Equals("envirment"))
        {
            wall = false;
        }
    }
    public void healthChange(int amount)
    {
        if (currentHealth == 0)
        {
            return;
        }
        if (amount < 0)
        {
            if (isinvincible) return;
            isinvincible = true;
            invicibleTimer = invincibleTime;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);//��������ֵ��Χ
        ratio = (float)currentHealth / ((float)maxHealth);
        if (currentHealth == 0)
        {
            ani.SetBool("dead", true);
            ani.SetBool("walk", false);
            ani.SetBool("attack", false);
            ani.SetBool("idle", false);
            isdead = true;
        }
    }
    private void changeHealthimg()
    {
        bloodimg.fillAmount = Mathf.Lerp(bloodimg.fillAmount, ratio, Time.deltaTime * 5);
    }
}
