using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Poeng : MonoBehaviour
{
    // gui referanse
    public Text poengTekst;

    // Use this for initialization
    void Start()
    {
        // setter poeng text
        poengTekst.text = "Poeng: " + GameManager.instance.antallPoeng;
    }

    public void leggTilPoeng(int add)
    {
        // legger til verdi som kommer in som parameter
        GameManager.instance.antallPoeng += add;

        // setter poeng text
        poengTekst.text = "Poeng: " + GameManager.instance.antallPoeng;
    }
}
