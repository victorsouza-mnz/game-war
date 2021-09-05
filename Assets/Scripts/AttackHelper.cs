using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;
public class AttackHelper : MonoBehaviour
{   
    // STATE
    private GameObject[] selectedEnemies = new GameObject[0];
    private GameObject selectedAttackerCountry = null;
    public TMP_InputField inputField;
    private GameObject conqueredCountry;
    private GameObject conquerorCountry;


    //DEPENDENCES
    private SelectCtrl selectCtrl;
    private RollDice rollDice;
    private DiceHelper diceHelper;


    
    public void Start()
    {
        selectCtrl = gameObject.GetComponent<SelectCtrl>();
        rollDice = gameObject.GetComponent<RollDice>();
        diceHelper = gameObject.GetComponent<DiceHelper>();
    }



    public void attackerManager()
    {
        Collider2D countryClicked = getCountryClicked();

        if (countryClicked != null && isOwnCountry(countryClicked))
        {
            selectAttackerCountryAndEnemyNeighbors(countryClicked);
        }
        else if (countryClicked == null && selectedAttackerCountry != null)
        {
            deselectEverythig();
        }
        else if (countryClicked != null && isEnemyCountry(countryClicked))
        {
            int[] resultado = diceHelper.rollDice(getNumberOfAttackSoldiers(), getNumberOfDefenseSoldiers(countryClicked));
            decreaseNumberOfAttackAndDefenseSoldiers(resultado, countryClicked);

            if (winANewTerritory(countryClicked))
            {
                conquerOtherTerritory(countryClicked);
            }
        }

    }

    

    public void conquerOtherTerritory(Collider2D countryClicked)
    {
        countryClicked.gameObject.GetComponent<countryAtributes>().changeCountryOwner(selectCtrl.getCurrentPlayerId());
        this.inputField.gameObject.SetActive(true);
        selectedEnemies = selectedEnemies.Where(val => val != countryClicked.gameObject).ToArray();
        countryClicked.gameObject.GetComponent<LineRenderer>().enabled = false;
        this.inputField.gameObject.SetActive(true);
        conqueredCountry = countryClicked.gameObject;
        conquerorCountry = selectedAttackerCountry;
    }



    public void transferSoldiers() // this runs when palyer press enter with some value in the text input
    {
        
        if (int.Parse(inputField.text) >= conquerorCountry.GetComponent<countryAtributes>().getSoldiers()
            || int.Parse(inputField.text) < 1)
        {
            return;
        }
        conquerorCountry.GetComponent<countryAtributes>().decrementTextNumberSoldiers(int.Parse(inputField.text));
        conqueredCountry.GetComponent<countryAtributes>().incrementTextNumberSoldiers(int.Parse(inputField.text));
        this.inputField.gameObject.SetActive(false);

    }


    public bool winANewTerritory(Collider2D countryClicked)
    {
        return !Convert.ToBoolean(countryClicked.gameObject.GetComponent<countryAtributes>().getSoldiers());
    }

    public void selectAttackerCountryAndEnemyNeighbors(Collider2D countryClicked)
    {
        if (selectedAttackerCountry != null)
        {
            deselectEverythig();
        }

        selectNewAttackerCountryAndEnemyNeighbors(countryClicked);
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

        selectedAttackerCountry.GetComponent<LineRenderer>().enabled = false;
        selectedAttackerCountry = null;
        for (int i = 0; i < selectedEnemies.Length; i++)
        {
            selectedEnemies[i].GetComponent<LineRenderer>().enabled = false;
        }
        selectedEnemies = new GameObject[0];
    }


    public void selectNewAttackerCountryAndEnemyNeighbors(Collider2D countryClicked)
    {
        selectedAttackerCountry = countryClicked.gameObject;
        selectedAttackerCountry.GetComponent<LineRenderer>().enabled = true;
        selectedAttackerCountry.GetComponent<LineRenderer>().materials[0].color = new Color(1f, 1f, 1f);

        GameObject[] neighbors = filterNeigbhors(selectedAttackerCountry.GetComponent<countryAtributes>().neighbors);
        
        for (int i = 0; i < neighbors.Length; i++)
        {
            neighbors[i].GetComponent<LineRenderer>().enabled = true;
            neighbors[i].GetComponent<LineRenderer>().materials[0].color = new Color(1.00000f, 0.84706f, 0.76078f);
            selectedEnemies = neighbors;
        }

    }

    public GameObject[] filterNeigbhors(GameObject[] neigbhors)
    {
        return neigbhors.Where(c => c.GetComponent<countryAtributes>().playerID != selectCtrl.getCurrentPlayerId() && c.GetComponent<countryAtributes>().numberSoldiers < selectedAttackerCountry.GetComponent<countryAtributes>().numberSoldiers).ToArray();
    }


    public bool isEnemyCountry(Collider2D countryClicked)
    {
        return selectedEnemies.Contains(countryClicked.gameObject);
    }


    private int getNumberOfAttackSoldiers()
    {
        if (selectedAttackerCountry.GetComponent<countryAtributes>().getSoldiers() <= 3)
        {
            return selectedAttackerCountry.GetComponent<countryAtributes>().getSoldiers() - 1;
        }

        return 3;
    }

    private int getNumberOfDefenseSoldiers(Collider2D countryClicked)
    {
        if (countryClicked.gameObject.GetComponent<countryAtributes>().getSoldiers() > 3)
        {
            return 3;
        }
        return countryClicked.gameObject.GetComponent<countryAtributes>().getSoldiers();
    }

    private void decreaseNumberOfAttackAndDefenseSoldiers (int[] resultado, Collider2D countryClicked)
    {
        selectedAttackerCountry.GetComponent<countryAtributes>().decrementTextNumberSoldiers(resultado[0]);
        countryClicked.gameObject.GetComponent<countryAtributes>().decrementTextNumberSoldiers(resultado[1]);
    }
}
