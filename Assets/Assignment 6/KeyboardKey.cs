using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class KeyboardKey : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClip;

    public KeyCode keyboardButton;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardInput();
    }

    void KeyboardInput()
    {

        audioSource.pitch = Input.GetKey(KeyCode.LeftShift) ? audioSource.pitch = 2.0f : audioSource.pitch = 1.0f;

        if (Input.GetKeyDown(keyboardButton))
        {
            PlayKey();
        }
    }

    void PlayKey()
    {
        audioSource.PlayOneShot(audioClip);
    }


}
