using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float damage = 1.0f;

    public GameObject owner;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Hurtbox") || !collision.gameObject.GetComponent<Hurtbox>())
        {
            return;
        }

        Hurtbox victim = collision.gameObject.GetComponent<Hurtbox>();
        if (victim.hitted == false && !victim.owner.GetComponent<Health>().invincible)
        {
            victim.dir = victim.transform.position - transform.position;
            victim.hitted = true;
            victim.owner.GetComponent<Health>().HealthPoint -= damage;
        }
    }
}
