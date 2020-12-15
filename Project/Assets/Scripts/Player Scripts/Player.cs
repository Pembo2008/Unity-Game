using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Weapon currentWeapon;
    public GameObject bullet;
    
    private Rigidbody2D myBody;
    private Animator anim;
    private Animator legAnim;
    
    public float speed;
    [SerializeField]
    private float health;

    private Vector2 moveVelocity;

    private float nextTimeOfFire = 0;

    private bool hit = true;

    
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        legAnim = transform.GetChild(2).GetComponent<Animator>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(Time.time >= nextTimeOfFire)
            {
                currentWeapon.Shoot();
                nextTimeOfFire = Time.time + 1 / currentWeapon.fireRate;
            }

        }

        if (health > 0)
            Rotation();
        
        //Bounds
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -500.0f, 500.0f), Mathf.Clamp(transform.position.y, -400.0f, 400.0f));
    }

    void FixedUpdate()
    {
        if(health > 0)
            Movement();
    }

    void Rotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }

    void Movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        myBody.MovePosition(myBody.position + moveVelocity*Time.fixedDeltaTime);

        if (moveVelocity == Vector2.zero)
            legAnim.SetBool("Moving", false);
        else
            legAnim.SetBool("Moving", true);

    }

    IEnumerator HitBoxOff()
    {
        hit = false;
        anim.SetTrigger("Hit");
        yield return new WaitForSeconds(2f);
        anim.ResetTrigger("Hit");
        hit = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy")
        {
            if (hit)
            {
                StartCoroutine(HitBoxOff());
                health--;
                Destroy(GameObject.Find("Life-Box").transform.GetChild(0).gameObject);
                if(health < 1)
                {
                    StartCoroutine(Death());
                }
            }
        }

    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
