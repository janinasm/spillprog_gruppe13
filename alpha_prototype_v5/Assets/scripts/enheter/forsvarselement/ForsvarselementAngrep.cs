using UnityEngine;
using System.Collections;

public class ForsvarselementAngrep : MonoBehaviour
{
    private float tid;
    private bool angriper = false;

    // gameobject referanser
    private Transform target;
    private GameObject scriptHolder;
    public Transform projektilPrefab;
    public Transform skytePosisjon;
    private Vector3 resetSkytePosisjon;

    // script referanser
    private Forsvarselement forsvarselement;
    private SelectedForsvarselement selectedForsvarselement;

    void Start()
    {
        // cacher referanser
        scriptHolder = GameObject.Find("ScriptHolder");
        forsvarselement = GetComponent<Forsvarselement>();
        selectedForsvarselement = GetComponent<SelectedForsvarselement>();

        resetSkytePosisjon = skytePosisjon.position;
        resetSkytePosisjon.y = skytePosisjon.transform.position.y;
    }

    void Update()
    {
        // dersom gameobjektet er plassert i spillverden og det ikke er valgt (klikket på) av spilleren
        if (scriptHolder.GetComponent<ForsvarselementPlacement>().erPlassert && !selectedForsvarselement.erValgt)
        {
            // holder på tid gått
            tid += Time.deltaTime;

            // sjekker hver update om forsvarselementet har et target
            // og om det er har gått lang nok tid siden sist angrep
            if (target != null && tid >= forsvarselement.tidMellomAngrip)
            {
                // kjører metode for angrip
                Angrip();
            }
        }
    }

    // kjører når forsvarselementet kolliderer med gameobject
    public void OnTriggerStay(Collider col)
    {
        // hvis gameobject har tag Fiende
        if (col.transform.gameObject.tag == "Fiende")
        {
            // og hvis forsvarselementet ikke allerede angriper noen
            if (!angriper)
            {
                // sier at forsvarselementet angriper, slik at forsvarselementet ikke kan angripe flere på likt
                angriper = true;
              
                // holder på gameobjectet
                target = col.gameObject.transform;
            }
        }
    }

    // kjører når forsvarselementet slutter å kollidere med et gameobject
    public void OnTriggerExit(Collider col)
    {
        skytePosisjon.LookAt(resetSkytePosisjon);

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

        // setter skaden på projektilen til verdi fra Forsvarselement-script
        projektilInstance.GetComponent<ForsvarselementProjektil>().skade = forsvarselement.skade;

        // gir fart til projektilen via Rigidbody-komponent
        projektilInstance.GetComponent<Rigidbody>().velocity = projektilInstance.transform.forward * 20;

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
