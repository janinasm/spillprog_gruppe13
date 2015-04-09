using UnityEngine;
using System.Collections;

public class ForsvarselementHelse : MonoBehaviour
{
    // script referanser
    private Forsvarselement forsvarselement;

    void Start()
    {
        // cache
        forsvarselement = GetComponent<Forsvarselement>();
    }

    public void taSkade(int skadeInn)
    {
        // trekker skaden fra HP
        forsvarselement.helse -= skadeInn;

        // sjekker om HP er lik eller større enn 0
        if (forsvarselement.helse <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // sletter gameobjektet
        Destroy(gameObject);
    }
}
