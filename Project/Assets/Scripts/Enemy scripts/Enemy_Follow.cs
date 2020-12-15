using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Follow : MonoBehaviour
{
    private List<Rigidbody2D> EnemyRBs;
    private Rigidbody2D rb;
    private Animator myAnim;
    private Transform target;
    private float repelRange = .5f;
    public Transform homePos;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float range;
    
    void Start()
    {
        myAnim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    void OnDestroy()
    {
        EnemyRBs.Remove(rb);

    }

    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= range)
        {
            FollowPlayer();

        }
        else if(Vector3.Distance(target.position, transform.position) >= range)
        {
            GoHome();
        }
        Vector2 repelForce = Vector2.zero;
        foreach (Rigidbody2D enemy in EnemyRBs)
        {
            if (enemy == rb)
                continue;

            if (Vector2.Distance(enemy.position, rb.position) <= repelRange)
            {

                Vector2 repelDir = (rb.position - enemy.position).normalized;
                repelForce += repelDir;
            }

        }
    }
    

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

    }

    public void GoHome()
    {
        myAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

        if (Vector3.Distance(homePos.position, transform.position) == 0)
        {
            myAnim.SetBool("isMoving", false);
        }
    }


}
