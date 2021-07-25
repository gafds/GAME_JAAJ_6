﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField, Range(0f, 10f)] float speed;
    [SerializeField] Animator anim;

    Coroutine interact;
    public bool movable;

    Vector2 direction;

    void Update()
    {
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
        direction = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed).normalized;

        anim.SetBool("Andando",rb.velocity.magnitude != 0);
        if (movable && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            this.transform.up = direction;
        }
    }
    private void FixedUpdate()
    {
        if (movable)
            rb.velocity = direction;
        else
            rb.velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Iinteractable>() != null)
        {
            if (interact != null)
                StopCoroutine(interact);
            interact = StartCoroutine(Interact(other.gameObject));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Iinteractable>() != null)
        {
            print("desliguei");
            StopCoroutine(interact);
        }
    }
    private IEnumerator Interact(GameObject objeto)
    {
        while(true)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                objeto.GetComponent<Iinteractable>().Interact();
            }
            yield return null;
        }
    }
}
