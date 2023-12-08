using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wood")) gameManager.Scores[0]++;
        if (other.gameObject.CompareTag("Stone")) gameManager.Scores[1]++;
        if (other.gameObject.CompareTag("Crystal")) gameManager.Scores[2]++;
    }
  
}
