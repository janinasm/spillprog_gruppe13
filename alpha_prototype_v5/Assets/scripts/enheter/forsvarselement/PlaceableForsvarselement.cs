using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceableForsvarselement : MonoBehaviour {

    // liste som skal holde på kolliderende gameobjekter
    public List<Collider> colliders = new List<Collider>();

    // kjøres dersom det skjer en kollisjon
    void OnTriggerEnter(Collider c)
    {
        // hvis gameobjekter har en av disse tag'ene
        if (c.tag == "Forsvarselement" || c.tag == "Path" || c.tag == "Landsby")
        {
            // legg til i listen
            colliders.Add(c);
        }
    }

    // kjøres dersom et gameobject slutter å kollidere
    void OnTriggerExit(Collider c)
    {
        // hvis gameobjekter har en av disse tag'ene
        if (c.tag == "Forsvarselement" || c.tag == "Path" || c.tag == "Landsby")
        {
            // fjern fra listen
            colliders.Remove(c);
        }
    }
}
