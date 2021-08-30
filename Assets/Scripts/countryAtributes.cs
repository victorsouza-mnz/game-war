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


    void Start()
    {   
        initialize();

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




    private void initialize () {
        colorArray = new Color[6]{
            new Color(0.34118f,  0.17255f,  0.53333f), // roxo
            new Color(0.40784f,  0.72157f,  0.20784f), // verde
            new Color(0.07451f,  0.74118f,  0.83137f), // azul
            new Color(0.85882f,  0.31373f,  0.49020f), // rosa
            new Color(0.90000f,  0.75490f,  0.00000f), // amarelo
            new Color(0.25098f,  0.25490f,  0.25882f) // cinza
        };

        this.numberSoldiers = 1;
        this.playerID = UnityEngine.Random.Range(0,6);
        gameObject.GetComponent<SpriteRenderer>().color = colorArray[playerID];
        textNumberSoldiers = GetComponentInChildren<Text>();
        textNumberSoldiers.text = "" + numberSoldiers;
    }   

}
