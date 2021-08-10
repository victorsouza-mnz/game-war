using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countryAtributes : MonoBehaviour
{   
    [SerializeReference]
    public int playerID;
    [SerializeReference]
    public Color color;
    [SerializeReference]
    public int numberSoldiers;
    [SerializeReference]
    public GameObject[] neighbors;


    private Text textNumberSoldiers;

    // Start is called before the first frame update


    void Start()
    {   
        
        gameObject.GetComponent<SpriteRenderer>().color = color;
        textNumberSoldiers = GetComponentInChildren<Text>();
        textNumberSoldiers.text = "" + numberSoldiers;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTextNumberSoldiers()
    {
        this.numberSoldiers--;
        textNumberSoldiers.text = "" + numberSoldiers;
    }   

}
