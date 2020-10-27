using UnityEngine;

internal class Idle : IState
{
    private readonly BossIA _bossIA;
    private readonly Animator _animator;
    private readonly Transform _target;
    private readonly float _speed;

    private Vector3 dir;

    public float maxTimeStuck = 3f;
    public float timeStuck;

    public Idle(BossIA bossIA, Transform target, Animator animator, float speed)
    {
        _bossIA = bossIA;
        _animator = animator;
        _target = target;
        _speed = speed;
    }
    public void Tick()
    {
        FollowPlayer();
        _bossIA.PrintState(this.GetType());
        timeStuck += Time.deltaTime;
        if (timeStuck >= maxTimeStuck)
            _bossIA.stateDecision = Random.Range(0, 3);
    }

    public void OnEnter()
    {
        _animator.SetBool("Idle", true);
        timeStuck = 0f;
    }

    public void OnExit()
    {
        _animator.SetBool("Idle", false);
    }

    private void FollowPlayer()
    {
        var direction = _target.position.x - _bossIA.transform.position.x;
        dir = new Vector3(direction, 0);
        _bossIA.transform.Translate(dir * Time.deltaTime * _speed);
    }
}
