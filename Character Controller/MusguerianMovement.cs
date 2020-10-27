using UnityEngine;

public class MusguerianMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 _inputs;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void CharacterRotate(float horizontalInput)
    {
        _inputs = Vector3.zero;
        _inputs.x = horizontalInput;

        if (_inputs != Vector3.zero)
            transform.right = _inputs;
    }

    public void CharacterMove(float speed)
    {
        rb.MovePosition(rb.position + _inputs * speed * Time.fixedDeltaTime);
    }

    public void CharacterJump(float jumpHeight)
    {
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }
}
