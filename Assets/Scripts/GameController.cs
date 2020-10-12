using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { Playing, GameOver, Finished }

    public GameObject player;
    public GameObject mainCanvas;
    public GameObject gameOverCanvas;
    public GameObject finishedCanvas;

    internal GameState state = GameState.Playing;
    private GameState previousState;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");

        health = player.GetComponent<Health>();

        if (mainCanvas != null)
            mainCanvas.SetActive(true);
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);
        if (finishedCanvas != null)
            finishedCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.Playing:
                if (health != null && !health.isAlive)
                {
                    state = GameState.GameOver;
                }

                HandleCanvases();
                break;
            case GameState.GameOver:
                if (health != null && health.isAlive)
                {
                    state = GameState.Playing;
                }

                HandleCanvases();
                break;
            case GameState.Finished:
                HandleCanvases();
                break;
            default:
                break;
        }

        previousState = state;
    }

    private void HandleCanvases()
    {
        if (previousState == state) return;

        switch (state)
        {
            case GameState.Playing:
                if (mainCanvas != null)
                    mainCanvas.SetActive(true);
                if (gameOverCanvas != null)
                    gameOverCanvas.SetActive(false);
                if (finishedCanvas != null)
                    finishedCanvas.SetActive(false);
                break;
            case GameState.GameOver:
                if (mainCanvas != null)
                    mainCanvas.SetActive(false);
                if (gameOverCanvas != null)
                    gameOverCanvas.SetActive(true);
                if (finishedCanvas != null)
                    finishedCanvas.SetActive(false);
                break;
            case GameState.Finished:
                if (mainCanvas != null)
                    mainCanvas.SetActive(false);
                if (gameOverCanvas != null)
                    gameOverCanvas.SetActive(false);
                if (finishedCanvas != null)
                    finishedCanvas.SetActive(true);
                break;
            default:
                break;
        }
    }
}
