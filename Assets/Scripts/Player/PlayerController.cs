using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject gameOverUi;

    private Animator animator;
    private bool isGrounded;
    private Rigidbody2D rb;
    private WeaponManager weaponManager;
    private PlayerManager playerManager;
    private bool isGameOver = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        gameOverUi.SetActive(false);
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

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

        if (Input.GetKeyDown(KeyCode.W))
        {
            weaponManager.UsingSkill();
        }
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * playerManager.GetSpeed(), rb.linearVelocity.y);
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = Vector2.zero;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }

    public void Die()
    {
        isGameOver = true;
        animator.SetBool("isDying", true);
        Invoke(nameof(GameOver), 1.5f);
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }

    public void RestartGame()
    {
        isGameOver = false;
        playerManager.ResetPlayerStats();
        Time.timeScale = 1;
        SceneManager.LoadScene("Setup");
    }
}