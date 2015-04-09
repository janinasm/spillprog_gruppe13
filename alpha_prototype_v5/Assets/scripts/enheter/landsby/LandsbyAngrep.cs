using UnityEngine;
using System.Collections;

public class LandsbyAngrep : MonoBehaviour
{
    private float tid;
    private bool angriper = false;

    // gameobject referanser
    private Transform target;

    // script referanser
    private Landsby landsby;

    void Start()
    {
        // cacher referanser
        landsby = GetComponent<Landsby>();
    }

    void Update()
    {
        // holder på tid gått
        tid += Time.deltaTime;

        // sjekker hver update om forsvarselementet har et target
        // og om det er har gått lang nok tid siden sist angrep
        if (target != null && tid >= 3f)
        {
            // kjører metode for angrip
            Angrip();
        }
    }

    // kjører når landsby kolliderer med gameobject
    public void OnTriggerStay(Collider col)
    {
        // hvis gameobject har tag Fiende
        if (col.transform.gameObject.tag == "Fiende")
        {
            // og hvis forsvarselementet ikke allerede angriper noen
            if (!angriper)
            {
                // sier at landsbyen angriper, slik at landsbyen ikke kan angripe flere på likt
                angriper = true;

                // holder på gameobjectet
                target = col.gameObject.transform;
            }
        }
    }

    // kjører når forsvarselementet slutter å kollidere med et gameobject
    public void OnTriggerExit(Collider col)
    {
        // resetter variabler som styrer angrep
        resetAngrip();
    }

    public void Angrip()
    {
        // kjører metode på Fiende-gameobject som fjerner skade fra helse
        target.parent.gameObject.SendMessage("taSkade", 100);

        // resetter variabler som styrer angrep
        resetAngrip();
    }

    // metode som resetter variabler som styrer angrep
    public void resetAngrip()
    {
        angriper = false;
        tid = 0f;
    }
}
