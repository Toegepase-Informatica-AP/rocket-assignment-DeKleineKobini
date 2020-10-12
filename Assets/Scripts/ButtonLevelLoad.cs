using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevelLoad : MonoBehaviour
{
    public string mLevelToLoad;

    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void LoadLevel()
    {
        // Load the new level
        SceneManager.LoadScene(mLevelToLoad);
        gameController.state = GameController.GameState.Playing;
    }
}
