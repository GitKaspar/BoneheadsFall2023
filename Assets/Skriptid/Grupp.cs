using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grupp : MonoBehaviour
{
    // Viimane gravitatsioonin�kke aeg
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
                v�rskendaRuudustik();
            else
                // Ei sobi, �ra uuenda.
                transform.position += new Vector3(1, 0, 0);
        }

        // Liigub paremale
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Muudab asendit
            transform.position += new Vector3(1, 0, 0);

            // Sobiv asend?
            if (KasOnSobivAsendRuudustikus())
                // Sobiv. V�rskenda ruudustik.
                v�rskendaRuudustik();
            else
                // Ei sobi, p��ra tagasi.
                transform.position += new Vector3(-1, 0, 0);
        }

        // P��rab
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            // Sobib?
            if (KasOnSobivAsendRuudustikus())
                // Sobib, uuenda.
                v�rskendaRuudustik();
            else
                // Ei sobi, v�ta tagasi.
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
                // Sobib. V�rskenda.
                v�rskendaRuudustik();
            }
            else
            {
                // Ei sobi, v�ta tagasi.
                transform.position += new Vector3(0, 1, 0);

                // Kustuta t�idetud read
                M�nguv�li.KustutaTervedRead();

                // Loo j�rgmine klots
                FindObjectOfType<Spawner>().SpawnNext();

                // L�lita skript v�lja
                enabled = false;
            }

            viimaneKukkumine = Time.time;
        }
    }

    bool KasOnSobivAsendRuudustikus()
    {
        foreach (Transform child in transform)
        {
            Vector2 vektor = M�nguv�li.�mardaVector2(child.position);

            
            // Kas on piiridest v�ljas?
            if (!M�nguv�li.Piirides(vektor)) 
            {return false;}

            // Kas blokk on ruudustikus (ja mitte samas grupis - pole sama klots)?
            if (M�nguv�li.ruudustik[(int)vektor.x, (int)vektor.y] != null &&
                M�nguv�li.ruudustik[(int)vektor.x, (int)vektor.y].parent != transform)
            { return false; }
        }
        return true;
    }

    void v�rskendaRuudustik()
    {
        // Vanad blokid eemaldatakse ruudustikust
        for (int y = 0; y < M�nguv�li.k�rgus; ++y)
            for (int x = 0; x < M�nguv�li.laius; ++x)
                if (M�nguv�li.ruudustik[x, y] != null)
                    if (M�nguv�li.ruudustik[x, y].parent == transform)
                        M�nguv�li.ruudustik[x, y] = null;
        foreach (Transform child in transform)
        {
            Vector2 vektor = M�nguv�li.�mardaVector2(child.position);
            M�nguv�li.ruudustik[(int)vektor.x, (int)vektor.y] = child;
        }
    }
}
