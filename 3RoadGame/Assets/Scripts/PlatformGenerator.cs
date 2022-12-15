using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] platformPrefab;
    public float zResp = 0; // pozycja z spawnowania platform x i y sie nie zmienaja
    public float dlugoscPlatformy = 30; //dlugosc platformy to 30
    public int iloscPlatform = 5;
    private List<GameObject> aktywnePlatformy = new List<GameObject>();

    public Transform gracz;

    void Start()
    {
        for(int i = 0; i < iloscPlatform; i++)
        {
            if(i==0)
                GenerujPlatformy(0);
            else
                GenerujPlatformy(Random.Range(0, platformPrefab.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gracz.position.z - 35 > zResp-(iloscPlatform*dlugoscPlatformy))
        {
            GenerujPlatformy(Random.Range(0, platformPrefab.Length));
            UsunPlatforme();
        }
    }

    public void GenerujPlatformy(int platformIndex)
    {
        GameObject go = Instantiate(platformPrefab[platformIndex], transform.forward * zResp,               transform.rotation);
        aktywnePlatformy.Add(go);
        zResp = zResp + dlugoscPlatformy;
    }

    private void UsunPlatforme()
    {
        Destroy(aktywnePlatformy[0]);
        aktywnePlatformy.RemoveAt(0);
    }

}
