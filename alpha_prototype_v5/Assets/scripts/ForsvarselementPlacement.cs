using UnityEngine;
using System.Collections;

public class ForsvarselementPlacement : MonoBehaviour
{

    // basert på kode vist her: https://www.youtube.com/watch?v=OuqThz4Zc9c

    public bool erPlassert;
    private float avstandTilSkjerm;
    private Vector3 flyttPosisjon;

    // script referanser
    private PlaceableForsvarselement placeableForsvarselement;
    private SelectedForsvarselement forrigeForsvarselement;

    // gameobject referanser
    public Transform holdtForsvarselement;

    public LayerMask forsvarselementMask;

    void Update()
    {
        // hvis forsvarselement eksisterer og ikke er plassert
        if (holdtForsvarselement != null && !erPlassert)
        {
            // gameobjekt skal flyttes i 3D spillverden med musens 2D posisjon på skjermen,
            // derfor må 2D-posisjonen (Vector2) gjøres om til 3D verdier (Vector3)

            // henter avstanden kameraet har fra der gameobjektet er i spillverden
            avstandTilSkjerm = Camera.main.WorldToScreenPoint(holdtForsvarselement.transform.position).z;

            // gjør om musplassering på skjerm til plass i spillverden, 
            // ved å lage Vector3 av musens xy-posisjon, og avstanden kamera hadde til gameobjektet for å finne dybden (z-posisjon)
            flyttPosisjon = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, avstandTilSkjerm));

            // gir gameobjektet plasseringen, men y-posisjon er 0 fordi det er bakkenivået
            holdtForsvarselement.position = new Vector3(flyttPosisjon.x, 0, flyttPosisjon.z);

            // sjekker etter museklikk
            if (Input.GetMouseButtonDown(0))
            {
                // hvis plassen er gyldig sier vi at den er plassert, og objektet flyttes ikke lenger av musen
                if (erGyldigPosisjon())
                {
                    erPlassert = true;
                }
            }
        }

        // sjekker etter museklikk når spilleren ikke flytter et forsvarselement
        else if (Input.GetMouseButtonDown(0))
        {
            // kjører metode som sjekker om vi trykker på et plassert forsvarselement
            velgForsvarselement();
        }
    }

    // metode som sjekker om vi trykker på et plassert forsvarselement
    public void velgForsvarselement()
    {
        // lager en raycast fra der musen er på skjermen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // holder på der ray'en treffer
        RaycastHit hit = new RaycastHit();

        // hvis raycast treffer et forsvarselement
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, forsvarselementMask))
        {
            // sjekker om det er det forrige
            if (forrigeForsvarselement != null)
            {
                // den ikke skal være det valgte objektet
                forrigeForsvarselement.settSomValgt(false);
            }

            // hvis objektet er det valgte objektet
            hit.collider.gameObject.GetComponent<SelectedForsvarselement>().settSomValgt(true);

            // lagre denne slik at vi vet hvilket som er valgt
            forrigeForsvarselement = hit.collider.gameObject.GetComponent<SelectedForsvarselement>();
        }

        // hvis vi har et valgt gameobjekt og treffer noe som det ikke er et gui-element 
        else if (forrigeForsvarselement != null && GUIUtility.hotControl == 0)
        {
            // den ikke skal være det valgte objektet
            forrigeForsvarselement.settSomValgt(false);
        }
    }

    // lager gameobject sendt fra ForsvarselementManager
    public void SetItem(GameObject b)
    {
        // gameobject er ikke plassert
        erPlassert = false;

        // instantiater gameobjectet og holder på det
        holdtForsvarselement = ((GameObject)Instantiate(b)).transform;

        // henter referanse til script som ligger på gameobjectet
        // brukes til å sjekke om gameobjektet kan plasseres ved å telle kollisjoner
        placeableForsvarselement = holdtForsvarselement.GetComponent<PlaceableForsvarselement>();
    }

    // sjekker om vi kan plassere gameobjektet
    bool erGyldigPosisjon()
    {
        // hvis listen med kolliderte gameobjekter er større enn 0  
        if (placeableForsvarselement.colliders.Count > 0)
        {
            // kan ikke plasseres
            return false;
        }

        // kan plasseres
        return true;
    }
}