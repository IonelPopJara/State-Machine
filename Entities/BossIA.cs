using System;
using UnityEngine.UI;
using UnityEngine;

public class BossIA : MonoBehaviour
{
    public Animator animator;
    public Transform target;
    public GameObject fireBallPrefab;
    public Transform firePoint;
    public DeathLaser laser;
    public Slider healthBar;
    public float idleSpeed;

    public int health = 500;

    public bool enraged = false;

    public float bombSpeed;
    public bool playerMoving;
    public bool playerInMeleeRange;
    public int stateDecision;

    private StateMachine _stateMachine;

    private void Awake()
    {
        healthBar.maxValue = health;
        _stateMachine = new StateMachine();

        var idle = new Idle(this, target, animator, idleSpeed);
        var bomb = new Bomb(this, animator);
        var laser = new Laser(this, animator);
        var punch = new Punch(this, animator);

        At(idle, bomb, IdleToBomb());
        At(bomb, idle, BombToIdle());
        At(idle, laser, IdleToLaser());
        At(laser, idle, LaserToIdle());
        At(idle, punch, IdleToPunch());
        At(punch, idle, PunchToIdle());

        _stateMachine.SetState(idle);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> IdleToBomb() => () => (idle.timeStuck > idle.maxTimeStuck) && (stateDecision == 0); //&& (playerMoving) && (!playerInMeleeRange);
        Func<bool> IdleToLaser() => () => (idle.timeStuck > idle.maxTimeStuck) && (stateDecision == 1); //&& (!playerMoving) && (!playerInMeleeRange);
        Func<bool> IdleToPunch() => () => (idle.timeStuck > idle.maxTimeStuck) && (stateDecision == 2);
        
        Func<bool> BombToIdle() => () => (bomb.timeStuck > bomb.maxTimeStuck);
        Func<bool> LaserToIdle() => () => (laser.timeStuck > laser.maxTimeStuck);
        Func<bool> PunchToIdle() => () => (punch.timeStuck > punch.maxTimeStuck);

    }

    private void Update()
    {
        healthBar.value = health;
        _stateMachine.Tick();
        if (health <= 100)
            enraged = true;
        if (enraged)
        {
            transform.localScale = new Vector3(3f, 3f, 3f);
        }
    }

    #region debug
    public void PrintIdle(bool time)
    {
        print($"Idle time: {time} || Hola:");
    }

    public void PrintLaser()
    {
        print("Laser");
    }

    public void PrintBomb()
    {
        print("Bomb");
    }

    public void PrintState(Type stateName)
    {
        //print(stateName);
    }
    #endregion

    public void LaunchBombPrefab()
    {
        var dir = target.position - firePoint.position;
        dir.Normalize();
        GameObject bomb = Instantiate(fireBallPrefab, firePoint.position, Quaternion.identity);
        bomb.GetComponent<Rigidbody>().AddForce(dir * bombSpeed, ForceMode.Impulse);
    }

    public void ActivateLaser()
    {
        laser.isLaserOn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if (other.CompareTag("Player"))
        {
            MusguerianKnight knight = other.gameObject.GetComponent<MusguerianKnight>();
            knight?.TakeDamage();
            print("Punch Damage");
        }
    }

    public void TakeDamage()
    {
        animator.SetTrigger("Hurt");
        health -= 5;
    }

    private bool GoingToCrash()
    {
        return false;
    }

    private void DoNot()
    {
        health = 100;
    }
}
