using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Tooltip("Maximum slope")]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;
    [Tooltip("Move speed m/s")]
    public float moveSpeed = 2f;
    [Tooltip("Turn speed deg/s, left +, right -")]
    public float turnSpeed = 300;
    [Tooltip("Character can jump")]
    public bool allowJump = false;
    [Tooltip("Jump forward speed m/s")]
    public float jumpSpeed = 4f;

    public bool IsGrounded { get; private set; }
    public float ForwardInput { get; set; }
    public float StrafeInput { get; set; }
    public float TurnInput { get; set; }
    public bool JumpInput { get; set; }

    new private Rigidbody rigidbody;
    private CapsuleCollider collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        ProcessActions();
    }

    /// <summary>
    /// Checks whether the character is on the ground and updates <see cref="IsGrounded"/>
    /// </summary>
    private void CheckGrounded()
    {
        IsGrounded = false;
        float capsuleHeight = Mathf.Max(collider.radius * 2f, collider.height);
        Vector3 capsuleBottom = transform.TransformPoint(collider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(collider.radius, 0f, 0f).magnitude;

        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                if (hit.distance < maxDist)
                    IsGrounded = true;
            }
        }
    }

    /// <summary>
    /// Processes input actions and converts them into movement
    /// </summary>
    private void ProcessActions()
    {
        // Turning
        transform.Rotate(0,(Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime), 0, Space.World);

        // Movement
        Vector3 move = (transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) + transform.right * Mathf.Clamp(StrafeInput, -1f, 1f)) * moveSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(transform.position + move);

        // Jump
        if (JumpInput && allowJump && IsGrounded)
        {
            rigidbody.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
        }
    }

    public void PowerUp()
    {
        StartCoroutine(PUTimer());
    }

    IEnumerator PUTimer()
    {
        float duration = 5f;

        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }

        jumpSpeed /= 1.5f;
        moveSpeed /= 1.5f;
    }
}
