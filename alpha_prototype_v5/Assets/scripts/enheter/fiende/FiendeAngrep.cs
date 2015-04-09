using UnityEngine;
using System.Collections;

public class FiendeAngrep : MonoBehaviour
{
    private float tid; 
    private bool angriper = false;
   
    // gameobject referanser
    public Transform projektilPrefab;
    public GameObject skytePosisjon;
    public Transform target;

    // script referanser
    private Fiende fiende;

    void Start()
    {
        // cacher referanser
        fiende = GetComponent<Fiende>();
    }

    void Update()
    {
        // holder på tid gått
        tid += Time.deltaTime;

        // sjekker hver update om fienden har et target,
        // og om det er har gått lang nok tid siden sist angrep
        if (target != null && tid >= fiende.tidMellomAngrip)
        {
            // kjører metode for angrip
            Angrip();
        }
    }

    // kjører når fienden kolliderer med gameobject
    public void OnTriggerStay(Collider col)
    {
        // hvis gameobject har tag Landsby eller Forsvarselement
        if (col.transform.gameObject.tag == "Landsby" || col.transform.gameObject.tag == "Forsvarselement")
        {
            // og hvis fienden ikke allerede angriper noen
            if (!angriper)
            {
                // sier at fienden angriper, slik at fienden ikke kan angripe flere på likt
                angriper = true;
               
                // holder på gameobjectet
                target = col.gameObject.transform;
            }
        }
    }

    // kjører når fienden slutter å kollidere med et gameobject
    public void OnTriggerExit(Collider col)
    {
        // resetter variabler som styrer angrep
        resetAngrip();
    }

    // metode for angrep
    public void Angrip()
    {
        // skal holde på projektilen
        Transform projektilInstance;

        // posisjonen der projektilen skyter retter seg mot gameobjectet som er target
        skytePosisjon.transform.LookAt(target);

        // instantiater projektil-prefab på skyteposisjonen
        projektilInstance = Instantiate(projektilPrefab, skytePosisjon.transform.position, skytePosisjon.transform.rotation) as Transform;

        // setter skaden på projektilen til verdi fra Fiende-script
        projektilInstance.GetComponent<FiendeProjektil>().skade = fiende.skade;

        // gir fart til projektilen via Rigidbody-komponent
        projektilInstance.GetComponent<Rigidbody>().velocity = projektilInstance.transform.forward * 50;

        // resetter variabler som styrer angrep
        resetAngrip();
    }

    // metode som resetter variabler som styrer angrep
    public void resetAngrip()
    {
        target = null;
        angriper = false;
        tid = 0f;
    }
}
