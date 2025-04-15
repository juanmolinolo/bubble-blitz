using Assets.Constants;
using Assets.Enums;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject nextBall;
    public Direction initialForceDirection;
    public bool isOriginalBall;
    public float yStartForce;
    public float bounceHeight;

    private bool HasNextBall => nextBall != null;
    private bool HasNeverBounced => bounceCount == 0;

    private int bounceCount = 0;

    void Start()
    {
        if (isOriginalBall)
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
        if (HasNextBall)
        {
            CreateNewBall(Direction.Right);
            CreateNewBall(Direction.Left);
        }
        Destroy(gameObject);
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
        }
    }

    private void BounceBall()
    {
        bounceCount++;
        rb.linearVelocityY = 0;
        rb.AddForce(Vector2.up * bounceHeight, ForceMode2D.Impulse);
    }

    private void CreateNewBall(Direction initialForceDirection)
    {
        Vector2 directionVector = initialForceDirection == Direction.Right ? Vector2.right : Vector2.left;
        GameObject gameObject = Instantiate(nextBall, rb.position + directionVector / BallConstants.BALL_SEPARATION_DIVISOR, Quaternion.identity);
        Bubble newBall = gameObject.GetComponent<Bubble>();
        newBall.initialForceDirection = initialForceDirection;
        newBall.isOriginalBall = false;
        if (HasNeverBounced)
        {
            newBall.yStartForce = yStartForce * BallConstants.Y_START_FORCE_MULTIPLIER;
        }
        else
        {
            newBall.yStartForce = yStartForce;
        }
    }
}
