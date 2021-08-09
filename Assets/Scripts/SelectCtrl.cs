using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCtrl : MonoBehaviour
{

    private bool isSelected = false;
    private GameObject selectedCountry;
    private LineRenderer outline;


    // Start is called before the first frame update
    void Start()
    {
        
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
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);
                
            if(hit.collider != null)
            {
                
                for (int i =0; i < hit.collider.gameObject.GetComponent<countryAtributes>().neighbors.Length; i++)
                {
                    Debug.Log("pais : " + hit.collider.gameObject.GetComponent<countryAtributes>().neighbors[i].gameObject.name);
                }
                if (isSelected == true)
                {
                    selectedCountry.gameObject.GetComponent<LineRenderer>().enabled = false; 
                }
                isSelected = true;
                selectedCountry = hit.collider.gameObject;
                outline = hit.collider.gameObject.GetComponent<LineRenderer>();
                outline.enabled = !outline.enabled;

                hit.collider.gameObject.GetComponent<countryAtributes>().setTextNumberSoldiers();
                
            }
            else
            {
                if (isSelected == true)
                {
                    selectedCountry.gameObject.GetComponent<LineRenderer>().enabled = !selectedCountry.gameObject.GetComponent<LineRenderer>().enabled;
                    isSelected = false;
                }
            }
        }
    }
}
