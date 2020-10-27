using UnityEngine;

public class Jab3 : IState
{
    private readonly MusguerianKnight _knight;
    private readonly Animator _animator;

    private readonly float _speed = 10f;

    public float maxTimeStuck = 0.35f;
    public float timeStuck;

    public Jab3(MusguerianKnight knight, Animator animator)
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
        _animator.SetBool("Jab3", true);
    }

    public void OnExit()
    {
        _animator.SetBool("Jab3", false);
    }
}