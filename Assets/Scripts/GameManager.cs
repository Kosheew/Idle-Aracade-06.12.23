using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Text TextWood;
    [SerializeField] private Text TextStone;
    [SerializeField] private Text TextCrystal;

    public int[] Scores = {0, 0, 0};


    private void LateUpdate()
    {
        TextWood.text = Scores[0].ToString();
        TextStone.text = Scores[1].ToString();
        TextCrystal.text = Scores[2].ToString();
    }
}
