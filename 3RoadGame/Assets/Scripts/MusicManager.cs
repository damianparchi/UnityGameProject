using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public Music[] dzwieki;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Music i in dzwieki)
        {
            i.source = gameObject.AddComponent<AudioSource>();
            i.source.clip = i.klip;
            i.source.loop = i.petla;
        }

        PlayMusic("backgroundMusic");
        
    }

    public void PlayMusic(string nazwa)
    {
       foreach(Music i in dzwieki)
        {
            if(i.nazwa == nazwa)
            {
                i.source.Play();
            }
        }
    }
}
