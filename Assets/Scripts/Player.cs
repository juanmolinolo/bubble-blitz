using Assets.Constants;
using Assets.Enums;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Animator animator;

    private Direction moveDirection = Direction.Idle;
    private Direction lastDirectionPressed = Direction.Idle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            lastDirectionPressed = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            lastDirectionPressed = Direction.Right;
        }

        bool leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        if (leftPressed && !rightPressed)
        {
            MovePlayer(Direction.Left);
        }
        else if (rightPressed && !leftPressed)
        {
            MovePlayer(Direction.Right);
        }
        else if (rightPressed && leftPressed)
        {
            MovePlayer(lastDirectionPressed);
        }
        else
        {
            moveDirection = Direction.Idle;
        }

        animator.SetInteger(PlayerConstants.MOVEMENT_ANIMATION_VARIABLE, (int)moveDirection);
    }

    private void MovePlayer(Direction direction)
    {
        Vector2 currentPosition = transform.position;
        
        if (direction == Direction.Left)
        {
            currentPosition.x -= PlayerConstants.MOVEMENT_SPEED * Time.deltaTime;
        }
        else if (direction == Direction.Right)
        {
            currentPosition.x += PlayerConstants.MOVEMENT_SPEED * Time.deltaTime;
        }

        transform.position = currentPosition;
        moveDirection = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(BubbleConstants.BUBBLE_TAG))
        {
            gameManager.ShowLoseMenu();
        }
    }
}
