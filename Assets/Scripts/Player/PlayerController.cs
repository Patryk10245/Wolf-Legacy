using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb;
    
    public Transform swordArm;
    public Animator anim;
    public BoxCollider2D swordCollider;
    public GameObject trailObject;

    public Vector2 moveInput;
    public float horizontal;
    public float vertical;
    public float moveSpeed = 20;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if(player.prohibitAllActions)
        {
            return;
        }


        Movement();
        Sword_Rotation();
        SwordAttack();
    }
    void Movement()
    {
        // Return if player cant move
        if(!player.canMove)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveInput.x = horizontal;
        moveInput.y = vertical;

        if (horizontal != 0 || vertical != 0)
        {
            rb.AddForce(moveInput* moveSpeed);
        }
    }
    void Sword_Rotation()
    {
        if(player.inAttack == false)
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
    void SwordAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.inAttack = true;
            anim.SetTrigger("Attack");
            swordCollider.enabled = true;
            trailObject.SetActive(true);
        }
    }
}
