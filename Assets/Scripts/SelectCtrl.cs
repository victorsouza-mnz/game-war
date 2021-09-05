using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectCtrl : MonoBehaviour
{

    //MOCK - TODO : Mecanica de turnos
    private int currentPlayer = 2;
    private int currentPlayerSoldiers = 6;
    private string phase = "fortification";

    //DECLARATIONS
    private bool isSelected = false;
    private GameObject selectedCountry;

    private LineRenderer outline;

    //HELPERS
    public AttackHelper attackHelper;

    // Start is called before the first frame update
    void Start()
    {
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
            if (this.phase == "fortification") {
                fortifyCountry();
                selectCountry();
            }
            else if (this.phase == "attack")
            {
                attackHelper.attackerManager();

               
            }

        }
        if (Input.GetMouseButtonUp(1))
        {
            this.phase = "attack";
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

