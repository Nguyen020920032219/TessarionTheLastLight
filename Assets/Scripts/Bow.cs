using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    private Animator animator;
    private bool isShooting = false;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void StartShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
        animator.SetBool("isShooting", false);
    }

    private void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            animator.SetBool("isShooting", true);
            Invoke("ResetShooting", 0.2f);

            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

            float direction = transform.lossyScale.x > 0 ? 1 : -1;
            arrow.transform.localScale = new Vector3(direction * 0.02f, 0.01f, 0.01f);
            arrowRb.linearVelocity = new Vector2(direction * 10f, 0);

            nextFireTime = Time.time + fireRate;
        }
    }

    private void ResetShooting()
    {
        animator.SetBool("isShooting", false);
    }
}