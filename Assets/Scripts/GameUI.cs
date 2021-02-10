using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{

    public Text moves;
    public Text gameOverText;
    public Init init;
    int minMoves; //2^n − 1
    public GameObject mainMenu;
    public GameObject hanoiPlatform;


    public void CountMinMoves(int hanoiRingCount)
    {
        minMoves = (int)Mathf.Pow(2, hanoiRingCount) - 1;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMovesText(int moveCount)
    {
        moves.text = moveCount.ToString() + "/" + minMoves;
    }

    public void GameWonText()
    {
        gameOverText.text = "Game finished";
        gameOverText.gameObject.SetActive(true);
    }

    public void OpenMenu()
    {
        mainMenu.SetActive(true);
    }
}
