using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DropdownHandler : MonoBehaviour
{
    public Text TextBox;
    public static int playersQuantity;
    // Starta o componente Text do resultado
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        List<string> items = new List<string>();
        items.Add("3");
        items.Add("4");
        items.Add("5");
        items.Add("6");

        //preenche o dropdown com as opções
        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item});
        }
        
        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        playersQuantity = int.Parse(dropdown.options[index].text);

        TextBox.text = "A partida terá " + dropdown.options[index].text + " jogadores";
    }
}
