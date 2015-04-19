using UnityEngine;
using System.Collections;

public class ForsvarselementRotasjon : MonoBehaviour
{
    private float avstandTilSkjerm;
    private Vector3 musPos;

    //int rotasjonGrad = 360 / 8;

    // script referanser
    private SelectedForsvarselement selectedForsvarselement;

    void Start()
    {
        // cacher referanser
        selectedForsvarselement = GetComponent<SelectedForsvarselement>();
    }

    void Update()
    {
        // forsvarselementet kan bare roteres dersom det er valgt (klikket på) av spilleren.
        if (selectedForsvarselement.erValgt)
        {
            roterMotMusPos();
        }
    }

    // roterer mot muspoisjon
    public void roterMotMusPos()
    {
        // gameobjekt skal roteres i 3D spillverden mot musens 2D posisjon på skjermen,
        // derfor må 2D-posisjonen (Vector2) gjøres om til 3D verdier (Vector3)

        // henter avstanden kameraet har fra der gameobjektet er i spillverden
        avstandTilSkjerm = Camera.main.WorldToScreenPoint(transform.position).z;

        // gjør om musplassering på skjerm til plass i spillverden, 
        // ved å lage Vector3 av musens xy-posisjon, og avstanden kamera hadde til gameobjektet for å finne dybden (z-posisjon)
        musPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, avstandTilSkjerm));

        // lager ny Vector3 der y-posjonen er 0 slik at gameobjektet holdes på spillflaten
        musPos = new Vector3(musPos.x, 0, musPos.z);

        // forsvarselementet skal rotere mot den nye posisjonen
        transform.LookAt(musPos);
    }

    // gradvis rotasjon med keyboard-shortcuts
    //public void roterGradvis()
    //{
    //    if (Input.GetKeyDown(KeyCode.R) && selectedForsvarselement.erValgt)
    //    {
    //        // roter gameobject med klokken
    //        transform.Rotate(0, rotasjonGrad, 0);
    //    }

    //    else if (Input.GetKeyDown(KeyCode.F) && selectedForsvarselement.erValgt)
    //    {
    //        // roter gameobject med klokken
    //        transform.Rotate(0, -rotasjonGrad, 0);
    //    }
    //}
}
