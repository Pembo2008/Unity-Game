using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField]

    private float health;

    public GameObject healthBar;

    void Update()
    {
        if (health < 1)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PurpleBullet")
        {
            health -= GameObject.Find("Player").GetComponent<Player>().currentWeapon.damage;
            Destroy(target.gameObject);

            healthBar.transform.localScale = new Vector3(health / 100, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

    }
}
