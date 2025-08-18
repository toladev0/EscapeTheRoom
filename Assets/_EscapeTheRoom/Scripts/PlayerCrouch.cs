using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerCrouch : MonoBehaviour
{
    // Height of the CharacterController when standing
    public float standingHeight = 2.0f;
    // Height of the CharacterController when crouching
    public float crouchingHeight = 1.0f;
    // Movement speed while crouching
    public float crouchSpeed = 2.0f;
    // Movement speed while standing
    public float standingSpeed = 5.0f;

    // Reference to the CharacterController component
    private CharacterController characterController;
    // Reference to the FirstPersonController (assumed to handle movement)
    private StarterAssets.FirstPersonController firstPersonController;
    // Original center of the CharacterController capsule (used to reset after crouch)
    private Vector3 originalCenter;
    // Flag to track if player is currently crouching
    private bool isCrouching = false;

    private void Start()
    {
        // Cache the CharacterController component
        characterController = GetComponent<CharacterController>();
        // Cache the FirstPersonController component
        firstPersonController = GetComponent<StarterAssets.FirstPersonController>();
        // Save the original center of the capsule collider so we can restore it later
        originalCenter = characterController.center;
    }

    private void FixedUpdate()
    {
        // Check if Left Ctrl key is held down using the new Input System
        if (Keyboard.current.leftCtrlKey.isPressed)
        {
            // If not already crouching, start crouch
            if (!isCrouching)
                Crouch();
        }
        else
        {
            // If Ctrl not pressed and player is crouching, stand up
            if (isCrouching)
                StandUp();
        }
    }

    void Crouch()
    {
        // Instantly set the CharacterController height to crouching height
        characterController.height = crouchingHeight;

        // Optional: you had code to adjust the center to keep the collider aligned, but it's commented out
        // This would move the collider center proportionally lower
        //characterController.center = new Vector3(
        //    originalCenter.x,
        //    crouchingHeight * (originalCenter.y / standingHeight),
        //    originalCenter.z
        //);

        // If FirstPersonController exists, reduce movement speed for crouch
        if (firstPersonController != null)
        {
            firstPersonController.MoveSpeed = crouchSpeed;
        }

        // Set crouching flag
        isCrouching = true;
    }

    void StandUp()
    {
        // Reset CharacterController height back to standing height
        characterController.height = standingHeight;
        // Reset the center of the CharacterController capsule to original
        characterController.center = originalCenter;

        // Restore movement speed to standing speed
        if (firstPersonController != null)
        {
            firstPersonController.MoveSpeed = standingSpeed;
        }

        // Clear crouching flag
        isCrouching = false;
    }
}
