using UnityEngine;

public class PlayerRunning : IState
{
    private readonly MusguerianKnight _knight;
    private readonly Animator _animator;
    private readonly float _speed = 10f;

    public PlayerRunning(MusguerianKnight knight, Animator animator)
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
        _animator.SetBool("Running", true);
        _knight.attacking = false;
        _knight.speed = _speed;
    }

    public void OnExit()
    {
        _animator.SetBool("Running", false);
    }
}
