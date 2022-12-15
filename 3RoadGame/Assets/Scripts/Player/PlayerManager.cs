using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool koniecGry;
    public GameObject koniecGryEkran;

    public static bool start;
    public GameObject StartHeader;

    public static int LiczbaMonet;
    public Text Monety;//referencja do tekstu monet

    void Start()
    {
        koniecGry = false; // na poczatku flaga na 0
        Time.timeScale = 1; // po restarcie gry timescale musibyc rowny 1
        
        start = false; // na poczatku 0 dla napisu
        LiczbaMonet = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(koniecGry) // jezeli koniec gry
        {
            Time.timeScale = 0;
            koniecGryEkran.SetActive(true); // wlacz panel gameover
        }
        Monety.text = "Monet: "+LiczbaMonet;

        if(Input.anyKey) // uzywamy swipemanager.tap do tego 
        {
            start = true; // gdy tapniemy na ekran zmianiamy bool start na true w playercontroller sprawdzimy czy: gra sie nie zaczela to nie bedziemy mogli sie ruszac
            Destroy(StartHeader);

        }
    }
}
