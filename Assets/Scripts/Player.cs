using Assets.Constants;
using Assets.Enums;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip punchClip;

    private Direction moveDirection = Direction.Idle;

    private Direction lastDirectionPressed;

    #region Events

    private void Update()
    {
        SetLastDirectionPressed();

        bool leftCurrentlyPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool rightCurrentlyPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        if (leftCurrentlyPressed && !rightCurrentlyPressed)
        {
            MovePlayer(Direction.Left);
        }
        else if (rightCurrentlyPressed && !leftCurrentlyPressed)
        {
            MovePlayer(Direction.Right);
        }
        else if (rightCurrentlyPressed && leftCurrentlyPressed)
        {
            MovePlayer(lastDirectionPressed);
        }
        else
        {
            moveDirection = Direction.Idle;
        }

        animator.SetInteger(PlayerConstants.MOVEMENT_ANIMATION_VARIABLE, (int)moveDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(BubbleConstants.BUBBLE_TAG))
        {
            audioSource.PlayOneShot(punchClip);
            gameManager.ShowLoseMenu();
        }
    }

    #endregion Events

    private void SetLastDirectionPressed()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            lastDirectionPressed = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            lastDirectionPressed = Direction.Right;
        }
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
}