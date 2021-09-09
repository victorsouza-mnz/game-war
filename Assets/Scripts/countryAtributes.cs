using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countryAtributes : MonoBehaviour
{   
    
    private Color[] colorArray;
    //[SerializeReference]
    public int playerID; // 0-5
    [SerializeReference]
    private Color color;
    //[SerializeReference]
    public int numberSoldiers;
    [SerializeReference]
    public GameObject[] neighbors;
    private Text textNumberSoldiers;

    // Start is called before the first frame update
    public int soldiersArrivedInMovementPhase;

    void Start()
    {   


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void incrementTextNumberSoldiers(int increment)
    {
        this.numberSoldiers = this.numberSoldiers + increment;
        textNumberSoldiers.text = "" + numberSoldiers;
    }

    public void decrementTextNumberSoldiers(int decrement)
    {
        this.numberSoldiers = this.numberSoldiers - decrement;
        textNumberSoldiers.text = "" + numberSoldiers;
    }

    public void changeCountryOwner(int newPlayerID)
    {
        this.playerID = newPlayerID;
        gameObject.GetComponent<SpriteRenderer>().color = colorArray[playerID]; // recolorir
    }



    public void initialize (int playerID) {
        colorArray = new Color[6]{
            new Color(0.34118f,  0.17255f,  0.53333f), // roxo
            new Color(0.40784f,  0.72157f,  0.20784f), // verde
            new Color(0.07451f,  0.74118f,  0.83137f), // azul
            new Color(0.80000f,  0.09020f,  0.20784f), // vermelho
            //new Color(0.85882f,  0.31373f,  0.49020f), // rosa
            new Color(0.90000f,  0.75490f,  0.00000f), // amarelo
            new Color(0.25098f,  0.25490f,  0.25882f) // cinza
        };

        this.numberSoldiers = 1;
        this.playerID = playerID;
        gameObject.GetComponent<SpriteRenderer>().color = colorArray[playerID];
        textNumberSoldiers = GetComponentInChildren<Text>();
        textNumberSoldiers.text = "" + numberSoldiers;
        this.soldiersArrivedInMovementPhase = 0;
    }
    
    public void incrementSoldiersArrived(int increment)
    {
        this.soldiersArrivedInMovementPhase = this.soldiersArrivedInMovementPhase + increment;
    }


    public int getSoldiers()
    {
        return this.numberSoldiers;
    }

}
