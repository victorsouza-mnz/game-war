using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceHelper : MonoBehaviour
{
    public int[] rollDice(int attackSoldiers, int defenseSoldiers) //versão parcial
    {

        List<int> diceResult1 = new List<int>();
        List<int> diceResult2 = new List<int>();

        for (int i = 0; i < attackSoldiers; i++)
        {
            diceResult1.Add(UnityEngine.Random.Range(1, 7));
        }


        for (int i = 0; i < defenseSoldiers; i++)
        {
            diceResult2.Add(UnityEngine.Random.Range(1, 7));
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

        return (new int[2] { attackLoss, defLoss });


    }
}
