using System.Collections;
using UnityEngine;

public class Kampfase : MonoBehaviour
{
    public int antallFienderPerWave;
    public int antallWave;
    public int antallFiender;
    public int spawnedeFiender;

    private float ventMedGruppe;
    private float ventMedFiende;

    private bool erIgangMedSpawning = false;
    private bool erForsteWave;

    // gameobject referanser
    public Transform fiendeHolder;

    // gui referanser
    private FaseSkifte faseSkifte;
    private FaseGUI faseGUI;
    private Forberedelsesfase forberedelsesfase;

    void Start()
    {
        // cacher referanser
        faseSkifte = GetComponent<FaseSkifte>();
        faseGUI = GetComponent<FaseGUI>();
        forberedelsesfase = GetComponent<Forberedelsesfase>();

        // setter verdier
        spawnedeFiender = fiendeHolder.transform.childCount;

        antallFienderPerWave = 3;
        antallWave = 1;
        antallFiender = 0;

        ventMedGruppe = 5f;
        ventMedFiende = 1f;
    }

    void Update()
    {
        // dersom det ikke er forbereldesfase
        if (!GameManager.instance.erForberedelsesFase)
        {
            // TEST-metode som lar spilleren avslutte kampfase med "U"
            avsluttKampfase();

            // metode som sjekker om kampfasen skal avsluttes
            sjekkOmAlleFienderErDrept();
        }
    }

    public void startKampfase()
    {
        // disabler gameobject som viser GUI for funksjonalitet som ikke kan gjøres i kampfase
        faseGUI.slotContainer.SetActive(false);

        // sier at det er første wave av fiender i denne runden
        erForsteWave = true;

        // starter metoden som spawner fiender
        // den skal bare starte om det ikke er igang med spawning 
        // (ikke sikkert om sjekken trengs, men lar den stå)
        if (!erIgangMedSpawning) StartCoroutine("spawnFiender");
    }

    public IEnumerator spawnFiender()
    {
        // det velges et eget spawnpoint for hver gruppe (wave) av fiender i per kampfase
        // for antall runder som har gått skal det kjøres en egen for loop
        for (int i = 0; i < GameManager.instance.runde; i++)
        {
            // dersom det ikke er den første gruppen av fiender skal det ventes før neste gruppe
            if (!erForsteWave)
            {
                // venter med neste gruppe
                yield return new WaitForSeconds(ventMedGruppe);
            }

            // det er ikke lenger første wave av fiender
            erForsteWave = false;

            // denne loopen kjøres for hver fiende vi har i en gruppe (wave)
            for (int j = 0; j < antallFienderPerWave; j++)
            {
                // venter med å lage neste fiende
                yield return new WaitForSeconds(ventMedFiende);

                GameObject fiendeInstance;

                // instantiater (spawner) fiende på plasseringen til spawnpoint fra listen med tilfeldige spawnpoints
                fiendeInstance = Instantiate(GameManager.instance.fiende, forberedelsesfase.randSpawnpointListe[i].transform.position, Quaternion.identity) as GameObject;

                // legger de i et annet gameobject (for orden i hierarkiet)
                fiendeInstance.transform.parent = fiendeHolder.transform;

                // øker for hver fiende vi legger til
                antallFiender++;

                // vi er i gang med spawning
                erIgangMedSpawning = true;
            }
        }

        // vi er ferdige med spawning
        erIgangMedSpawning = false;
    }

    // midlertidig måte å finne ut om alle fiendene er drept
    public void sjekkOmAlleFienderErDrept()
    {
        // henter antallet fiender som finnes i spillverden
        spawnedeFiender = fiendeHolder.transform.childCount;

        // hvis antall fiender i spillverden er 0, 
        // og antallet fiender som er blitt spawnet er likt antallet fiender som skal spawne i denne runden
        if (spawnedeFiender == 0 && antallFiender == (antallFienderPerWave * GameManager.instance.runde))
        {
            // reset antall fiender
            antallFiender = 0;

            // kjør metode som resetter lys
            resetSpawnpointLys();

            // sett forberedelsfase
            GameManager.instance.erForberedelsesFase = true;
            faseSkifte.SkiftFase(GameManager.instance.erForberedelsesFase);
        }
    }

    // FOR TESTING: midlertidig måte å avslutte kampfase uten å ha drept alle fiender
    public void avsluttKampfase()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            slettAlleFiender();
            GameManager.instance.erForberedelsesFase = true;
            faseSkifte.SkiftFase(GameManager.instance.erForberedelsesFase);
        }
    }

    public void slettAlleFiender()
    {
        // kjør metode som resetter lys
        resetSpawnpointLys();

        // henter antall fiender i fiendeholderen
        int childs = fiendeHolder.childCount;

        // sletter fiendene
        for (int i = 0; i < childs; i++)
        {
            GameObject.Destroy(fiendeHolder.GetChild(i).gameObject);
        }
    }

    // metode som resetter lys
    public void resetSpawnpointLys()
    {
        // for hvert lys i listen over tilfelige spawnpoints
        for (int i = 0; i < forberedelsesfase.randSpawnpointListe.Count; i++)
        {
            forberedelsesfase.randSpawnpointListe[i].GetComponent<Light>().enabled = false;
            forberedelsesfase.randSpawnpointListe[i].GetComponent<Light>().color = Color.yellow;
            forberedelsesfase.randSpawnpointListe[i].GetComponent<Light>().intensity = 1;
        }

        // tømmer lista med randomiserte spawnpoints
        forberedelsesfase.randSpawnpointListe.Clear();
    }
}
