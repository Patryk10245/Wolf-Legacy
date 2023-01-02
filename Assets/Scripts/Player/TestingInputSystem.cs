using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TestingInputSystem : MonoBehaviour
{
    [SerializeField] Vector2 moveInput;
    [SerializeField] float moveSpeed = 3f;

    Player player;
    Rigidbody2D rb;
    Animator animBody;
    PlayerInput playerInput;
    Player_Controller playerInputActions;

    [SerializeField] Animator swordAnimator;
    [SerializeField] BoxCollider2D swordCollider;
    [SerializeField] GameObject trailObject;
    [SerializeField] Transform paladinSwordHolder;

    [Header("Debug")]
    public Vector2 dMousePos;

    private void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animBody = GetComponent<Animator>();

        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new Player_Controller();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Attack.performed += Attack;
    }

    private void FixedUpdate()
    {
        // Movement
        if (!player.canMove)
        {
            return;
        }

        moveInput = playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
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
    private void Update()
    {
        SwordRotation();
    }


    // Start is called before the first frame update
    public void Attack(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            player.inAttack = true;
            swordAnimator.SetTrigger("isClicked");
            swordCollider.enabled = true;
            trailObject.SetActive(true);
        }

    }

    void SwordRotation()
    {
        Vector2 mousePos;
        mousePos = playerInputActions.Player.Rotate.ReadValue<Vector2>().normalized;
        //Debug.Log("mousepos = " + mousePos);
        dMousePos = mousePos;
        

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

        //Vector3 screenPoint = swordArm.localPosition;
        Vector3 screenPoint = Vector3.zero;

        if (mousePos.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            paladinSwordHolder.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if (mousePos.x > 0)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(1f, 1f, 1f);
            paladinSwordHolder.localScale = Vector3.one;
        }

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        //offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        //angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        /*
        // Fixes the issue with broken joysticks where they stick slightly to one side
        if ((mousePos.x > -0.1f && mousePos.x < 0.1f) && (mousePos.y > -0.1f && mousePos.y < 0.1f))
        {
            angle = 0;
        }
        */

        paladinSwordHolder.rotation = Quaternion.Euler(0, 0, angle);

    }
    public void Rotate(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }
}