using UnityEngine;

public class PlayerIdle : IState
{
    private readonly MusguerianKnight _knight;
    private readonly Animator _animator;

    public PlayerIdle(MusguerianKnight knight, Animator animator)
    {
        _knight = knight;
        _animator = animator;
    }

    public void Tick()
    {

    }

    public void OnEnter()
    {
        _animator.SetBool("Idle", true);
        _knight.attacking = false;
    }

    public void OnExit()
    {
        _animator.SetBool("Idle", false);
    }
}
