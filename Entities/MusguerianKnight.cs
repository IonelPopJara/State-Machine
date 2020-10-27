using System;
using UnityEngine.UI;
using UnityEngine;

public class MusguerianKnight : MonoBehaviour
{
    public GroundCheck groundCheck;
    public Animator animator;
    public Rigidbody rb;

    public Slider healthBar;

    public int health;

    public float walkSpeed = 3f;
    public float runSpeed = 10f;

    public float speed;

    private StateMachine _stateMachine;
    private SimpleInputController _inputController;
    private MusguerianMovement _musguerianMovement;

    private float horizontal;
    private float horizontalZeroTime;

    private bool attackButton;
    private bool upButton;
    private bool sprinting;
    private bool jump;

    public bool attacking;
    public bool beingHurt;

    private bool grounded;

    public void Awake()
    {
        healthBar.maxValue = health;

        _stateMachine = new StateMachine();

        _inputController = GetComponent<SimpleInputController>();
        _musguerianMovement = GetComponent<MusguerianMovement>();

        var idle = new PlayerIdle(this, animator);
        var walking = new PlayerWalking(this, animator);
        var running = new PlayerRunning(this, animator);
        var jumping = new PlayerJumping(this, _musguerianMovement, animator);
        var upSmash = new PlayerUpSmash(this, animator);
        var jab1 = new Jab1(this, animator);
        var jab2 = new Jab2(this, animator);
        var jab3 = new Jab3(this, animator);

        At(idle, walking, IdleToWalking());
        At(walking, idle, WalkingToIdle());
        At(walking, running, WalkingToRunning());
        At(running, walking, RunningToWalking());
        At(jumping, running, JumpingToRunning());
        At(upSmash, running, UpSmashToRunning());
        At(jab1, running, Jab1ToRunning());
        //At(jab1, jab2, Jab1ToJab2());
        //At(jab2, jab3, Jab2ToJab3());
        //At(jab2, running, Jab2ToRunning());
        //At(jab3, running, Jab3ToRunning());
        At(idle, jab1, TransitionToJab());
        At(walking, jab1, TransitionToJab());
        At(running, jab1, TransitionToJab());
        At(jumping, jab1, TransitionToJab());

        _stateMachine.AddAnyTransition(jumping, TransitionToJumping());
        _stateMachine.AddAnyTransition(upSmash, TransitionToUpSmash());
        _stateMachine.AddAnyTransition(jab1, TransitionToJab());
        _stateMachine.SetState(idle);

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

        Func<bool> IdleToWalking() => () => ((horizontal != 0) && grounded);
        Func<bool> WalkingToIdle() => () => ((horizontal == 0) && grounded);
        Func<bool> WalkingToRunning() => () => (sprinting && (horizontal != 0) && grounded);
        Func<bool> RunningToWalking() => () => ((horizontal == 0) && horizontalZeroTime > 0.05f && grounded);
        Func<bool> TransitionToJumping() => () => (jump && grounded && !attacking && !beingHurt);
        Func<bool> JumpingToRunning() => () => (grounded);
        Func<bool> TransitionToUpSmash() => () => (attackButton && upButton);
        Func<bool> UpSmashToRunning() => () => (upSmash.timeStuck > upSmash.maxTimeStuck);
        Func<bool> TransitionToJab() => () => (attackButton && !upButton);
        Func<bool> Jab1ToRunning() => () => (jab1.timeStuck > jab1.maxTimeStuck);
        //Func<bool> Jab1ToJab2() => () => ((jab1.timeStuck < jab1.maxTimeStuck) && attackButton);
        //Func<bool> Jab2ToJab3() => () => ((jab2.timeStuck < jab2.maxTimeStuck) && attackButton);
        //Func<bool> Jab2ToRunning() => () => (jab2.timeStuck > jab2.maxTimeStuck);
        //Func<bool> Jab3ToRunning() => () => (jab3.timeStuck > jab3.maxTimeStuck);

    }

    public void Update()
    {
        healthBar.value = health;
        _stateMachine.Tick();
        CheckInputs();
        _musguerianMovement.CharacterRotate(horizontal);
    }

    private void FixedUpdate()
    {
        _musguerianMovement.CharacterMove(speed);
    }

    public void CheckInputs()
    {
        horizontal = _inputController.GetHorizontal();

        sprinting = _inputController.GetSprint();
        jump = _inputController.GetJump();

        upButton = _inputController.Up;
        attackButton = _inputController.GetAttackA();

        grounded = groundCheck.ReturnGrounded();

        if(horizontal == 0)
        {
            horizontalZeroTime += Time.deltaTime;
            horizontalZeroTime = Mathf.Clamp(horizontalZeroTime, 0f, 1f);
        }
        else
        {
            horizontalZeroTime = 0;
        }
    }

    public void TakeDamage()
    {
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(0.1f, 0.1f));
        health -= 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if(other.CompareTag("Enemy"))
        {
            BossIA boss = other.gameObject.GetComponent<BossIA>();
            boss?.TakeDamage();

            print("Enemy Hit");
        }
    }
}
