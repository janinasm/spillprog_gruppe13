using System.Collections.Generic;
using UnityEngine;

public class FaseSkifte : MonoBehaviour
{
    public List<GameObject> fienderListe = new List<GameObject>();

    // script referanser
    private Forberedelsesfase forberedelsesfase;
    private Kampfase kampfase;
    private FaseGUI faseGUI;

    void Start()
    {
        // cacher referanser
        forberedelsesfase = GetComponent<Forberedelsesfase>();
        kampfase = GetComponent<Kampfase>();
        faseGUI = GetComponent<FaseGUI>();

        // setter GUI
        faseGUI.faseText.text = "Forberedelsesfase";
        faseGUI.rundeText.text = "Runde " + GameManager.instance.runde.ToString();

        // starter spillet i forberedelsesfasen
        GameManager.instance.erForberedelsesFase = true;
        forberedelsesfase.startForberedelsesFase();
    }

    void Update()
    {
        // teller ned til 0 hvis det er forberedelsesfase
        if (GameManager.instance.nedteller >= 0f && GameManager.instance.erForberedelsesFase)
        {
            GameManager.instance.nedteller -= Time.deltaTime;

            // oppdaterer nedtelleren i heltall
            faseGUI.nedtellerText.text = GameManager.instance.nedteller.ToString("F0");
        }

        // 
        if (GameManager.instance.nedteller <= 0f && GameManager.instance.erForberedelsesFase)
        {
            GameManager.instance.erForberedelsesFase = false;
            SkiftFase(GameManager.instance.erForberedelsesFase);
        }
    }

    public void SkiftFase(bool b)
    {
        if (b)
        {
            forberedelsesfase.startForberedelsesFase();
            GameManager.instance.erForberedelsesFase = true;

            faseGUI.faseText.text = "Forberedelsesfase";
        }
        else
        {
            kampfase.startKampfase();

            faseGUI.faseText.text = "Kampfase";
        }
    }

}
