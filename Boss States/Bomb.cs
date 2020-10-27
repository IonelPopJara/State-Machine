using UnityEngine;

internal class Bomb : IState
{
    private readonly BossIA _bossIA;
    private readonly Animator _animator;

    public float maxTimeStuck = 10f;
    public float timeStuck;

    private float bombTime = 1.5f;
    private float currentTime;

    public Bomb(BossIA bossIA, Animator animator)
    {
        _bossIA = bossIA;
        _animator = animator;
    }

    public void Tick()
    {
        _bossIA.PrintState(this.GetType());
        timeStuck += Time.deltaTime;

        if(currentTime <= 0)
        {
            _bossIA.LaunchBombPrefab();
            currentTime = bombTime;
        }
        else if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    public void OnEnter()
    {
        _animator.SetBool("Bomb", true);
        if (_bossIA.enraged)
            bombTime = 0.8f;
        timeStuck = bombTime;
        currentTime = 0f;
    }

    public void OnExit()
    {
        _animator.SetBool("Bomb", false);
    }
}
