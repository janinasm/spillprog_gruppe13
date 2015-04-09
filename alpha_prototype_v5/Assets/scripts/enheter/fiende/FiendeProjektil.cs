using UnityEngine;
using System.Collections;

public class FiendeProjektil : MonoBehaviour
{
    public int skade;

    // kjører når projektilen kolliderer med gameobject
    void OnTriggerEnter(Collider col)
    {
        // hvis projektilen kolliderer med Landsby eller Forsvarselement
        if (col.transform.gameObject.tag == "Landsby" || col.transform.gameObject.tag == "Forsvarselement")
        {
            // kjører metode til gameobject for skade tatt
            col.transform.gameObject.SendMessage("taSkade", skade);

            // sletter projektilen
            Destroy(gameObject);

        }
        // hvis den treffer spill-flaten
        if (col.transform.gameObject.tag == "Ground")
        {
            // sletter projektilen
            Destroy(gameObject);
        }
    }
}
