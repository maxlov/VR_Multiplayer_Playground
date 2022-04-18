using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerControllerVertical : MonoBehaviour
{
    private CharacterController characterController;

    private float currentYVelocity = 0;
    private float appliedYVelocity = 0;

    private float gravity;
    private float groundedGravity = -.5f;

    private float initialJumpVelocity;
    [SerializeField] private FloatVariable jumpHeight;
    [SerializeField] private FloatVariable jumpTime;
    [SerializeField] private FloatVariable fallMultiplier;

    private bool isJumping = false;

    [SerializeField] private InputActionReference jumpActionReference;

    [SerializeField] private UnityEvent OnJump;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        SetupJumpVariables();
    }

    public void SetupJumpVariables()
    {
        float timeToApex = jumpTime.Value / 2;
        gravity = (-2 * jumpHeight.Value / Mathf.Pow(timeToApex, 2));
        initialJumpVelocity = (2 * jumpHeight.Value) / timeToApex;
    }


    void Update()
    {
        characterController.Move(new Vector3(0, appliedYVelocity, 0) * Time.deltaTime);

        HandleGravity();
        HandleJump();
    }

    private void HandleGravity()
    {
        bool isFalling = currentYVelocity <= 0.0f || !jumpActionReference.action.IsPressed();

        if (!isJumping && characterController.isGrounded)
        {
            currentYVelocity = appliedYVelocity = groundedGravity;
            return;
        }

        float previousYVelocity = currentYVelocity;

        if (isFalling)
        {
            currentYVelocity += (gravity * fallMultiplier.Value * Time.deltaTime);
            appliedYVelocity = Mathf.Max((previousYVelocity + currentYVelocity) * .5f, -20f);

            // reset player position when player has fallen
            if (transform.position.y < -75) { transform.position = new Vector3(0, 75, 0); }
        }
        else
        {
            currentYVelocity += (gravity * Time.deltaTime);
            appliedYVelocity = (previousYVelocity + currentYVelocity) * .5f;
        }
    }

    private void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && jumpActionReference.action.IsPressed())
        {
            isJumping = true;
            OnJump.Invoke();
            currentYVelocity = appliedYVelocity = initialJumpVelocity;
        } else if (!jumpActionReference.action.IsPressed() && isJumping && characterController.isGrounded)
        {
            isJumping = false;
            return;
        }
    }
}
