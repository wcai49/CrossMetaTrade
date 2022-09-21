using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Camera userCamera;
    public Transform cam;
    public Transform groundCheck;
    public Transform handPosition;
    public float groundDistance = 0.4f;

    public LayerMask groundMask;
    public GameObject NFT_Prefab;


    bool isGrounded;
    Animator playerAnimator;
    PlayerControlAction playerControlAction;
    AudioSource footStepSound;
    GameObject backpackCanvas;
    GameObject sellingObject;
    Button sellBtn;

    PhotonView view;
    private void Awake()
    {
        playerControlAction = new PlayerControlAction();
        // bind WASD movement
        playerControlAction.Player.Move.performed += moveValue => moveInput = moveValue.ReadValue<Vector2>();
        playerControlAction.Player.Move.canceled += moveValue => moveInput = moveValue.ReadValue<Vector2>();

        cam = Camera.main.transform;
        userCamera = Camera.main;
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
        if (isSelling)
        {
            return;
        }

        sellBtn = btn;
        sellBtn.interactable = false;
        sellingObject = PhotonNetwork.Instantiate(NFT_Prefab.name, handPosition.position, Quaternion.identity);
        float rotateAngle = Vector3.Angle(sellingObject.transform.forward, transform.forward);
        sellingObject.transform.Rotate(new Vector3(30f, rotateAngle, 0f));
        playerAnimator.SetBool("isSelling", true);
        isSelling = true;


    }

    public void StopSell()
    {
        if (!isSelling)
        {
            return;
        }

        sellBtn.interactable = true;
        sellBtn = null;
        playerAnimator.SetBool("isSelling", false);
        if (sellingObject != null)
        {
            PhotonNetwork.Destroy(sellingObject);
        }
        isSelling = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerControl>().isSelling)
        {
            Debug.Log("Start");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Left");
    }
}
