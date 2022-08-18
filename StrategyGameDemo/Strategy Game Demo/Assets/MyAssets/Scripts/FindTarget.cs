using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FindTarget : Damageable
{
    private AIDestinationSetter destSetter;
    private AIPath aiPath;

    private Transform dest;
    public bool selected = false;

    public GameObject selectCircle;

    public int damage = 1, radius = 5;

    public EnemyBarrackManager[] enemies;

    [SerializeField] private Animator soldierAnim;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        maxHealth = health;

        destSetter = this.gameObject.GetComponent<AIDestinationSetter>();
        aiPath = this.gameObject.GetComponent<AIPath>();

        destSetter.target = this.gameObject.transform;
        dest = GameObject.Find("dest").transform;
        selectCircle.SetActive(false);

        enemies = GameObject.FindObjectsOfType<EnemyBarrackManager>();




        InvokeRepeating("DealDamage", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1);

        if (selected)
        {
            selectCircle.SetActive(true);
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dest.transform.position = new Vector3(mousePos.x, mousePos.y, -1);

                destSetter.target = dest;

                if (mousePos.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                        transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                        transform.localScale.z);
                }
            }
        }
        else
        {
            destSetter.target = null;
            selectCircle.SetActive(false);
        }


        if (destSetter.target != null)
        {
            soldierAnim.SetBool("IsWalk", aiPath.velocity.magnitude > 0.5f || (destSetter.target.position - this.gameObject.transform.position).magnitude < 3f);
        }
        else
        {
            soldierAnim.SetBool("IsWalk", aiPath.velocity.magnitude > 0.5f);
        }
    }


    void DealDamage()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                continue;
            }

            if ((enemies[i].transform.position - this.gameObject.transform.position).magnitude < radius)
            {
                if(enemies[i].GetComponent<Damageable>().GetHealth() <= damage){CancelInvoke();}
                enemies[i].GetComponent<Damageable>().TakeDamage(damage);
            }
        }
    }
}
