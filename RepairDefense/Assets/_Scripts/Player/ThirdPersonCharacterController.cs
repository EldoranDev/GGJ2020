using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Rigidbody))]

public class ThirdPersonCharacterController : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform camAnker;
    [SerializeField]
    float speed = 0;
    [SerializeField]
    float rotationSpeed = 0 ;
    [SerializeField]
    float jumpVelocity = 0;

    [SerializeField]
    float castOffset;
    [SerializeField]
    float castRadius;
    
    bool IsGrounded = true;
    bool Isinteracting = false;
    Rigidbody rigi;

    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        #region mouseDebug
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        #endregion

        if (IsGrounded)
        {
            if (!Isinteracting)
            {
                CheckRunInput();
                CheckJump();
            }
            CheckInteract();
        }
        if (!Isinteracting)
        {
            CheckRunInput();
        }
        CheckMouseLookInput();
    }
    private void FixedUpdate()
    {
        CheckGrounded();
        
    }

    private void CheckRunInput()
    {
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("IsRunning", true);
            this.transform.Translate(Vector3.forward * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.SetBool("IsRunning", true);
            this.transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            anim.SetBool("IsRunning", false);
        }
    }

    private void CheckMouseLookInput()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            float x = rotationSpeed * Input.GetAxis("Mouse X");
            float y = rotationSpeed * -Input.GetAxis("Mouse Y");
            transform.Rotate(0, x, 0);
            Camera.main.transform.RotateAround(camAnker.position, Camera.main.transform.right, -Input.GetAxis("Mouse Y") * rotationSpeed);
        }
    }

    private void CheckInteract()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            rigi.velocity = Vector3.zero;
            anim.SetBool("IsInteracting", true);
            anim.SetBool("IsRunning", false);
            Isinteracting = true;
        }
        else
        {
            anim.SetBool("IsInteracting", false);
            Isinteracting = false;
        }
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigi.velocity += jumpVelocity * Vector3.up;
            anim.SetBool("IsGrounded", false);
        }
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + Vector3.up * castOffset, castRadius, Vector3.down, out hit, .0035f))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                IsGrounded = true;
                anim.SetBool("IsGrounded", true);
            }
        }
        else
        {
            IsGrounded = false;
            anim.SetBool("IsGrounded", false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.up * castOffset, castRadius);
    }
}
