using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChargeJump : MonoBehaviour
{
    public float minJumpForce = 5f;
    public float maxJumpForce = 15f;
    public float chargeSpeed = 10f; // How fast the charge increases
    
    private float currentJumpForce;
    private bool isCharging = false;
    private bool isGrounded;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentJumpForce = minJumpForce;
    }

    void Update()
    {
        // Check if grounded (Simple check; use Raycasts for better precision)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (isGrounded)
        {
            // Start charging
            if (Input.GetButtonDown("Jump"))
            {
                isCharging = true;
                currentJumpForce = minJumpForce;
            }

            // Charge while holding button
            if (isCharging && Input.GetButton("Jump"))
            {
                currentJumpForce += chargeSpeed * Time.deltaTime;
                currentJumpForce = Mathf.Clamp(currentJumpForce, minJumpForce, maxJumpForce);
            }

            // Release to jump
            if (Input.GetButtonUp("Jump") && isCharging)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        isCharging = false;
        currentJumpForce = minJumpForce; // Reset charge
    }
}
