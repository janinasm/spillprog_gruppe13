using UnityEngine;
using System.Collections;

public class KanonTaarn : Forsvarselement
{

    private Forsvarselement forsvarselement;

    void Start()
    {
        forsvarselement = GetComponent<Forsvarselement>();

        forsvarselement.oppgraderingKostnad = 200;
        forsvarselement.level = 1;
        forsvarselement.helse = 200f;
        forsvarselement.skade = 50;
        forsvarselement.tidMellomAngrip = .5f;
    }
}