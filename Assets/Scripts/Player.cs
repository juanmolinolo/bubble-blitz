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

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 currentPosition = transform.position;
            currentPosition.x += PlayerConstants.MOVEMENT_SPEED * Time.deltaTime;
            transform.position = currentPosition;
            moveDirection = Direction.Right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 currentPosition = transform.position;
            currentPosition.x -= PlayerConstants.MOVEMENT_SPEED * Time.deltaTime;
            transform.position = currentPosition;
            moveDirection = Direction.Left;
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
}