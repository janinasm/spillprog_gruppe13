using UnityEngine;
using System.Collections;

public class SlettForsvarselement : MonoBehaviour
{
    private int oppgraderingVerdi;
    private int level;

    // script referanser
    private Forsvarselement forsvarselement;

    void Start()
    {
        // cacher referanser
        forsvarselement = GetComponent<Forsvarselement>();
    }

    public void Selg()
    {
        // henter verdier
        level = forsvarselement.level;
        oppgraderingVerdi = forsvarselement.oppgraderingKostnad;


        if (level == 1)
        {
            GameManager.instance.penger.leggTilPenger(oppgraderingVerdi * 1 / 2);
        }
        else if (level == 2)
        {
            GameManager.instance.penger.leggTilPenger(oppgraderingVerdi * 1);
        }
        else if (level == 3)
        {
            GameManager.instance.penger.leggTilPenger(oppgraderingVerdi * 3 / 2);
        }

        // slett gameobjektet
        Destroy(this.gameObject);
    }
}
