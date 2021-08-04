using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDice : MonoBehaviour
{
    private bool won;
    string resultado = " ";
    private Text showResult;

    // Starta o componente Text do resultado
    void Start()
    {
        showResult = GetComponentInChildren<Text>();
        Debug.Log(showResult);
    }

    // Atualizar resultado do lançamento de dado
    void Update()
    {
        showResult.text = resultado;
    }



    public void roll_dice()
    {
        int dice1 = Random.Range(1, 6);
        int dice2 = Random.Range(1, 6);
        

        while (dice1 == dice2)
        {
            dice1 = Random.Range(1, 6);
            dice2 = Random.Range(1, 6);
        }

        if (dice1 > dice2)
        {
            won = true;
        }
        else
        {
            won = false;
        }
        if (won == true)
        {
            resultado = "VENCEU! =)";
        }
        else
        {
            resultado = "PERDEU! =(";
        }
        Debug.Log(won);
        Debug.Log(resultado);
    }
}
