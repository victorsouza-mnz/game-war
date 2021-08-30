using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectCtrl : MonoBehaviour
{

    //MOCK - TODO : Mecanica de turnos
    private int currentPlayer = 2;
    private int currentPlayerSoldiers = 6;
    private string phase = "attack";

    //DECLARATIONS
    private bool isSelected = false;
    private GameObject selectedCountry;
    private LineRenderer outline;

    //HELPERS
    public AttackHelper attackHelper;

    // Start is called before the first frame update
    void Start()
    {
        attackHelper = this.gameObject.GetComponent<AttackHelper>();

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

            selectCountry();

            if (this.phase == "fortification") {
                fortifyCountry();
            }
            else if (this.phase == "attack")
            {

                attackHelper.console();
            }
            
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
                    outline.enabled = true;
                }
                if (isSelected == true) 
                {
                    selectedCountry.gameObject.GetComponent<LineRenderer>().enabled = false;
                    isSelected = true;
                    selectedCountry = hit.collider.gameObject;
                    outline = hit.collider.gameObject.GetComponent<LineRenderer>();
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

}

