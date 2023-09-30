using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grupp : MonoBehaviour
{
    // Viimane gravitatsiooninükke aeg
    float viimaneKukkumine = 0;

    void Start()
    {
        // Algasend ei sobi.
        if (!KasOnSobivAsendRuudustikus())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Liigub vasakule
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Muudab asendit
            transform.position += new Vector3(-1, 0, 0);

            // Kontrollib, kas asend on sobiv
            if (KasOnSobivAsendRuudustikus())
                // Sobiv asend, uuenda.
                värskendaRuudustik();
            else
                // Ei sobi, ära uuenda.
                transform.position += new Vector3(1, 0, 0);
        }

        // Liigub paremale
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Muudab asendit
            transform.position += new Vector3(1, 0, 0);

            // Sobiv asend?
            if (KasOnSobivAsendRuudustikus())
                // Sobiv. Värskenda ruudustik.
                värskendaRuudustik();
            else
                // Ei sobi, pööra tagasi.
                transform.position += new Vector3(-1, 0, 0);
        }

        // Pöörab
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            // Sobib?
            if (KasOnSobivAsendRuudustikus())
                // Sobib, uuenda.
                värskendaRuudustik();
            else
                // Ei sobi, võta tagasi.
                transform.Rotate(0, 0, 90);
        }

        // Liigub alla ja langeb
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                 Time.time - viimaneKukkumine >= 1)
        {
            // Muudab asukohta
            transform.position += new Vector3(0, -1, 0);

            // Sobib?
            if (KasOnSobivAsendRuudustikus())
            {
                // Sobib. Värskenda.
                värskendaRuudustik();
            }
            else
            {
                // Ei sobi, võta tagasi.
                transform.position += new Vector3(0, 1, 0);

                // Kustuta täidetud read
                Mänguväli.KustutaTervedRead();

                // Loo järgmine klots
                FindObjectOfType<Spawner>().SpawnNext();

                // Lülita skript välja
                enabled = false;
            }

            viimaneKukkumine = Time.time;
        }
    }

    bool KasOnSobivAsendRuudustikus()
    {
        foreach (Transform child in transform)
        {
            Vector2 vektor = Mänguväli.ÜmardaVector2(child.position);

            
            // Kas on piiridest väljas?
            if (!Mänguväli.Piirides(vektor)) 
            {return false;}

            // Kas blokk on ruudustikus (ja mitte samas grupis - pole sama klots)?
            if (Mänguväli.ruudustik[(int)vektor.x, (int)vektor.y] != null &&
                Mänguväli.ruudustik[(int)vektor.x, (int)vektor.y].parent != transform)
            { return false; }
        }
        return true;
    }

    void värskendaRuudustik()
    {
        // Vanad blokid eemaldatakse ruudustikust
        for (int y = 0; y < Mänguväli.kõrgus; ++y)
            for (int x = 0; x < Mänguväli.laius; ++x)
                if (Mänguväli.ruudustik[x, y] != null)
                    if (Mänguväli.ruudustik[x, y].parent == transform)
                        Mänguväli.ruudustik[x, y] = null;
        foreach (Transform child in transform)
        {
            Vector2 vektor = Mänguväli.ÜmardaVector2(child.position);
            Mänguväli.ruudustik[(int)vektor.x, (int)vektor.y] = child;
        }
    }
}
