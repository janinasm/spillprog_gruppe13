using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Forberedelsesfase : MonoBehaviour
{
    public int randomTall;

    public List<GameObject> spawnpointListe = new List<GameObject>();
    public List<GameObject> randSpawnpointListe = new List<GameObject>();

    // script referanser
    private FaseGUI faseGUI;

    // Use this for initialization
    void Start()
    {
        // cacher referanser
        faseGUI = GetComponent<FaseGUI>();

        // for hvert gameobject i scenen
        foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            // hvis gameobjektet har denne taggen
            if (go.tag == "Spawnpoint")
            {
                // legg objektet i listen over spawnpoints
                spawnpointListe.Add(go);
            }
        }
    }

    public void startForberedelsesFase()
    {
        // resetter nedtelleren
        GameManager.instance.nedteller = GameManager.instance.resetNedteller;

        // øker runde med 1
        GameManager.instance.runde++;

        // setter fase og runde tekst
        faseGUI.faseText.text = "Forberedelsesfase";
        faseGUI.rundeText.text = "Runde " + GameManager.instance.runde.ToString();

        // aktiverer gameobjektet som har GUI som kan brukes i denne fasen
        faseGUI.slotContainer.SetActive(true);

        // for antall runder som har gått skal det l
        for (int i = 0; i < GameManager.instance.runde; i++)
        {
            // henter et tilfeldig tall mellom 0 og antall i spawnpointslisten
            randomTall = Random.Range(0, spawnpointListe.Count);

            // henter en tilfeldig plass i spawnpointlisten og legger til i liste over tilfeldige spawnpoints
            randSpawnpointListe.Add(spawnpointListe[randomTall]);
        }

        // kjører metode som viser lys der fiendene skal komme fra i kampfasen
        settSpawnpointLys();
    }

    // viser hvor fiendene skal komme fra i kampfasen
    public void settSpawnpointLys()
    {
        // for hvert lys i listen over tilfelige spawnpoints
        for (int i = 0; i < GameManager.instance.runde; i++)
        {
            // hvis lyset er av
            if (!randSpawnpointListe[i].GetComponent<Light>().enabled)
            {
                // setter på lyset
                randSpawnpointListe[i].GetComponent<Light>().enabled = true;
            }

            // hvis lyset har intensitet 4 eller mindre
            else if (randSpawnpointListe[i].GetComponent<Light>().intensity <= 4)
            {
                // hvis lyset er på forandres fargen og intensiteten økes med 1
                randSpawnpointListe[i].GetComponent<Light>().color = Color.white;
                randSpawnpointListe[i].GetComponent<Light>().intensity++;
            }

            else
            {
                // hvis lyset er på forandres fargen og intensiteten økes med 1
                randSpawnpointListe[i].GetComponent<Light>().color = Color.cyan;
                randSpawnpointListe[i].GetComponent<Light>().intensity++;
            }
        }
    }
}
