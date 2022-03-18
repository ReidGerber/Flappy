using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] float hitPoints;
    [SerializeField] ParticleSystem thisParticleSystem;
    [SerializeField] Sprite deadsprite;

    private bool hasDied;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision " + collision.relativeVelocity.magnitude);
        hitPoints -= collision.relativeVelocity.magnitude;

    }

    private void Update()
    {
        if (hitPoints <= 0 && CanDie())
        {
            StartCoroutine (Die());
        }
    }

    bool CanDie()
    {
        if (hasDied)
        {
            return false;
        }

        return true;
    }

    IEnumerator Die()
    {
        hasDied = true;
        GetComponent<SpriteRenderer>().sprite = deadsprite;
        thisParticleSystem.Play();
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
