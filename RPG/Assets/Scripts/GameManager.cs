using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager cl_GameManager;


    public float f_counterForMusic = 50;
    public bool b_musicIsPlaying = false;

    void Start () {

        cl_GameManager = this;
        FindObjectOfType<AudioManager>().Play("mainTheme");
        FindObjectOfType<AudioManager>().Play("windSound");
        FindObjectOfType<AudioManager>().Play("mainThemeMusic");
    }


    void PlayMusic()
    {
        FindObjectOfType<AudioManager>().Play("mainThemeMusic");
        b_musicIsPlaying = false;
    }

	void Update () {

        if (f_counterForMusic > 0)
        {
            f_counterForMusic -= Time.deltaTime;
            
        }
        if (f_counterForMusic <= 0 && b_musicIsPlaying == false)
        {
            b_musicIsPlaying = true;
            f_counterForMusic = 50;
        }

	}
}
