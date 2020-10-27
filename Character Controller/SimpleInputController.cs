using UnityEngine;

public class SimpleInputController : MonoBehaviour
{
    public bool Up;
    public bool Down;
    public bool Right;
    public bool Left;

    public bool Jump;
    public bool Sprint;

    public bool AttackA;
    public bool AttackB;

    private float VerticalInput;
    private float HorizontalInput;

    private Vector3 TEMPVec3;

    private void Update()
    {
        CheckInput();
    }

    public void CheckInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        Jump = Input.GetButton("Jump");
        Sprint = Input.GetKey(KeyCode.LeftShift);

        AttackA = Input.GetKey(KeyCode.J);
        AttackB = Input.GetKey(KeyCode.K);

        Up = VerticalInput > 0;
        Down = VerticalInput < 0;
        Right = HorizontalInput > 0;
        Left = HorizontalInput < 0;
    }

    public float GetHorizontal()
    {
        return HorizontalInput;
    }

    public float GetVertical()
    {
        return VerticalInput;
    }

    public bool GetJump()
    {
        return Jump;
    }

    public bool GetSprint()
    {
        return Sprint;
    }

    public bool GetAttackA()
    {
        return AttackA;
    }

    public bool GetAttackB()
    {
        return AttackB;
    }

    public Vector3 GetMovementDirectionVector()
    {
        //temp vector for movement dir gets set to the value of an
        //otherwise unused vector that always has the value of (0,0,0)
        TEMPVec3 = Vector3.zero;

        TEMPVec3 = new Vector3(HorizontalInput, VerticalInput);

        //return the movement vector
        return TEMPVec3;
    }
}
