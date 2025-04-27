using Assets.Constants;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public GameManager gameManager;
    public int levelTime;

    private int timeLeft;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timeLeft = levelTime;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    void Update()
    {
        float xScale = TimerConstants.TIMER_BAR_X_MAX_SCALE * ((float)timeLeft / levelTime);
        transform.localScale = new Vector3(xScale, 0.35f, 1);
        if (timeLeft <= 0)
        {
            gameManager.ShowLoseMenu();
            StopCoroutine(Countdown());
        }
    }

    public int GetTimeLeft()
    {
        return timeLeft;
    }
}
