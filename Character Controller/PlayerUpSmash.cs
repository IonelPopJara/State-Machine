using UnityEngine;

public class PlayerUpSmash : IState
{
    private readonly MusguerianKnight _knight;
    private readonly Animator _animator;

    private readonly float _speed = 10f;
    
    public float maxTimeStuck = 1.1f;
    public float timeStuck;

    public PlayerUpSmash(MusguerianKnight knight, Animator animator)
    {
        _knight = knight;
        _animator = animator;
    }

    public void Tick()
    {
        timeStuck += Time.deltaTime;
    }

    public void OnEnter()
    {
        timeStuck = 0f;
        _knight.speed = _speed;
        _animator.SetBool("UpSmash", true);
    }

    public void OnExit()
    {
        _animator.SetBool("UpSmash", false);
    }
}