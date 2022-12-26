using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Player player;
    public Vector2 moveInput;
    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    [SerializeField] float moveSpeed = 20;
    public Rigidbody2D rb;

    [SerializeField] Camera theCam;
    [SerializeField] Transform swordArm;
    public bool inAttack;
    [SerializeField] Animator anim;
    public bool can_Input = true;


// A co powiesz na ten komentarz 
    void Update()
    {
        Movement();

        Sword_Rotation();
        

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
            }
        }
        
    }

    void Sword_Rotation()
    {

        theCam.transform.position = new Vector3(transform.position.x, transform.position.y, -8f);

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

        if (inAttack == false)
        {
            swordArm.rotation = Quaternion.Euler(0, 0, angle);
        }

        if(Input.GetMouseButtonDown(0))
        {
            inAttack = true;
            anim.SetTrigger("isClicked");
        }
    }
    public void EVENT_AttackAnimationEnd()
    {
        inAttack = false;
    }
}
