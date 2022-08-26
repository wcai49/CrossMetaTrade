using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // player movement
    public float moveSpeed = 5f;
    public Vector2 moveInput = new Vector2(0f, 0f);
            
    // camera control
    
    [SerializeField] float sensitivity = 8.0f;
    
    // sign components
    public CharacterController characterController;
    Animator playerAnimator;
    PlayerControlAction playerControlAction;
    Camera playerCamera;
   
    private void Awake()
    {
        playerControlAction = new PlayerControlAction();
        // bind WASD movement
        playerControlAction.Player.Move.performed += moveValue => moveInput = moveValue.ReadValue<Vector2>();
        playerControlAction.Player.Move.canceled += moveValue => moveInput = moveValue.ReadValue<Vector2>();
    }

    #region - Enable / Disable -
    private void OnEnable()
    {
        playerControlAction.Enable();
    }

    private void OnDisable()
    {
        playerControlAction.Disable();
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // assign gameobjects
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveInput.magnitude != 0)
        {
            playerAnimator.SetBool("isWalking", true);
            // player is moving backwards
            if (moveInput.y < 0)
            {
                float backwardSlow = 0.3f;
                characterController.Move(-transform.forward * moveSpeed * backwardSlow * Time.deltaTime);

            }
            // player strafe left or right
            else if (moveInput.x != 0)
            {
                if (moveInput.x < 0)
                {
                    characterController.Move(-transform.right * moveSpeed  * Time.deltaTime);
                }
                else
                {
                    characterController.Move(transform.right * moveSpeed * Time.deltaTime);
                }

            }
            // player move forward
            else
            {
                characterController.Move(transform.forward * moveSpeed  * Time.deltaTime);
            }
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }    
}
