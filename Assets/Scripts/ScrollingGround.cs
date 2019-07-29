using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingGround : MonoBehaviour {

    Rigidbody2D rb2d;
    GameObject birdreference;

    public float scrollSpeed = -1f;

    void Start() {
        birdreference = GameObject.FindWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(scrollSpeed, 0);
    }

    void Update() { 
        if(birdreference.GetComponent<MicBird>().checkIfDead()) {
            rb2d.velocity = Vector2.zero;
        }
    }
}
