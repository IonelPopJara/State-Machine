using UnityEngine;

internal class Laser : IState
{
    private readonly BossIA _bossIA;
    private readonly Animator _animator;

    public float maxTimeStuck = 10f;
    public float timeStuck;

    private float laserTime = 1f;
    private float currentTime;

    public Laser(BossIA bossIA, Animator animator)
    {
        _bossIA = bossIA;
        _animator = animator;
    }

    public void Tick()
    {
        _bossIA.PrintState(this.GetType());
        timeStuck += Time.deltaTime;

        if (currentTime <= 0)
        {
            _bossIA.ActivateLaser();
            currentTime = laserTime;
        }
        else if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    public void OnEnter()
    {
        _animator.SetBool("Laser", true);
        timeStuck = 0f;
        if (_bossIA.enraged)
            laserTime = 0.5f;
        timeStuck = laserTime;
    }

    public void OnExit()
    {
        _animator.SetBool("Laser", false);
    }
}
