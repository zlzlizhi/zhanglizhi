using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour {
    public static Audiomanager Instance;
    public AudioClip Bomb;
    public AudioClip Bi;
    private AudioSource player;

	// Use this for initialization
	void Start () {
        Instance = this;
        player = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlayBomb()
    {
       
        player.PlayOneShot(Bomb);

    }
    public void PlayBi()
    {
        player.PlayOneShot(Bi);
    }
}
