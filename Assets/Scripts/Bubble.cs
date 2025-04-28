using Assets.Constants;
using Assets.Enums;
using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    #region Class properties

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject nextBubble;

    [SerializeField]
    private Direction initialForceDirection;

    [SerializeField]
    private bool isOriginalBubble;

    [SerializeField]
    private float yStartForce;

    [SerializeField]
    private float bounceHeight;

    [SerializeField]
    private int scoreValue;

    #endregion Class properties

    private bool HasNextBubble => nextBubble != null;
    private bool HasNeverBounced => bounceCount == 0;

    private int bounceCount = 0;

    #region Events

    void Start()
    {
        RegisterBubble();
        AddInitialForce();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(WallConstants.FLOOR_TAG))
        {
            BounceBubble();
        }
        else if (collision.gameObject.CompareTag(WallConstants.ROOF_TAG))
        {
            Destroy(gameObject);
            gameManager.RemoveBubble(this);
        }
    }

    #endregion Events

    public void Split()
    {
        if (HasNextBubble)
        {
            gameManager.AddBubble(CreateNewBubble(Direction.Right));
            gameManager.AddBubble(CreateNewBubble(Direction.Left));
        }
        Destroy(gameObject);
        gameManager.RemoveBubble(this);
    }

    public int GetBubbleScore(double timeLeftAsPercentage)
    {
        return (int)Math.Round(scoreValue * timeLeftAsPercentage / 100, 0);
    }

    #region Helpers

    private void RegisterBubble()
    {
        if (isOriginalBubble)
        {
            gameManager.AddBubble(this);
        }
    }

    private void AddInitialForce()
    {
        if (isOriginalBubble)
        {
            yStartForce = BubbleConstants.Y_START_FORCE;
        }
        if (initialForceDirection == Direction.Right)
        {
            rb.AddForce(new Vector2(BubbleConstants.X_START_FORCE, yStartForce), ForceMode2D.Impulse);
        }
        else if (initialForceDirection == Direction.Left)
        {
            rb.AddForce(new Vector2(-BubbleConstants.X_START_FORCE, yStartForce), ForceMode2D.Impulse);
        }
    }

    private void BounceBubble()
    {
        bounceCount++;
        scoreValue--;
        rb.linearVelocityY = 0;
        rb.AddForce(Vector2.up * bounceHeight, ForceMode2D.Impulse);
    }

    private Bubble CreateNewBubble(Direction initialForceDirection)
    {
        Vector2 directionVector = initialForceDirection == Direction.Right ? Vector2.right : Vector2.left;
        GameObject gameObject = Instantiate(nextBubble, rb.position + directionVector / BubbleConstants.BUBBLE_SEPARATION_DIVISOR, Quaternion.identity);
        Bubble newBubble = gameObject.GetComponent<Bubble>();
        newBubble.gameManager = gameManager;
        newBubble.initialForceDirection = initialForceDirection;
        newBubble.isOriginalBubble = false;
        newBubble.spriteRenderer.color = spriteRenderer.color;
        if (HasNeverBounced)
        {
            newBubble.yStartForce = yStartForce * BubbleConstants.Y_START_FORCE_MULTIPLIER;
        }
        else
        {
            newBubble.yStartForce = yStartForce;
        }
        return newBubble;
    }

    #endregion Helpers
}
