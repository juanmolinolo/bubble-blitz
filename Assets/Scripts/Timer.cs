using Assets.Constants;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    void Start()
    {
        gameManager.timeLeft = gameManager.levelTime;
        StartCoroutine(Countdown());
    }

    void Update()
    {
        float xScale = TimerConstants.TIMER_BAR_X_MAX_SCALE * ((float)gameManager.timeLeft / gameManager.levelTime);
        transform.localScale = new Vector3(xScale, 0.35f, 1);
        if (gameManager.timeLeft <= 0)
        {
            gameManager.ShowLoseMenu();
            StopCoroutine(Countdown());
        }
    }

    private IEnumerator Countdown()
    {
        while (gameManager.timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            gameManager.timeLeft--;
        }
    }
}
