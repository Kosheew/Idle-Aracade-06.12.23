using UnityEngine;
using UnityEngine.UI;
using SaveData;
using UnityEngine.UIElements;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Text TextWood;
    [SerializeField] private Text TextStone;
    [SerializeField] private Text TextCrystal;
    private const string saveKey = "mainSave";
    private Vector3 player;
    private Vector3 PlayerSavePosition;
    public int[] Scores = {0, 0, 0};
    private Movement searchplayer;
    private void Awake()
    {
        Load();
        player = PlayerSavePosition;
        FindObjectOfType<Movement>().transform.position = player;
    }
        
    private void LateUpdate()
    {
        TextWood.text = Scores[0].ToString();
        TextStone.text = Scores[1].ToString();
        TextCrystal.text = Scores[2].ToString();
        player = FindObjectOfType<Movement>().transform.position;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        PlayerSavePosition = new Vector3(player.x, player.y, player.z);
        SaveManager.Save(saveKey, GetSave());
    }


    private void Load()
    {

        var data = SaveManager.Load<Saves>(saveKey);
        Scores = data.dataIntegerScores;
        PlayerSavePosition = data.dataTransform;
    }
  
    private Saves GetSave()
    {
        var saves = new Saves()
        {
            dataIntegerScores = Scores,
            dataTransform = PlayerSavePosition
        };
        return saves;
    }

    public void Restart()
    {
        Scores = new int[] { 0, 0, 0 };
        PlayerSavePosition = new Vector3(0, 0, 0);
        SaveManager.Save(saveKey, GetSave());
    }
}
