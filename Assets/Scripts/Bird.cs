using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    private bool isDead = false;
    private Rigidbody2D birdBody;
    public float upForce = 200;
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        birdBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (isDead == false) {
            if (Input.GetMouseButtonDown(0)) {
                birdBody.velocity = Vector2.zero;
                birdBody.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isDead = true;
            anim.SetTrigger("Die");
        }
    }
}
