using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mänguväli : MonoBehaviour
{
    // Loome ruudustiku. Transform'i järjend on koordinaatide järjend? Aga kolmandat koordinaati ei taha?
    public static int laius = 10;
    public static int kõrgus = 20;
    public static Transform[,] ruudustik = new Transform[laius, kõrgus];

    // Ei lase koordinaatidel olla ujukomaarv
    public static Vector2 ÜmardaVector2(Vector2 vektor)
    {
        return new Vector2(Mathf.Round(vektor.x),
            Mathf.Round(vektor.y));
    }

    // Kontrollib, kas vektor asub piiride sees ja kõrgemal kui alumine ots.
    public static bool Piirides(Vector2 asend) 
    {
        return (int)asend.x >= 0 &&
            (int)asend.x < laius &&
            (int)asend.y >= 0;
        // Meil võib olla vaja implementeerida kontroll, kus vaadeldakse ka y koordinaadi sobivust (ehk isegi spetsiifilisi ridu), kuna meie mänguväli on mitmeti piiratud
    }

    // NB! POLE MEIE MÄNGUKS VAJA? Kustutab kõik mänguobjektid antud reas
    public static void KustutaRida(int y)
    {
        for (int i = 0; i < laius; i++)
        {
            Destroy(ruudustik[i, y].gameObject);
            ruudustik[i, y] = null;
        }
    }

    // NB! POLE MEIE MÄNGUKS VAJA! Rida langeb allapoole

    public static void KukutaRida(int y)
    {
        for (int x = 0; x < laius; ++x)
        {
            if (ruudustik[x, y] != null)
            {
                // Liigutab ühe rea allapoole
                ruudustik[x, y - 1] = ruudustik[x, y];
                ruudustik[x, y] = null;

                // Värskendab ruudu asukohta. NB! Kui seda ei tee, siis on ruut maatriksis koha muutnud, aga visuaalselt mitte.
                ruudustik[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // NB! POLE MEIE MÄNGUKS VAJA!
    // Kontrollib y's ülespoole jäävaid ridu
    public static void KärbiÜlemisiRidu(int y)
    {
        for (int i = y; i < kõrgus; ++i)
            KukutaRida(i);
    }

    // Kotnrollib, kas rida y on täis.
    public static bool KasRidaTäis(int y)
    {
        for (int x = 0; x < laius; ++x)
            if (ruudustik[x, y] == null)
                return false;
        return true;
    }

    public static void KustutaTervedRead()
    {
        for(int y = 0; y < kõrgus; ++y)
        {
            if (KasRidaTäis(y))
            {
                KustutaRida(y);
                KärbiÜlemisiRidu(y + 1);
                --y;
            }
        }
    }

}
