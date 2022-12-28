using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class PlayerController : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb;
    
    public Transform swordArm;
    public Animator animBody;
    public Animator swordAnim;
    public BoxCollider2D swordCollider;
    public GameObject trailObject;

    public Vector2 moveInput;
    public float horizontal;
    public float vertical;
    public float moveSpeed = 80;

    protected abstract void Movement();
    protected abstract void Sword_Rotation();
    protected abstract void SwordAttack();

}
