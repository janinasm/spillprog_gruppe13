using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Penger : MonoBehaviour {

    // gui referanse
    public Text pengeTekst;

    void Start()
    {   
        // setter tekst
        pengeTekst.text = GameManager.instance.antallPenger.ToString() ;
    }

    public void leggTilPenger(int add)
    {
        // legger til verdi som kommer inn
        GameManager.instance.antallPenger += add;

        // setter tekst
        pengeTekst.text = "Penger: " + GameManager.instance.antallPenger;
    }

    public void fjernPenger(int remove)
    {
        // fjerner verdi som kommer inn
        GameManager.instance.antallPenger -= remove;

        // setter tekst
        pengeTekst.text = "Penger: " + GameManager.instance.antallPenger;
    }
    
}
