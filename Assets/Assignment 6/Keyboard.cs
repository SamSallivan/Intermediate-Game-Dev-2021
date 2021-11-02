using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class Keyboard : MonoBehaviour
{

    public AudioSource audioSource;

    public List<KeyboardKey> keyboardKeys = new List<KeyboardKey>();

    

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        audioSource.volume = MusicManager.instance.volume;
        audioSource.pitch = MusicManager.instance.tempo;

        if (Input.GetKeyDown(KeyCode.P))
        {
            audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Q) && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
