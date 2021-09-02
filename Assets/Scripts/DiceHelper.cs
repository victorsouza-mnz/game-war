using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceHelper : MonoBehaviour
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




    public void rollDice(int attackSoldiers, int defenseSoldiers) //versão parcial
    {

        List<int> diceResult1 = new List<int>();
        List<int> diceResult2 = new List<int>();

        for (int i = 0; i < attackSoldiers; i++)
        {
            diceResult1.Add(UnityEngine.Random.Range(1, 6));
        }


        for (int i = 0; i < defenseSoldiers; i++)
        {
            diceResult2.Add(UnityEngine.Random.Range(1, 6));
        }

        diceResult1.Sort();
        diceResult2.Sort();
        diceResult1.Reverse();
        diceResult2.Reverse();

        int attackLoss = 0;
        int defLoss = 0;

        for (int i = 0; i < System.Math.Min(attackSoldiers, defenseSoldiers); i++)
        {
            if (diceResult1[i] > diceResult2[i])
            {
                defLoss++;
            }
            else
            {
                attackLoss++;
            }
        }

        if (attackLoss < defLoss)
        {
            won = true;
        }
        else
        {
            won = false;
        }

        Debug.Log("[ O atacante perdeu " + attackLoss + " tropas // A defesa perdeu " + defLoss + " tropas ]");


    }
}
