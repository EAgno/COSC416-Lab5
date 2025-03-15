using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private int score = 0;// Adding score variable
    [SerializeField] private int lives = 3;// Adding lives variable
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float strength = 0.3f;

    [Header("UI")]
    [SerializeField] private CounterUI scoreCounterUI;
    [SerializeField] private CounterUI livesCounterUI;

    private int currentBrickCount;
    private int totalBrickCount;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        // add camera shake here
        CameraShake.Shake(duration, strength);
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        // update score on HUD here
        scoreCounterUI.UpdateCount(++score);

        if (currentBrickCount == 0) SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        // update lives on HUD here
        livesCounterUI.UpdateCount(--maxLives);
        // game over UI if maxLives < 0, then exit to main menu after delay
        ball.ResetBall();
    }
}

