using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    // verdier som blir brukt og forandret i flere script
    // verdier 
    public int runde;
    public float nedteller;
    public float resetNedteller;
    public bool erForberedelsesFase = false;
    public int antallPenger = 1000;
    public int antallPoeng = 0;

    // lister

    // prefabs
    public GameObject fiende;

    // referanser
    public Penger penger;
    public Poeng poeng;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        runde = 0;
        nedteller = 10f;
        resetNedteller = 10f;
    }

    void Start()
    {
        penger = GetComponent<Penger>();
        poeng = GetComponent<Poeng>();
    }
}
