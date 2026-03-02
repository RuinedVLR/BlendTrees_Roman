using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 moveInput;

    private Rigidbody2D playerRb;

    Animator animController;

    private int xPosHash = Animator.StringToHash("Xpos");
    private int yPosHash = Animator.StringToHash("Ypos");

    private int moveInputXHash = Animator.StringToHash("MoveInputX");
    private int moveInputYHash = Animator.StringToHash("MoveInputY");
    private int isMovingHash = Animator.StringToHash("IsMoving");

    private int lastMoveXHash = Animator.StringToHash("LastMoveX");
    private int lastMoveYHash = Animator.StringToHash("LastMoveY");

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null) Debug.LogError("Rigidbody2D component not found on the player object.");

        animController = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        HandlePlayerAnimations();
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void LateUpdate()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void HandlePlayerMovement()
    {
        playerRb.MovePosition(playerRb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandlePlayerAnimations()
    {
        if (moveInput != Vector2.zero)
        {
            animController.SetFloat(lastMoveXHash, animController.GetFloat(moveInputXHash));
            animController.SetFloat(lastMoveYHash, animController.GetFloat(moveInputYHash));
            
            animController.SetFloat(moveInputXHash, moveInput.x);
            animController.SetFloat(moveInputYHash, moveInput.y);

            animController.SetBool(isMovingHash, true);
        }
        else
        {
            animController.SetFloat(moveInputXHash, moveInput.x);
            animController.SetFloat(moveInputYHash, moveInput.y);

            animController.SetBool(isMovingHash, false);
        }
    }



}
















