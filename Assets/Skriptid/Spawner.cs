using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Klotside grupp
    public GameObject[] grupid;
    public GameObject KeelatudPrefab;

    public void SpawnNext()
    {
        // Võetakse suvaline indeks vahemikus 0 kuni gruppide järjendi suurus ja luuakse antud indeksiga elemendi isend
        int i = Random.Range(0, grupid.Length);

        Instantiate(grupid[i], transform.position, Quaternion.identity);
    }

    public void SpawnKeelatud()
    {
        foreach(Vector2 keelatud in Mänguväli._keelatudRuudud)
        {
            Instantiate(KeelatudPrefab, new Vector3(keelatud.x, keelatud.y, 0), Quaternion.identity);
        }
    }


    private void Start()
    {
        // Isimene objekt luuakse
        SpawnNext();
        SpawnKeelatud();
    }
}