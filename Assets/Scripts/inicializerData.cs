using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inicializerData : MonoBehaviour
{

    public int numberOfPlayers;
    public GameObject[] countries;

    void Start()
    {
        numberOfPlayers = 4;
        countries = GameObject.FindGameObjectsWithTag("Country");
        inicializerCountryOwners(numberOfPlayers, countries);
    }
    
       
    private void inicializerCountryOwners(int numberOfPlayers, GameObject[] countries)
    {
        int divider = 42 / numberOfPlayers;

        for (int i = 0; i < countries.Length; i ++)
        {
            GameObject temp = countries[i];
            int r = Random.Range(i, countries.Length);
            countries[i] = countries[r];
            countries[r] = temp;
        }

        for (int j = 0; j < numberOfPlayers; j++)
        {
            for (int k = 0; k < divider; k++)
            {
                countries[k + j*divider].GetComponent<countryAtributes>().initialize(j);
            }
        }

        for (int m = 0; m < 42 - divider * numberOfPlayers; m++)
        {
            countries[m + divider * numberOfPlayers].GetComponent<countryAtributes>().initialize(Random.Range(0, numberOfPlayers + 1));
        }

    }


}
