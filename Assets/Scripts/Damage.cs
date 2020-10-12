using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{

    public float damageAmount = 10.0f;

    public bool damageOnTrigger = true;
    public bool damageOnCollision = false;
    public bool continuousDamage = false;
    public float continuousTimeBetweenHits = 0;

    public bool destroySelfOnImpact = false;    // variables dealing with exploding on impact (area of effect)
    public float delayBeforeDestroy = 0.0f;
    public GameObject explosionPrefab;

    private float savedTime = 0;

    void OnTriggerEnter(Collider collision)                     // used for things like bullets, which are triggers.  
    {
        Debug.Log("OnTriggerEnter 0");
        if (damageOnTrigger)
        {
            Debug.Log("OnTriggerEnter 1");
            if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player") // if the player got hit with it's own bullets, ignore it
                return;

            if (collision.gameObject.GetComponent<Health>() != null)
            {   // if the hit object has the Health script on it, deal damage
                Debug.Log("OnTriggerEnter 2");
                collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);

                if (destroySelfOnImpact)
                {
                    Destroy(gameObject, delayBeforeDestroy);      // destroy the object whenever it hits something
                }

                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, transform.rotation);
                }
            }
        }
    }


    void OnCollisionEnter(Collision collision)                      // this is used for things that explode on impact and are NOT triggers
    {
        Debug.Log("OnCollisionEnter 0");
        if (damageOnCollision)
        {
            Debug.Log("OnCollisionEnter 1");
            if (CompareTag("PlayerBullet") && collision.gameObject.CompareTag("Player")) // if the player got hit with it's own bullets, ignore it
                return;

            if (collision.gameObject.GetComponent<Health>() != null)
            {
                Debug.Log("OnCollisionEnter 2");
                // if the hit object has the Health script on it, deal damage
                collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);

                if (destroySelfOnImpact)
                {
                    Destroy(gameObject, delayBeforeDestroy);      // destroy the object whenever it hits something
                }

                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, transform.rotation);
                }
            }
        }
    }


    void OnCollisionStay(Collision collision) // this is used for damage over time things
    {
        if (continuousDamage)
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Health>() != null)
            {   // is only triggered if whatever it hits is the player
                if (Time.time - savedTime >= continuousTimeBetweenHits)
                {
                    savedTime = Time.time;
                    collision.gameObject.GetComponent<Health>().ApplyDamage(damageAmount);
                }
            }
        }
    }

}