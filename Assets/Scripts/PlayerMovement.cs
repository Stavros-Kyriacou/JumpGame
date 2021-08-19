using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float yForce = 0f;
    [SerializeField] private float rayDistance = 0f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private int maxJumps = 1;
    [SerializeField] private ParticleSystem jumpParticles;
    [SerializeField] private int currentJumps;
    private bool isGrounded;
    private RaycastHit2D hit;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentJumps = maxJumps;
    }
    private void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        CheckGrounded();
    }

    private void GetInput()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        if (dirX != 0f)
        {
            Vector2 deltaX = new Vector2(dirX * Time.deltaTime * speed, 0f);
            rb.AddForce(deltaX);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(dirX);
        }
    }
    private void CheckGrounded()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, groundLayerMask);
        if (hit.collider != null)
        {
            // Debug.Log(hit.collider.name);
            isGrounded = true;
            currentJumps = maxJumps;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void Jump(float dirX)
    {
        if (currentJumps > 0)
        {
            rb.AddForce(new Vector2(dirX, yForce));
            currentJumps--;
            Instantiate(jumpParticles, transform.position, transform.rotation);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 dir = Vector2.down * rayDistance;
        Gizmos.DrawRay(transform.position, dir);
    }


}
