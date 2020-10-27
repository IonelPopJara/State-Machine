using UnityEngine;

public class PlayerJumping : IState
{
    private readonly MusguerianKnight _knight;
    private readonly MusguerianMovement _movement;
    private readonly Animator _animator;

    private readonly float _jumpHeight = 3f;
    private readonly float _speed = 10f;

    public PlayerJumping(MusguerianKnight knight, MusguerianMovement movement ,Animator animator)
    {
        _knight = knight;
        _animator = animator;
        _movement = movement;
    }

    public void Tick()
    {
        
    }

    public void OnEnter()
    {
        _animator.SetBool("Jumping", true);
        _movement.CharacterJump(_jumpHeight);
        _knight.attacking = false;
        _knight.speed = _speed;
    }

    public void OnExit()
    {
        _animator.SetBool("Jumping", false);
    }
}
