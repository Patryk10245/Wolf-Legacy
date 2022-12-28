using UnityEngine;

public class PlayerController_KeyboardMouse : PlayerController
{
    protected override void Movement()
    {
        // Return if player cant move
        if (!player.canMove)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveInput.x = horizontal;
        moveInput.y = vertical;

        if (horizontal != 0 || vertical != 0)
        {
            rb.AddForce(moveInput * moveSpeed);
        }
    }
    protected override void Sword_Rotation()
    {
        if (player.inAttack == false)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = player.theCam.WorldToScreenPoint(transform.localPosition);
            if (mousePos.x < screenPoint.x)
            {
                player.transform.localScale = new Vector3(-1f, 1f, 1f);
                swordArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                player.transform.localScale = new Vector3(1f, 1f, 1f);
                swordArm.localScale = Vector3.one;

            }
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            swordArm.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    protected override void SwordAttack()
    {
        if (Input.GetMouseButtonDown(0))
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
    private void Update()
    {
        Movement();
        Sword_Rotation();
        SwordAttack();
    }
}


