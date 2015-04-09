using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForsvarselementManager : MonoBehaviour
{
    // basert på kode fra https://www.youtube.com/watch?v=OuqThz4Zc9c
    // 

    // gui referanser
    public Text slotForsvarselement1KnappText;

    // gameobject referanser
    public GameObject forsvarselement;

    // script referanser
    private ForsvarselementPlacement forsvarselementPlacement;

    void Start()
    {
        // cacher referanser
        forsvarselementPlacement = GetComponent<ForsvarselementPlacement>();

        // setter gui text
        slotForsvarselement1KnappText.text = "";
    }

    // actionbarslot 1
    public void forsvarselementKnapp1()
    {
        // kjører metoden for å lage nytt forsvarselement
        // tar imot prisen og type forsvarselement som parameter
        lagForsvarslement(100, forsvarselement);
    }

    // actionbarslot 2
    public void forsvarselementKnapp2()
    {
        // kjører metoden for å lage nytt forsvarselement
        // tar imot prisen og type forsvarselement som parameter
        lagForsvarslement(1000, forsvarselement);
    }

    public void lagForsvarslement(int pris, GameObject go)
    {
        // hvis det er forberedelsesfase og det er nok penger
        if (GameManager.instance.erForberedelsesFase &&
            pris <= GameManager.instance.antallPenger)
        {
            // kjører metode i script for instansiating og plassering av forsvarselement
            forsvarselementPlacement.SetItem(go);

            // kjører metode som fjerner prisen fra penger
            GameManager.instance.penger.fjernPenger(pris);
        }
    }
}

