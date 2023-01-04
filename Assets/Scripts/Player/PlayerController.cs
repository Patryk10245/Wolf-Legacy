using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    Player player;
    [HideInInspector] public Rigidbody2D rb;
    Animator animBody;
    
    public Transform weaponHolder;
    public Animator weaponAnimator;
    public BoxCollider2D weaponCollider;
    public GameObject trailObject;

    PlayerInput playerInput;
    InputActionAsset inputAsset;
    InputActionMap playerMap;
    InputAction move;
    InputAction rotate;
    InputAction ability0;

    public Vector2 moveInput;
    public float horizontal;
    public float vertical;
    public float moveSpeed = 320;

    [Header("Debug")]
    public Vector2 dMousePos;
    public Vector2 dScreenPoint;

    private void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animBody = GetComponent<Animator>();

        playerInput = GetComponent<PlayerInput>();
        inputAsset = GetComponent<PlayerInput>().actions;
        playerMap = inputAsset.FindActionMap("Player");
        playerMap.FindAction("Attack").started += Attack;
        move = playerMap.FindAction("Move");
        rotate = playerMap.FindAction("Rotate");
        playerMap.FindAction("Ability0").started += BasicAbility;
        playerMap.Enable();

        

        Debug.Log("name = " + gameObject.name + " | device = " + playerMap.devices.Value[0].name);
    }

    private void Update()
    {
        Sword_Rotation();
    }
    private void FixedUpdate()
    {
        if (!player.canMove)
        {
            return;
        }

        Movement();
    }

    void Movement()
    {
        moveInput = move.ReadValue<Vector2>().normalized;
        //moveInput = playerMap..Move.ReadValue<Vector2>().normalized;
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            rb.AddForce(moveInput * moveSpeed);
            animBody.SetBool("isMoving", true);
        }
        else
        {
            animBody.SetBool("isMoving", false);
        }
        
    }
    void Sword_Rotation()
    {
        Vector2 mousePos;
        mousePos = rotate.ReadValue<Vector2>();
        Vector3 screenPoint = player.currentCamera.WorldToScreenPoint(transform.localPosition);
        dMousePos = mousePos;


        if(playerInput.currentControlScheme == "GamePad")
        {
            screenPoint = Vector3.zero;
        }
        dScreenPoint = screenPoint;
        /* ROTATION TOWARDS WALK DIRECTION
        if (mousePos == Vector2.zero)
        {
            mousePos.x += moveInput.x;
            mousePos.y += moveInput.y;
        }
        */

        if (mousePos == Vector2.zero)
        {
            return;
        }


        if (mousePos.x < screenPoint.x) // Turns Left
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponHolder.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if (mousePos.x > screenPoint.x) // Turns Right
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(1f, 1f, 1f);
            weaponHolder.localScale = Vector3.one;
        }

        //mousePos.Normalize();

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        weaponHolder.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            player.attackScript.Attack();
        }
    }
    void BasicAbility(InputAction.CallbackContext context)
    {
        //Debug.Log("player = " + player);
        //Debug.Log("ability = " + player.abilityBasic);
        player.abilityBasic.Use();
    }

}
