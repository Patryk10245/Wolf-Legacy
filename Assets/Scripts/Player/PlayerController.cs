using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
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

    public Vector2 moveInput;
    public float horizontal;
    public float vertical;
    public float moveSpeed = 320;


    public Vector3 screenPoint;
    public Vector2 mousePos;

    [Header("Debug")]
    public Vector2 dMousePos;
    public Vector2 dScreenPoint;

    private void Start()
    {
        //Debug.Log("Player controller Start = " + gameObject.name);

        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animBody = GetComponent<Animator>();

        playerInput = GetComponent<PlayerInput>();
        inputAsset = GetComponent<PlayerInput>().actions;
        playerMap = inputAsset.FindActionMap("Player");
        move = playerMap.FindAction("Move");
        rotate = playerMap.FindAction("Rotate");
        playerMap.FindAction("Attack").started += Attack;
        playerMap.FindAction("Ability0").started += BasicAbility;
        playerMap.FindAction("Ability1").started += SecondaryAbility;
        playerMap.FindAction("Pause").started += PauseGame;
        playerMap.Enable();


        transform.position = Level_FightReferenecs.ins.playerManager.playerSpawnPosition.transform.position;

        //Debug.Log("name = " + gameObject.name + " | device = " + playerMap.devices.Value[0].name);
    }

    private void Update()
    {
        //Debug.Log("Player pos = " + gameObject.transform.position);
        if (Game_State.gamePaused)
            return;
        if(player.isDead)
        {
            return;
        }

        Sword_Rotation();
    }
    private void FixedUpdate()
    {
        if (!player.canMove)
        {
            return;
        }
        if (player.isDead)
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
        
        if(player.canRotateWeapon == false)
        {
            return;
        }

        mousePos = rotate.ReadValue<Vector2>();
        screenPoint = player.currentCamera.WorldToScreenPoint(transform.localPosition);
        dMousePos = mousePos;


        if(playerInput.currentControlScheme == "GamePad")
        {
            screenPoint = Vector3.zero;
        }
        dScreenPoint = screenPoint;

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
            //Debug.Log("player = " + player.id);
            player.attackScript.Attack();
        }
    }
    void BasicAbility(InputAction.CallbackContext context)
    {
        //Debug.Log("player = " + player);
        //Debug.Log("ability = " + player.abilityBasic);
        player.abilityBasic.Use();
    }
    void SecondaryAbility(InputAction.CallbackContext context)
    {
        player.abilitySecondary.Use();
    }


    public void RemoveListeningOnEvents()
    {
        playerMap.FindAction("Attack").started -= Attack;
        playerMap.FindAction("Ability0").started -= BasicAbility;
        playerMap.FindAction("Ability1").started -= SecondaryAbility;
        playerMap.FindAction("Pause").started -= PauseGame;
    }
    public void PauseGame(InputAction.CallbackContext context)
    {
        Game_State.PauseGame();
        Debug.Log("current state = " + Game_State.gamePaused);
    }
}
