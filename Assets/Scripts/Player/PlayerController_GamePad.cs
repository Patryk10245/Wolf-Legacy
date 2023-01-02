using UnityEngine;

public class PlayerController_GamePad : PlayerController
{
    public Vector3 mousePos;

    [Space(20)]
    [Header("DEBUG")]
    public float horizontal_mouse;
    public float vertical_mouse;
    public Vector2 offset;
    public float angle;

    protected override void Movement()
    {
        // Return if player cant move
        if (!player.canMove)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal J1");
        vertical = Input.GetAxisRaw("Vertical J1");

        moveInput.x = horizontal;
        moveInput.y = vertical;

        if (horizontal != 0 || vertical != 0)
        {
            rb.AddForce(moveInput * moveSpeed);
            animBody.SetBool("isMoving", true);
        }
        else
        {
            animBody.SetBool("isMoving", false);
        }
    }
    protected override void Sword_Rotation()
    {

        //float horizontal_mouse;
        //float vertical_mouse;


        horizontal_mouse = Input.GetAxisRaw("Horizontal2 J1");
        vertical_mouse = Input.GetAxisRaw("Vertical2 J1");

        mousePos = Vector3.zero;
        mousePos.x += horizontal_mouse;
        mousePos.y += vertical_mouse;

        if(mousePos == Vector3.zero)
        {
            mousePos.x += horizontal;
            mousePos.y += vertical;
        }

        if (mousePos == Vector3.zero)
        {
            return;
        }

        //Vector3 screenPoint = swordArm.localPosition;
        Vector3 screenPoint = Vector3.zero;

        if (mousePos.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            swordArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else if(mousePos.x > 0)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(1f, 1f, 1f);
            swordArm.localScale = Vector3.one;
        }

        //Vector2 offset = new Vector2(horizontal_mouse - screenPoint.x, vertical_mouse - screenPoint.y);
        //float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        /*
        // Fixes the issue with broken joysticks where they stick slightly to one side
        if ((mousePos.x > -0.1f && mousePos.x < 0.1f) && (mousePos.y > -0.1f && mousePos.y < 0.1f))
        {
            angle = 0;
        }
        */

        swordArm.rotation = Quaternion.Euler(0, 0, angle);

    }
    protected override void SwordAttack()
    {
        if (Input.GetButtonDown("Fire J1"))
        {
            player.inAttack = true;
            swordAnim.SetTrigger("isClicked");
            swordCollider.enabled = true;
            trailObject.SetActive(true);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        animBody = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sword_Rotation();
        SwordAttack();
        Ability1Use();
    }

    void Ability1Use()
    {
        if(Input.GetButtonDown("Dash J1"))
        {
            player.abilityBasic.Use();
        }
    }
}
