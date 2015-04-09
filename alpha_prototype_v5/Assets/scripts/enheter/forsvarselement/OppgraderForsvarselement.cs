using UnityEngine;
using System.Collections;

public class OppgraderForsvarselement : MonoBehaviour
{
    private int maxLevel = 3;
    private int oppgraderingKostnad;
    private Vector3 nyStorrelse;

    // gameobject referanser
    public Transform range;

    // komponent referanser
    private BoxCollider skyteRange;

    // script referanser
    private Forsvarselement forsvarselement;

    void Start()
    {
        // cacher referanser
        skyteRange = range.GetComponent<BoxCollider>() as BoxCollider;
        forsvarselement = GetComponent<Forsvarselement>();
    }

    public void oppgrader()
    {
        // finner oppgraderingkostnad basert på level
        oppgraderingKostnad = forsvarselement.oppgraderingKostnad * forsvarselement.level;

        // hvis level er mindre enn maxlevel og det er nok penger til å oppgradere
        if (forsvarselement.level < maxLevel && oppgraderingKostnad <= GameManager.instance.antallPenger)
        {
            // øker level med 1
            forsvarselement.level++;

            // sjekker level og gjør ting basert på det
            // lot det være en switch her tilfelle levels skal ha unike karakteristikker
            switch (forsvarselement.level)
            {
                case 2:

                    // lager ny størrelse til skyterange-gameobjekt
                    nyStorrelse = new Vector3(3f, 0.1f, 16f);

                    // sender med nye verdier til metode som oppgraderer verdiene
                    oppgraderVerdier(nyStorrelse, oppgraderingKostnad);
                    break;

                case 3:

                    // lager ny størrelse til skyterange-gameobjekt
                    nyStorrelse = new Vector3(6f, 0.1f, 20f);

                    // sender med nye verdier til metode som oppgraderer verdiene
                    oppgraderVerdier(nyStorrelse, oppgraderingKostnad);
                    break;
            }
        }

        else
        {
            // TODO gi feilmeldinger til spilleren
        }
    }

    public void oppgraderVerdier(Vector3 str, int oPris)
    {
        // øker statistikk verdier
        forsvarselement.skade *= 1.5f;
        forsvarselement.helse *= 1.5f;

        // gir skyterange-gameobject ny størrelse
        skyteRange.transform.localScale = str;

        // fjerner oppgraderingskostnad fra penger
        GameManager.instance.penger.fjernPenger(oPris);
    }
}
