using UnityEngine;

public class PlayerWalking : IState
{
    private readonly MusguerianKnight _knight;
    private readonly Animator _animator;
    private readonly float _speed = 3f;

    public PlayerWalking(MusguerianKnight knight, Animator animator)
    {
        _knight = knight;
        _animator = animator;
    }

    public void Tick()
    {
        //Mover al personaje
    }

    public void OnEnter()
    {
        _animator.SetBool("Walking", true);
        _knight.attacking = false;
        _knight.speed = _speed;
    }

    public void OnExit()
    {
        _animator.SetBool("Walking", false);
    }
}
