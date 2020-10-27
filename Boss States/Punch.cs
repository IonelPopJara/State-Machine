using UnityEngine;

internal class Punch : IState
{
    private readonly BossIA _bossIA;
    private readonly Animator _animator;

    public float maxTimeStuck = 2f;
    public float timeStuck;

    public Punch(BossIA bossIA, Animator animator)
    {
        _bossIA = bossIA;
        _animator = animator;
    }

    public void Tick()
    {
        _bossIA.PrintState(this.GetType());
        timeStuck += Time.deltaTime;
    }

    public void OnEnter()
    {
        _animator.SetBool("Punch", true);
        timeStuck = 0f;
    }

    public void OnExit()
    {
        _animator.SetBool("Punch", false);
    }
}