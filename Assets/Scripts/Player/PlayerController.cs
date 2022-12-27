using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referencje
    Player player;
    [HideInInspector]public Rigidbody2D rb;
    [HideInInspector]public PlayerHitCollider hit_collider;
    [SerializeField] Camera theCam;
    [SerializeField] Transform swordArm;
    [SerializeField] Animator swordArmAnimator;
    [SerializeField] Animator animBody;
    public TrailRenderer trail_renderer;

    float horizontal;
    float vertical;
    [SerializeField] float moveSpeed = 20;

    [HideInInspector]public bool inAttack;
    [HideInInspector]public Vector2 moveInput;
    [HideInInspector]public bool can_Input = true;

    


    private void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        hit_collider = GetComponentInChildren<PlayerHitCollider>();
    }
 
    void Update()
    {
        Movement();
        Sword_Rotation();
        SwordAttack();
        

    }
    void Movement()
    {
        if(can_Input)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(horizontal, vertical);
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
        
    }

    void Sword_Rotation()
    {

        theCam.transform.position = new Vector3(transform.position.x, transform.position.y, -8f);

        if (inAttack == false)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);
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
        if (Input.GetMouseButton(0) && inAttack == false)
        {
            inAttack = true;
            swordArmAnimator.SetTrigger("isClicked");
            trail_renderer.enabled = true;
            hit_collider.player_hit_collider.enabled = true;
        }
    }

}
