using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicTestScript : MonoBehaviour {

    public static float micLoudness;
    public string device;
    public float sensitivity = 0;

    bool microphoneInitialized;
    AudioSource audioSource;
    int sampleWindow = 128;

    void Start() {
        CreateMicrophone();
    }

    void CreateMicrophone() {
        audioSource = GetComponent<AudioSource>();
        device = Microphone.devices[0];
        audioSource.clip = Microphone.Start(device, true, 1, 44100);
        microphoneInitialized = true;
        
    }

     void StopRecording(){
        Microphone.End(device);
    }

    float FindMaxVolume() {
        float maxVol = 0;
        float[] soundWaveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1);
        audioSource.clip.GetData(soundWaveData, 0);
        for (int i = 0; i < sampleWindow; i++) {
        float wavePeak = soundWaveData[i] * soundWaveData[i];
            if (maxVol < wavePeak) {
                maxVol = wavePeak;
            }
        }
        float level = Mathf.Sqrt(Mathf.Sqrt(maxVol));
        return 100 * level;
    }

    void Update() {
        micLoudness = FindMaxVolume();
        if (micLoudness > sensitivity) {
            Debug.Log("Flap: "+micLoudness);
        }

        if (micLoudness < sensitivity) {
            Debug.Log("Not FLap: " + micLoudness);
        }
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
}
