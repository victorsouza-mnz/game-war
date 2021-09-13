using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectCtrl : MonoBehaviour
{

    //MOCK - TODO : Mecanica de turnos
    private int currentPlayer;
    private List<int> currentPlayerSoldiers = new List<int>();
    private string[] phases = new string[3];
    private int i = 0;
    private Text textCurrentPlayer;

    //DECLARATIONS
    private bool isSelected = false;
    private GameObject selectedCountry;

    private LineRenderer outline;

    //HELPERS
    public AttackHelper attackHelper;
    public MoveHelper moveHelper;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < DropdownHandler.playersQuantity; i++) {
            currentPlayerSoldiers.Add(6);
        }
        phases[0] = "fortification";
        phases[1] = "attack";
        phases[2] = "movement";
        moveHelper = gameObject.GetComponent<MoveHelper>();
        attackHelper = gameObject.GetComponent<AttackHelper>();
        textCurrentPlayer = GetComponentInChildren<Text>();
        textCurrentPlayer.text = "Jogador atual: " + (this.currentPlayer+1);

    }   

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Jogador atual: "+ this.currentPlayer);
        MouseInput();
    }


    void MouseInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (this.phases[i] == "fortification") {
                fortifyCountry();
                selectCountry();
            }
            else if (this.phases[i] == "attack")
            {
                attackHelper.attackerManager();
            }
            else if (this.phases[i] == "movement")
            {
                moveHelper.moveManager();
            }

        }
        if (Input.GetMouseButtonUp(1))
        {
            i++;
            if(i > 2) i=0;
        }
    }


    public void selectCountry() {
        Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

        if (hit.collider != null) {
            if (hit.collider.gameObject.GetComponent<countryAtributes>().playerID == this.currentPlayer){
                if (isSelected == false) 
                {
                    isSelected = true;
                    selectedCountry = hit.collider.gameObject;
                    outline = hit.collider.gameObject.GetComponent<LineRenderer>();
                    selectedCountry.GetComponent<LineRenderer>().materials[0].color = new Color(1.00000f, 1.00000f, 1.00000f);
                    outline.enabled = true;

                }
                else if (isSelected == true) 
                {
                    selectedCountry.gameObject.GetComponent<LineRenderer>().enabled = false;
                    isSelected = true;
                    selectedCountry = hit.collider.gameObject;
                    outline = hit.collider.gameObject.GetComponent<LineRenderer>();
                    selectedCountry.GetComponent<LineRenderer>().materials[0].color = new Color(1.00000f, 1.00000f, 1.00000f);
                    outline.enabled = true;
                }
            }

        } else {
            if (isSelected)
                {
                    isSelected = false;
                    outline = selectedCountry.gameObject.GetComponent<LineRenderer>();
                    selectedCountry = null;
                    outline.enabled = false;
                }
        }
    }


    public void fortifyCountry (){
        Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

        if (hit.collider != null) {
            if (isSelected == true && hit.collider.gameObject == selectedCountry && currentPlayerSoldiers[getCurrentPlayerId()] > 0) {
                hit.collider.gameObject.GetComponent<countryAtributes>().incrementTextNumberSoldiers(1);
                currentPlayerSoldiers[getCurrentPlayerId()]--;
            }
        }
    }

    public void passarTurno () 
    {
        int players = DropdownHandler.playersQuantity - 1;
        if (this.currentPlayer == players){
            this.currentPlayer = 0;
        } else {
            this.currentPlayer+=1;
        }
        this.i = 0;
        moveHelper.deselectEverythig();
        changeTextPlayer();
    }

    public void changeTextPlayer() {
        textCurrentPlayer.text = "Jogador atual: " + (this.currentPlayer+1);
        return;
    }

    public int getCurrentPlayerId()
    {
        return this.currentPlayer;
    }

}
