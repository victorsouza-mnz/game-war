using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectCtrl : MonoBehaviour
{

    //MOCK - TODO : Mecanica de turnos
    private int currentPlayer = 2;
    private int currentPlayerSoldiers = 6;
    private string[] phases = new string[3];
    private int i = 0;

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
        phases[0] = "fortification";
        phases[1] = "attack";
        phases[2] = "movement";
        moveHelper = gameObject.GetComponent<MoveHelper>();
        attackHelper = gameObject.GetComponent<AttackHelper>();

    }   

    // Update is called once per frame
    void Update()
    {
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
            if (isSelected == true && hit.collider.gameObject == selectedCountry && currentPlayerSoldiers > 0) {
                hit.collider.gameObject.GetComponent<countryAtributes>().incrementTextNumberSoldiers(1);
                currentPlayerSoldiers--;
            }
        }
    }


    public int getCurrentPlayerId()
    {
        return this.currentPlayer;
    }

}
