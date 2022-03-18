using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float launchForce = 500;
    [SerializeField] float maxDragDistance = 3.5f;

    Vector2 startPosition;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    bool resetting;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<LineRenderer>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = rigidBody2D.position;
        rigidBody2D.isKinematic = true;
        resetting = false;
    }

    void OnMouseDown()
    {
        spriteRenderer.color = Color.red;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        GetComponent<LineRenderer>().enabled = true;
        GetComponent<LineRenderer>().SetPosition(0, startPosition);
        GetComponent<LineRenderer>().SetPosition(1, transform.position);

        float distance = Vector2.Distance(startPosition, desiredPosition);
        if (distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - startPosition;
            direction.Normalize();
            desiredPosition = startPosition + (direction * maxDragDistance);
        }
        
        if (desiredPosition.x > startPosition.x)
        {
            desiredPosition.x = startPosition.x;
        }

        rigidBody2D.position = desiredPosition;
    }
    void OnMouseUp()
    {
        var currentPosition = rigidBody2D.position;
        Vector2 direction = startPosition - currentPosition;
        direction.Normalize();

        rigidBody2D.isKinematic = false;
        rigidBody2D.AddForce(direction * launchForce);

        spriteRenderer.color = Color.white;
        GetComponent<LineRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!resetting)
        {
            StartCoroutine(ResetAfterDelay());
        }
        
    }

    IEnumerator ResetAfterDelay()
    {
        resetting = true;
        yield return new WaitForSeconds(3);
        rigidBody2D.position = startPosition;
        rigidBody2D.isKinematic = true;
        rigidBody2D.velocity = Vector2.zero;
        resetting = false;
    }
}
