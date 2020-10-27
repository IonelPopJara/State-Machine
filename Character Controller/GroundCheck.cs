using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask Ground;
    
    private float groundDistance = 0.2f;

    private bool _isGrounded;

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(transform.position, groundDistance, Ground, QueryTriggerInteraction.Ignore);
    }

    public bool ReturnGrounded()
    {
        return _isGrounded;
    }
}
