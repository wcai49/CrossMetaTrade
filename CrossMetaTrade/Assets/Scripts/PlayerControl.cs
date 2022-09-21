using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    // player movement
    public float moveSpeed = 5f;
    public Vector2 moveInput = new Vector2(0f, 0f);
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    float turnSmoothVelocity;
    public bool isBackpackOpen = true;
    public bool isSelling = false;
    Vector3 velocity;

    // sign components
    public CharacterController characterController;
    public Transform cam;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public GameObject NFT_Prefab;

    bool isGrounded;
    Animator playerAnimator;
    PlayerControlAction playerControlAction;
    AudioSource footStepSound;
    GameObject backpackCanvas;
    GameObject sellingObject;

    PhotonView view;
    private void Awake()
    {
        playerControlAction = new PlayerControlAction();
        // bind WASD movement
        playerControlAction.Player.Move.performed += moveValue => moveInput = moveValue.ReadValue<Vector2>();
        playerControlAction.Player.Move.canceled += moveValue => moveInput = moveValue.ReadValue<Vector2>();

        cam = Camera.main.transform;
        view = GetComponent<PhotonView>();
        backpackCanvas = GameObject.Find("BackpackCanvas");
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
        playerAnimator = GetComponent<Animator>();
        footStepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (moveInput.magnitude != 0)
            {
                if (isSelling)
                {
                    StopSell();
                }
                playerAnimator.SetBool("isWalking", true);
                if (!footStepSound.isPlaying)
                {
                    footStepSound.Play();
                }
                Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

                // Apply gravity to player characters.
                velocity.y += gravity * Time.deltaTime;
                characterController.Move(velocity * Time.deltaTime);
            }
            else
            {
                playerAnimator.SetBool("isWalking", false);
                footStepSound.Stop();
            }

            if (playerControlAction.UI.Backpack.triggered)
            {
                toggleBackpack();
            }
        }

    }
    public void toggleBackpack()
    {
        isBackpackOpen = !isBackpackOpen;
        backpackCanvas.SetActive(isBackpackOpen);

        Cursor.visible = isBackpackOpen;
    }

    public void StartSell()
    {
        playerAnimator.SetBool("isSelling", true);
        isSelling = true;

        sellingObject = PhotonNetwork.Instantiate(NFT_Prefab.name, new Vector3(transform.position.x, 0.42f, transform.position.z + 0.5f), Quaternion.identity);
        sellingObject.transform.eulerAngles = new Vector3(-130f, 0f, 0f);
    }

    public void StopSell()
    {
        playerAnimator.SetBool("isSelling", false);
        PhotonNetwork.Destroy(sellingObject);
        isSelling = false;
    }
}
