using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHighScore;
    [SerializeField] private TMP_InputField inputFieldPlayerName;

    // Start button callback, start game
    public void OnStartButtonClicked()
    {
        GameManager gameManager = GameManager.instance;

        // Set current player name (or set to "None" if name is empty string)
        gameManager.playerNameCurrent = inputFieldPlayerName.text.Length > 0 ? inputFieldPlayerName.text : "None";

        // Start the game
        gameManager.StartGame();
    }

    // Exit button callback, exit game
    public void OnExitButtonClicked()
    {
        GameManager.instance.ExitGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameManager.instance;

        // Update highscore text
        textHighScore.text = "Best Score : " + gameManager.playerNameHigh + " : " + gameManager.scoreHigh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
