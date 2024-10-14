using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    private Vector2 moveInput;
    private Animator animator;
    private Rigidbody2D rb;
    private float originalSpeed; // Store the original speed
    private bool isSlowed = false;

    private bool isMoving = false;
    //public bool isHiding = false;

    public GameObject playerProjectilePrefab; // Assign your projectile prefab in the Inspector
    public float projectileSpeed = 10f;

    public float projectileOffset = 1f;
    void Start()
    {
        originalSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    public void ApplyEnemyFreeze()
    {
        if (!isSlowed)
        {
            Debug.Log("FREEZED");
            isSlowed = true; // Set the slowed state
            moveSpeed = 0f; // Reduce speed by the percentage
            StartCoroutine(RemoveSlowdown());
        }
    }


    public void ApplySlowdown(float percentage)
    {
        if (!isSlowed)
        {
            Debug.Log("SLOWED"); 
            isSlowed = true; // Set the slowed state
            moveSpeed = 1f; // Reduce speed by the percentage
            StartCoroutine(RemoveSlowdown());
        }
    }

    private IEnumerator RemoveSlowdown()
    {
        // Wait for a certain duration (e.g., 2 seconds) before restoring speed
        yield return new WaitForSeconds(2f);

        moveSpeed = originalSpeed; // Restore original speed
        isSlowed = false; // Reset the slowed state
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bush"))
    //    {
    //        // Set hiding animation and change color to gray
    //        Debug.Log("Hit Bush"); 
    //        isHiding = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bush"))
    //    {
    //        // Revert hiding animation and restore original color
    //        isHiding = false;
    //    }
    //}

    void Update()
    {

        // Capture movement input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        if(moveInput.x == 0 && moveInput.y == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving=true;
        }

        // Normalize to prevent faster diagonal movement
        moveInput = moveInput.normalized;
        animator.SetFloat("moveX", moveInput.x);
        animator.SetFloat("moveY", moveInput.y);
        animator.SetBool("isMoving", isMoving);


        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in the world
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate the direction from the player to the mouse position
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            // Instantiate the projectile at the player's position
            Vector2 spawnPosition = (Vector2)transform.position + direction * projectileOffset;
            GameObject projectile = Instantiate(playerProjectilePrefab, spawnPosition, Quaternion.identity);


            // Set the projectile's rotation to face the mouse position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Add a Rigidbody2D component to the projectile for movement
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {

                rb.velocity = direction * projectileSpeed; // Set the velocity of the projectile
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement based on input
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
