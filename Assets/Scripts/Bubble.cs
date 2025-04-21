using Assets.Constants;
using Assets.Enums;
using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb;
    public GameObject nextBubble;
    public Direction initialForceDirection;
    public bool isOriginalBubble;
    public float yStartForce;
    public float bounceHeight;

    private bool HasNextBubble => nextBubble != null;
    private bool HasNeverBounced => bounceCount == 0;

    private int bounceCount = 0;

    void Start()
    {
        RegisterBall();
        AddInitialForce();
    }

    private void RegisterBall()
    {
        if (gameManager == null)
        {
            throw new Exception("GameManager is not assigned");
        }
        if (isOriginalBubble)
        {
            gameManager.AddBubble(gameObject);
        }
    }

    private void AddInitialForce()
    {
        if (isOriginalBubble)
        {
            yStartForce = BallConstants.Y_START_FORCE;
        }
        if (initialForceDirection == Direction.Right)
        {
            rb.AddForce(new Vector2(BallConstants.X_START_FORCE, yStartForce), ForceMode2D.Impulse);
        }
        else if (initialForceDirection == Direction.Left)
        {
            rb.AddForce(new Vector2(-BallConstants.X_START_FORCE, yStartForce), ForceMode2D.Impulse);
        }
    }

    public void Split()
    {
        if (HasNextBubble)
        {
            gameManager.AddBubble(CreateNewBubble(Direction.Right));
            gameManager.AddBubble(CreateNewBubble(Direction.Left));
        }
        Destroy(gameObject);
        gameManager.RemoveBubble(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(WallConstants.FLOOR_TAG))
        {
            BounceBall();
        }
        else if (collision.gameObject.CompareTag(WallConstants.ROOF_TAG))
        {
            Destroy(gameObject);
            gameManager.RemoveBubble(gameObject);
        }
    }

    private void BounceBall()
    {
        bounceCount++;
        rb.linearVelocityY = 0;
        rb.AddForce(Vector2.up * bounceHeight, ForceMode2D.Impulse);
    }

    private GameObject CreateNewBubble(Direction initialForceDirection)
    {
        Vector2 directionVector = initialForceDirection == Direction.Right ? Vector2.right : Vector2.left;
        GameObject gameObject = Instantiate(nextBubble, rb.position + directionVector / BallConstants.BALL_SEPARATION_DIVISOR, Quaternion.identity);
        Bubble newBall = gameObject.GetComponent<Bubble>();
        newBall.gameManager = gameManager;
        newBall.initialForceDirection = initialForceDirection;
        newBall.isOriginalBubble = false;
        if (HasNeverBounced)
        {
            newBall.yStartForce = yStartForce * BallConstants.Y_START_FORCE_MULTIPLIER;
        }
        else
        {
            newBall.yStartForce = yStartForce;
        }
        return gameObject;
    }
}
