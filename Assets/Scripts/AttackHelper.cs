using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class AttackHelper : MonoBehaviour
{   
    // STATE
    private GameObject[] selectedEnemies = new GameObject[0];
    private GameObject selectedAttackerCountry = null;

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
            diceHelper.rollDice(getNumberOfAttackSoldiers(), countryClicked.gameObject.GetComponent<countryAtributes>().getSoldiers());
        }
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

        return selectedAttackerCountry.GetComponent<countryAtributes>().getSoldiers();
    }
}
