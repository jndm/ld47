using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private AudioSource music;
    private static MusicPlayer instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
            music = GetComponent<AudioSource>();
            music.Play();
            music.volume = 0.5f;
            DontDestroyOnLoad(gameObject);
        }
    }
}
