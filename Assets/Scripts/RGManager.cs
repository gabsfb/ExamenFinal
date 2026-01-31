using UnityEngine;
using System.Collections;

public class RGManager : MonoBehaviour
{
    [Header("Configuración Salto")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody mainRigidbody;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.2f;

    [Header("Configuración Ragdoll")]
    [SerializeField] private Animator animator;
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;
    private bool isRagdoll = false;
    private Collider mainCollider;

    [Header("Configuración Slide")]
    [SerializeField] private float slideHeight = 0.5f;
    [SerializeField] private float slideCenterY = 0.25f;
    [SerializeField] private float slideDuration = 1.0f;
    private float originalHeight;
    private Vector3 originalCenter;
    private bool isSlidingActive = false;

    void Start()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        mainCollider = GetComponent<Collider>();
        if (mainCollider is CapsuleCollider capsule)
        {
            originalHeight = capsule.height;
            originalCenter = capsule.center;
        }

        if (mainRigidbody == null) mainRigidbody = GetComponent<Rigidbody>();

        SetRagdollState(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SetRagdollState(true);
        if (Input.GetKeyDown(KeyCode.T)) SetRagdollState(false);

        isJumping();
        isSliding();
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance, groundLayer);
    }

    public void isJumping()
    {
        bool grounded = IsGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && !isRagdoll && grounded)
        {
            animator.SetBool("isJumping", true);
            if (mainRigidbody != null)
            {
                mainRigidbody.linearVelocity = new Vector3(mainRigidbody.linearVelocity.x, 0, mainRigidbody.linearVelocity.z);
                mainRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        animator.SetBool("isGrounded", grounded);

        if (Input.GetKeyUp(KeyCode.Space) || !grounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    public void isSliding()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isRagdoll && !isSlidingActive)
        {
            StartCoroutine(SlideRoutine());
        }
    }

    private IEnumerator SlideRoutine()
    {
        isSlidingActive = true;
        animator.SetBool("isSliding", true);

        if (mainCollider is CapsuleCollider capsule)
        {
            capsule.height = slideHeight;
            capsule.center = new Vector3(originalCenter.x, slideCenterY, originalCenter.z);
        }

        yield return new WaitForSeconds(slideDuration);

        if (mainCollider is CapsuleCollider capsuleRestore)
        {
            capsuleRestore.height = originalHeight;
            capsuleRestore.center = originalCenter;
        }

        animator.SetBool("isSliding", false);
        isSlidingActive = false;
    }

    public void SetRagdollState(bool state)
    {
        isRagdoll = state;
        animator.enabled = !state;

        if (state)
        {
            StopAllCoroutines();
            isSlidingActive = false;
        }

        if (mainRigidbody != null) mainRigidbody.isKinematic = state;

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            if (rb == mainRigidbody) continue;
            rb.isKinematic = !state;
        }

        foreach (Collider col in ragdollColliders)
        {
            if (col == mainCollider) continue;
            col.enabled = state;
        }

        if (mainCollider != null) mainCollider.enabled = !state;
    }

}