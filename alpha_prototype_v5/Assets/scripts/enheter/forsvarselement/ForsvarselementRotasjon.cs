using UnityEngine;
using System.Collections;

public class ForsvarselementRotasjon : MonoBehaviour
{
    int rotasjonGrad = 360 / 8;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && GetComponent<SelectedForsvarselement>().erValgt)
        {
            // roter gameobject med klokken
            transform.Rotate(0, rotasjonGrad, 0);
        }

        if (Input.GetKeyDown(KeyCode.F) && GetComponent<SelectedForsvarselement>().erValgt)
        {
            // roter gameobject med klokken
            transform.Rotate(0, -rotasjonGrad, 0);
        }
    }
}
