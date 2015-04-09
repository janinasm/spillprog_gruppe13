using UnityEngine;
using System.Collections;

public class FiendeHelse : MonoBehaviour
{
    // script referanser
    private Fiende fiende;

    void Start()
    {
        // cacher referanser
        fiende = GetComponent<Fiende>();
    }

    public void taSkade(int skadeInn)
    {
        // trekker skaden fra HP
        fiende.helse -= skadeInn;

        // sjekker om HP er lik eller større enn 0
        if (fiende.helse <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // sletter gameobjektet
        Destroy(transform.gameObject);

        // legger til poeng
        GameManager.instance.poeng.SendMessage("leggTilPoeng", fiende.poengVerdi);

        // legger til penger
        GameManager.instance.penger.SendMessage("leggTilPenger", fiende.poengVerdi);
    }
}
