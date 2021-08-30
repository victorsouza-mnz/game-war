using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHelper : MonoBehaviour
{
    private GameObject[] selectedEnemies;
    private Gradient red;


    
    public void Start()
    {

    }

    public void showNeighbors(GameObject selectedCountry)
    {
        GameObject[] neighbors = selectedCountry.GetComponent<countryAtributes>().neighbors;
        
        if (selectedEnemies != null)
        {
            for (int j = 0; j < selectedEnemies.Length; j++)
            {
                if (selectedEnemies[j] != selectedCountry)
                {
                    selectedEnemies[j].GetComponent<LineRenderer>().enabled = false;
                }
            }
        }

        for ( int i = 0; i < neighbors.Length; i++)
        {   
            neighbors[i].GetComponent<LineRenderer>().enabled = true;
            neighbors[i].GetComponent<LineRenderer>().materials[0].color = new Color(0.80000f, 0.09020f, 0.20784f);
            selectedEnemies = neighbors;
        }
    }

}
