using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.VFX;

public class PlayerControl : MonoBehaviour
{
    // player movement
    public float moveSpeed = 5f;
    public Vector2 moveInput = new Vector2(0f, 0f);
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f;
    float turnSmoothVelocity;
    public bool isBackpackOpen = false;
    public bool isSelling = false;
    public VisualEffect levelup;
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
    public GameObject subscribeCanvas;
    public GameObject backpackCanvas;

    public Button phone_e_button;



    bool isGrounded;
    Animator playerAnimator;
    PlayerControlAction playerControlAction;
    AudioSource footStepSound;
    GameObject sellingObject;
    GameObject inter_target;
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

#if UNITY_ANDROID || UNITY_IOS
        phone_e_button = GameObject.Find("E").GetComponent<Button>();
#endif
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
#if UNITYI_ANDROID || UNITY_IOS
            if (inter_target != null)
            {
                phone_e_button.interactable = true;
            }
            else{
                phone_e_button.interactable = false;
            }
#else
            if (inter_target != null && playerControlAction.UI.Interaction.triggered)
            {
                startBuying();
            }
#endif
        }

    }
    public void toggleBackpack()
    {
        isBackpackOpen = !isBackpackOpen;
        backpackCanvas.SetActive(isBackpackOpen);

        Cursor.visible = isBackpackOpen;
    }

    public void StartSell(Button btn)

    {
        if (isSelling)
        {
            return;
        }

        sellBtn = btn;
        sellBtn.interactable = false;
        sellingObject = PhotonNetwork.Instantiate(NFT_Prefab.name, handPosition.position, Quaternion.identity);
        float rotateAngle = Vector3.Angle(sellingObject.transform.forward, transform.forward);
        sellingObject.transform.eulerAngles = new Vector3(30f, transform.eulerAngles.y, 0);
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


    // called once per frame for every Collider or Rigidbody that touches another Collider or Rigidbody.
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 3)
        {
            return;
        }

        if (collision.collider.tag == "Player")
        {
            inter_target = collision.gameObject;
        }
    }

    // called when this collider/rigidbody has stopped touching another rigidbody/collider.
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 3)
        {
            return;
        }
        if (inter_target != null && inter_target == collision.gameObject)
        {
            inter_target = null;
            GameObject.Find("GameManager").GetComponent<GameManagerSystem>().HideTradingCanvas();
        }
        Debug.Log("Left");
    }

    public void setSubscriptionCanvas(bool visibility, bool cursor)
    {
        subscribeCanvas.SetActive(visibility);
        Cursor.visible = cursor;
    }

    public void Levelup()
    {
        levelup.Play();
    }

    public void startBuying()
    {
        GameObject.Find("GameManager").GetComponent<GameManagerSystem>().ShowTradingCanvas();
    }
}
