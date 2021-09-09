using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;
public class MoveHelper : MonoBehaviour
{   
    // STATE
    private GameObject[] selectedAllies = new GameObject[0];
    private Collider2D[] blockedOriginCountry = new Collider2D[0];
    private GameObject selectedOriginAllyCountry = null;
    public TMP_InputField inputField;
    private GameObject alliedCountry;
    private GameObject originCountry;


    //DEPENDENCES
    private SelectCtrl selectCtrl;


    
    public void Start()
    {
        selectCtrl = gameObject.GetComponent<SelectCtrl>();
    }



    public void moveManager()
    {
        Collider2D countryClicked = getCountryClicked();

        if (countryClicked != null && isAllyCountry(countryClicked))
        {
            if((selectedOriginAllyCountry.GetComponent<countryAtributes>().getSoldiers() - selectedOriginAllyCountry.GetComponent<countryAtributes>().soldiersArrivedInMovementPhase) > 1)
            {
                alterNumberOfOriginCountryandAllySoldiers(countryClicked);
            }
        }

        else if (countryClicked != null && isOwnCountry(countryClicked))
        {
            selectOriginCountryAndAlliesNeighbors(countryClicked);
        }
        
        else if (countryClicked == null && selectedOriginAllyCountry != null)
        {
            deselectEverythig();
        }
    }

    
/*
    public void transferSoldiers() // this runs when palyer press enter with some value in the text input
    {
        
        if (int.Parse(inputField.text) > numberOfSoldiersMoving
            || int.Parse(inputField.text) < 1)
        {
            return;
        }
        originCountry.GetComponent<countryAtributes>().decrementTextNumberSoldiers(int.Parse(inputField.text));
        alliedCountry.GetComponent<countryAtributes>().incrementTextNumberSoldiers(int.Parse(inputField.text));
        this.inputField.gameObject.SetActive(false);

    }
*/
    public void selectOriginCountryAndAlliesNeighbors(Collider2D countryClicked)
    {
        if (selectedOriginAllyCountry != null)
        {
            deselectEverythig();
        }

        selectNewOriginCountryAndAllyNeighbors(countryClicked);
    }



    public Collider2D getCountryClicked()
    {
        Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(raycastPos, Vector2.zero).collider;
    }




    public bool isOwnCountry(Collider2D countryClicked)
    {
        if (countryClicked != null && countryClicked.gameObject.GetComponent<countryAtributes>().playerID == selectCtrl.getCurrentPlayerId())
        {
            return true;
        }
        return false;
    }


    public void deselectEverythig()
    {

        selectedOriginAllyCountry.GetComponent<LineRenderer>().enabled = false;
        selectedOriginAllyCountry = null;
        for (int i = 0; i < selectedAllies.Length; i++)
        {
            selectedAllies[i].GetComponent<LineRenderer>().enabled = false;
        }
        selectedAllies = new GameObject[0];
    }


    public void selectNewOriginCountryAndAllyNeighbors(Collider2D countryClicked)
    {
        //Highlight no país de origem
        selectedOriginAllyCountry = countryClicked.gameObject;
        selectedOriginAllyCountry.GetComponent<LineRenderer>().enabled = true;
        selectedOriginAllyCountry.GetComponent<LineRenderer>().materials[0].color = new Color(1f, 1f, 1f);

        GameObject[] neighbors = filterAllyNeigbhors(selectedOriginAllyCountry.GetComponent<countryAtributes>().neighbors);
        
        //Highlight nos vizinhos
        for (int i = 0; i < neighbors.Length; i++)
        {
            neighbors[i].GetComponent<LineRenderer>().enabled = true;
            neighbors[i].GetComponent<LineRenderer>().materials[0].color = new Color(1.00000f, 0.84706f, 0.76078f);
            selectedAllies = neighbors;
        }

    }

    public GameObject[] filterAllyNeigbhors(GameObject[] neigbhors)
    {
        return neigbhors.Where(c => c.GetComponent<countryAtributes>().playerID == selectCtrl.getCurrentPlayerId()).ToArray();
    }


    public bool isAllyCountry(Collider2D countryClicked)
    {
        return selectedAllies.Contains(countryClicked.gameObject);
    }


    private int getNumberOfOriginAllySoldiers()
    {
        return selectedOriginAllyCountry.GetComponent<countryAtributes>().getSoldiers();
    }

    private void alterNumberOfOriginCountryandAllySoldiers (Collider2D countryClicked)
    {
        if(selectedOriginAllyCountry.GetComponent<countryAtributes>().getSoldiers() != 1)
        {
            selectedOriginAllyCountry.GetComponent<countryAtributes>().decrementTextNumberSoldiers(1);
            countryClicked.gameObject.GetComponent<countryAtributes>().incrementTextNumberSoldiers(1);
            countryClicked.gameObject.GetComponent<countryAtributes>().incrementSoldiersArrived(1);

        }
    }
   
}
