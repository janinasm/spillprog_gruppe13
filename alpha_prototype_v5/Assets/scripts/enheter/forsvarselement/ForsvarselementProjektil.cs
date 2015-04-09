using UnityEngine;
using System.Collections;

public class ForsvarselementProjektil : MonoBehaviour
{
    public float skade;

    // kjører når projektilen kolliderer med gameobject
    void OnTriggerEnter(Collider col)
    {
        // hvis projektilen kolliderer med Landsby eller Forsvarselement
        if (col.transform.gameObject.tag == "Fiende")
        {
            Debug.Log("FE projektil traff fiende");

            // kjører metode til gameobject for skade tatt
            col.gameObject.transform.parent.gameObject.SendMessage("taSkade", skade);

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
