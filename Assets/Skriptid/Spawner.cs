using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Klotside grupp
    public GameObject[] grupid;

    public void SpawnNext()
    {
        // V�etakse suvaline indeks vahemikus 0 kuni gruppide j�rjendi suurus ja luuakse antud indeksiga elemendi isend
        int i = Random.Range(0, grupid.Length);

        Instantiate(grupid[i], transform.position, Quaternion.identity);
    }

    private void Start()
    {
        // Isimene objekt luuakse
        SpawnNext();
    }



}
