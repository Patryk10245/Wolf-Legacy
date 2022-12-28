using UnityEngine;

public class Player_Controller_GamePad : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb;

    public Transform swordArm;
    public Animator anim;
    public Animator swordAnim;
    public BoxCollider2D swordCollider;
    public GameObject trailObject;

    public Vector2 moveInput;
    public float horizontal;
    public float vertical;
    [Space(15)]
    public Vector3 mousePos;
    public float horizontal_mouse;
    public float vertical_mouse;
    [Space(15)]

    public Vector2 offset;
    public float angle;

    public float moveSpeed = 80;
    void Movement()
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
    void Sword_Rotation()
    {

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
        
        offset = new Vector2(horizontal_mouse - screenPoint.x , screenPoint.y - vertical_mouse);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        // Fixes the issue with broken joysticks where they stick slightly to one side
        if((mousePos.x > -0.1f && mousePos.x < 0.1f) && (mousePos.y > -0.1f && mousePos.y < 0.1f))
        {
            angle = 0;
        }

        swordArm.rotation = Quaternion.Euler(0, 0, angle);

    }
    void SwordAttack()
    {
        if (Input.GetButtonDown("Fire J1"))
        {
            player.inAttack = true;
            swordAnim.SetTrigger("isClicked");
            swordCollider.enabled = true;
            trailObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Sword_Rotation();
        SwordAttack();
    }
}
