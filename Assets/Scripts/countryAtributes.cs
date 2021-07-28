using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countryAtributes : MonoBehaviour
{

    [SerializeReference]
    private Color cor;
    
    // Start is called before the first frame update


    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = cor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
