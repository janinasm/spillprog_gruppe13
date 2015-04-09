using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Landsby : MonoBehaviour
{
    public float helse;
    public int skade;
    public float tidMellomAngrip;

    void Start()
    {
        helse = 1000f;
        skade = 100;
        tidMellomAngrip = 3f;
    }
}
