using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    private GameManager gameManager;
    private int sellPrice = 10;
    private bool isDecreasingWood = false;
    private bool isDecreasingStone = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isDecreasingWood && gameManager.Scores[0] >= 10)
            StartCoroutine(DecreaseWoodWithDelay());
        if (!isDecreasingStone && gameManager.Scores[1] >= 10)
            StartCoroutine(DecreaseStoneWithDelay());
    }

    IEnumerator DecreaseWoodWithDelay()
    {
        isDecreasingWood = true;
        while (gameManager.Scores[0] > 0)
        {
            gameManager.Scores[0]--;
            sellPrice--;
            if(sellPrice <= 0)
            {
                sellPrice = 10;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        gameManager.Scores[1] += 5;
        isDecreasingWood = false;
    }

    IEnumerator DecreaseStoneWithDelay()
    {
        isDecreasingStone = true;
        while (gameManager.Scores[1] > 0)
        {
            gameManager.Scores[1]--;
            sellPrice--;
            if (sellPrice <= 0)
            {
                sellPrice = 10;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        gameManager.Scores[2] += 5;
        isDecreasingStone = false;
    }

}
