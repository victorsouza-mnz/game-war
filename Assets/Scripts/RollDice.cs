using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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



    public void roll_dice() //versão parcial
    {
        int troop1 = 3;
        int troop2 = 3;
        List<int> diceResult1 = new List<int>();
        List<int> diceResult2 = new List<int>();

        for (int i = 0; i < troop1; i++) {
            diceResult1.Add(UnityEngine.Random.Range(1, 6));
        }


        for (int i = 0; i < troop2; i++) {
            diceResult2.Add(UnityEngine.Random.Range(1, 6));
        }

        diceResult1.Sort();
        diceResult2.Sort();
        diceResult1.Reverse();
        diceResult2.Reverse();

        int attackLoss = 0;
        int defLoss = 0;
    
        for(int i = 0; i < System.Math.Min(troop1, troop2); i++){
            if (diceResult1[i] > diceResult2[i]){
                defLoss++;
            } else {
                attackLoss++;
            }
        }

        if (attackLoss < defLoss) {
            won = true;
        } else {
            won = false;
        }

        if (won == true)
        {
            resultado = "[ O atacante perdeu " + attackLoss + " tropas // A defesa perdeu " + defLoss + " tropas ]";
        } else {
            resultado = "[ O atacante perdeu " + attackLoss + " tropas // A defesa perdeu " + defLoss + " tropas ]";
        }
    }
}
