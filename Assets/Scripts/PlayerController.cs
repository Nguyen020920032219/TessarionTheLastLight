using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] public int hearth = 5;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private Animator animator;
    private Rigidbody2D rigidbody;
    private bool isGrounded;
    private WeaponManager weaponManager;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponManager.SwitchWeapon();
        }

        if (Input.GetKey(KeyCode.G))
        {
            weaponManager.Attack();
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            weaponManager.StopAttack();
        }
        if(hearth <= 0)
        {
            Dead();
        }
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (animator.GetBool("isAttacking") || animator.GetBool("isShooting"))
        {
            rigidbody.linearVelocity = new Vector2(0, rigidbody.linearVelocity.y);
            return;
        }

        rigidbody.linearVelocity = new Vector2(moveInput * moveSpeed, rigidbody.linearVelocity.y);

        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
        }
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rigidbody.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;

        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }

    //Load again scence
    private void Dead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}