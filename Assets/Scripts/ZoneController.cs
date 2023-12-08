using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneController : MonoBehaviour
{
    [SerializeField] private Text priceText;
    [SerializeField] private Image imageResurce;
    [SerializeField] private Sprite iconResurce;
    [SerializeField] private int priceZone = 25;
    [SerializeField] private int shooseResurce = 0;
    [SerializeField] private GameObject openTitle;
    private GameManager gameManager;
    private int sellScore = 0;
    private bool isDecreasingWood = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        imageResurce.sprite = iconResurce;
        priceText.text = $"{sellScore} / {priceZone}";
        //priceZone = Random.Range(15, 51);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isDecreasingWood)
            StartCoroutine(DecreaseWoodWithDelay());
        if (sellScore >= priceZone) 
        {
            openTitle.SetActive(true);
            Destroy(gameObject);
        }
    }
    IEnumerator DecreaseWoodWithDelay()
    {
        isDecreasingWood = true;
        while (gameManager.Scores[shooseResurce] > 0)
        {
            gameManager.Scores[shooseResurce]--;
            sellScore++;
            priceText.text = $"{sellScore} / {priceZone}";
            yield return new WaitForSeconds(0.1f);
        }
        isDecreasingWood = false;
    }
}
