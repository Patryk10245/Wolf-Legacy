using UnityEngine;

public class Player_Controller_GamePad : PlayerController
{
    protected override void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal J1");
        vertical = Input.GetAxisRaw("Vertical J1");

        moveInput.x = horizontal;
        moveInput.y = vertical;

        if (horizontal != 0 || vertical != 0)
        {
            rb.AddForce(moveInput * moveSpeed);
        }
    }
    protected override void Sword_Rotation()
    {
        Vector3 mousePos;
        float horizontal_mouse;
        float vertical_mouse;

        horizontal_mouse = Input.GetAxisRaw("Horizontal2 J1");
        vertical_mouse = Input.GetAxisRaw("Vertical2 J1");

        mousePos = swordArm.localPosition;
        mousePos.x += horizontal_mouse;
        mousePos.y += vertical_mouse;

        Vector3 screenPoint = swordArm.localPosition;

        if (mousePos.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            swordArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(1f, 1f, 1f);
            swordArm.localScale = Vector3.one;
        }

        Vector2 offset = new Vector2(horizontal_mouse - screenPoint.x, screenPoint.y - vertical_mouse);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        // Fixes the issue with broken joysticks where they stick slightly to one side
        if ((mousePos.x > -0.1f && mousePos.x < 0.1f) && (mousePos.y > -0.1f && mousePos.y < 0.1f))
        {
            angle = 0;
        }

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
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sword_Rotation();
        SwordAttack();
    }
}
