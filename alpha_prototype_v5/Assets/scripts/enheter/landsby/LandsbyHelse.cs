using UnityEngine;
using System.Collections;

public class LandsbyHelse : MonoBehaviour {

    // script referanser
    private Landsby landsby;

    void Start()
    {
        // cacher referanser
        landsby = GetComponent<Landsby>();
    }

    public void taSkade(int skadeInn)
    {
        // trekker skaden fra HP
        landsby.helse -= skadeInn;

        // sjekker om HP er mer enn null
        if (landsby.helse <= 0)
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
