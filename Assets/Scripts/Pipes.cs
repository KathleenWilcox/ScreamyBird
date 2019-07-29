using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour {

    public int numPipes = 5;
    public float spawnTime = 4f;
    public float pipeMinPos = -1f;
    public float pipeMaxPos = 3.5f;
    public GameObject pipe;

    private GameObject[] pipes;
    private Vector2 objectPosition = new Vector2(-15f, -25f);
    private float timeSinceLastSpawned;
    private float pipeXPosition = 5f;
    private int currentPipeInArray = 0;

    GameObject birdreference;

    void Start() {

        birdreference = GameObject.FindWithTag("Player");
        pipes = new GameObject[numPipes];
        for ( int pipeNumber = 0; pipeNumber < numPipes; pipeNumber++ ) {
            pipes[pipeNumber] = (GameObject) Instantiate(pipe, objectPosition, Quaternion.identity);
        }

        timeSinceLastSpawned = spawnTime;
    }

    void Update() {
        timeSinceLastSpawned += Time.deltaTime;
        if (timeSinceLastSpawned >= spawnTime) {

            timeSinceLastSpawned = 0;
            float pipeYPosition = Random.Range(pipeMinPos, pipeMaxPos);
            pipes[currentPipeInArray].transform.position = new Vector2(pipeXPosition, pipeYPosition);
            currentPipeInArray++;

            if(currentPipeInArray >= numPipes) {
                currentPipeInArray = 0;
            }
        }

        if (birdreference.GetComponent<MicBird>().checkIfDead()) {
            this.enabled = false;
        }
    }
}
