using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapeatingObject : MonoBehaviour {

    private BoxCollider2D groundCollider;
    private float horizontalGroundLength;

    void Start() {
        groundCollider = GetComponent<BoxCollider2D>();
        horizontalGroundLength = groundCollider.size.x;

    }

    void Update(){
        if (transform.position.x < -horizontalGroundLength) {
            repositionBackground();
        }
    }

    private void repositionBackground() {
        Vector2 groundOffset = new Vector2(horizontalGroundLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
