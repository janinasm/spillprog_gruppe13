using UnityEngine;
using System.Collections;

public class FiendePathFinding : MonoBehaviour {

    // Unity har en egen navigasjonskomponent som gjør veldig mye av arbeidet.
    // I editoren har vi laget navmesh på veiene der fienden skal kunne gå.
    // Vi lager noen enkle primitiver der veiene skal gå, og velger Navigation i menyen.
    // Disse må krysses av som "static" i inspektoren og deretter "bakes" til NavMesh i navigation-panen.
    // Vi har lagt til en NavAgent-komponent på fiendeprefabs, det er denne som styrer fiendene.
    // I skriptet lager vi en destinasjon for fiendene.
    // http://unity3d.com/learn/tutorials/modules/beginner/navigation/navigation-overview


    // objektet som er destinasjonen for fiendene
    public GameObject landsbyPrefab;

    // komponent referanse
    private NavMeshAgent agent;

    void Start()
    {
        // cacher komponent
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // setter fiendens destinasjon til Landsbyen
        agent.SetDestination(landsbyPrefab.transform.position);
    }
}
