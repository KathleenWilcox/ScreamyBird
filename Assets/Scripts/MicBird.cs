using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicBird : MonoBehaviour { 
    public static float micLoudness;
    public string device;
    public float sensitivity = 0;
    public bool isFlapping = false;
    public float upForce = 200;
    public GameObject UI;

    private bool isDead = false;
    private Rigidbody2D birdBody;
    private Animator anim;

    bool microphoneInitialized;
    AudioSource audioSource;
    int sampleWindow = 128;

    void Start() {
        UI = GameObject.Find("GameController");
        anim = GetComponent<Animator>();
        birdBody = GetComponent<Rigidbody2D>();
        CreateMicrophone();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isDead = true;
            anim.SetTrigger("Die");
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        UI.GetComponent<UserInterface>().increaseScore();
    }
   

    void CreateMicrophone() {
        audioSource = GetComponent<AudioSource>();
        device = Microphone.devices[0];
        audioSource.clip = Microphone.Start(device, true, 1, 44100);
        microphoneInitialized = true;

    }

    void StopRecording() {
        Microphone.End(device);
    }

    float FindMaxVolume() {
        float maxVol = 0;
        float[] soundWaveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1);
        audioSource.clip.GetData(soundWaveData, 0);
        for (int i = 0; i < sampleWindow; i++)
        {
            float wavePeak = soundWaveData[i] * soundWaveData[i];
            if (maxVol < wavePeak)
            {
                maxVol = wavePeak;
            }
        }
        float level = Mathf.Sqrt(Mathf.Sqrt(maxVol));
        return 100 * level;
    }

    void Update() {
        micLoudness = FindMaxVolume();
        Debug.Log("micLoudness: " + micLoudness);
        if (isDead == false) { 
            if (micLoudness > sensitivity && !isFlapping) {
                isFlapping = true;
                StartCoroutine(waitToFlap());
                anim.SetTrigger("Flap");
                birdBody.velocity = Vector2.zero;
                birdBody.AddForce(new Vector2(0, upForce));
            }

        }

    }

    IEnumerator waitToFlap() {
        yield return new WaitForSeconds(0.5f);
        isFlapping = false;
    }

    void OnDisable() {
        StopRecording();
    }

    void OnDestroy() {
        StopRecording();
    }

    void OnApplicationFocus(bool focus) {
        if (focus) {
            if (!microphoneInitialized) {
                CreateMicrophone();
                microphoneInitialized = true;
            }
        }
        if (!focus) {
            StopRecording();
            microphoneInitialized = false;
        }
    }

    public bool checkIfDead() {
        return isDead;
    }
}
