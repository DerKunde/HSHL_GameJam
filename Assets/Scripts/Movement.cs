using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CapsuleCollider2D _collider;
    private BoxCollider2D _groundTrigger;
    private float _move;
    private bool _isFasterFallRunning;
    private bool _movedUp_LastFrame;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float debuffMultiplier = 0.7f;
    private float eternalDebuff = 1f;
  //  [SerializeField] private float additionalMass = .1f;

    [SerializeField] private float jumpForce = 20f;
    [SerializeField] public float jumpCooldown = .2f;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canDoubleJump = true;
    
    [SerializeField] private float defaultGravityScale = 4;
    [SerializeField] private float fallMultiplier = 1.2f;
    [SerializeField] private float maxFallMultiplier = 8f;
    
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = .2f;
    [SerializeField] private float dashCooldown = .4f;
    [SerializeField] private bool canDash = true;

    [SerializeField] private float groundCheckDist = .1f;
    [SerializeField] private bool isGrounded = true;

    public bool dashEnabled = false;
    public bool doubleJumpEnabled = false;
    public bool shieldEnabled = false;
    public bool castJumpEnabled = false;

    public bool debuff_enemies_to_character = false;

    private PlaySound sound;
    private Animator anim;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _groundTrigger = GetComponent<BoxCollider2D>();
        defaultGravityScale = _rb.gravityScale;

        //sound = GameObject.Find("SFX").GetComponent<PlaySound>();
        if(GetComponent<Animator>())
         anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if (canJump) 
            isGrounded = _groundTrigger.Cast(Vector2.down, new ContactFilter2D(), new List<RaycastHit2D>(), groundCheckDist) > 0;
        
        if (isGrounded)
            canDoubleJump = true;
        
        if (_movedUp_LastFrame && _rb.velocity.y < 0 && !_isFasterFallRunning)
            StartCoroutine(FasterFall());
        _movedUp_LastFrame = _rb.velocity.y >= 0;
        

        if (Input.GetButtonDown("Jump") && canJump && isGrounded && !Input.GetAxisRaw("Vertical").Equals(-1f))
            Jump();

        if (doubleJumpEnabled && Input.GetButtonDown("Jump") && !isGrounded && canJump && canDoubleJump)
            DoubleJump();

        if (Input.GetAxisRaw("Vertical").Equals(-1f))
            Fall();

        if (dashEnabled && Input.GetButtonDown("Dash") && !Input.GetAxisRaw("Horizontal").Equals(0f) && canDash)
            Dash();
        
        _move = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
       // Debug.Log(additionalMass);
    }

    private void Jump()
    {
        _rb.velocity *= Vector2.right;
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        StartCoroutine(JumpCooldown());
    }
    private void DoubleJump()
    {
        canDoubleJump = false;
        _rb.gravityScale = defaultGravityScale;
        _rb.velocity *= Vector2.right;
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        StartCoroutine(JumpCooldown());
    }

    private void Dash()
    {
        StartCoroutine(DashDuration(moveSpeed));
        StartCoroutine(DashCooldown());
    }

    private void Fall()
    {
        var hits = new List<RaycastHit2D>();
        _groundTrigger.Cast(Vector2.down, new ContactFilter2D(), hits, groundCheckDist * 2);
        foreach (var hit in hits)
        {
            StartCoroutine(DisableColliderTemp(hit.collider, .4f));
        }
    }

    private void Move()
    {
        //sound.PlayFootstepsSound();
        _rb.velocity = eternalDebuff * _move * moveSpeed * Vector2.right + _rb.velocity.y * Vector2.up;
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private IEnumerator DashDuration(float defaultValue)
    {
        moveSpeed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        moveSpeed = defaultValue;
    }

    private IEnumerator FasterFall()
    {
        _isFasterFallRunning = true;
        yield return new WaitForSeconds(jumpCooldown);
        while (!isGrounded)
        {
            _rb.gravityScale = Mathf.Min(_rb.gravityScale * fallMultiplier, maxFallMultiplier);
            yield return new WaitForSeconds(.1f);
        }

        _rb.gravityScale = defaultGravityScale;
        _isFasterFallRunning = false;
    }

    private IEnumerator DisableColliderTemp(Collider2D col, float duration_s)
    {
        col.enabled = false;
        yield return new WaitForSeconds(duration_s);
        col.enabled = true;
    }

    public void toggleShield()
    {
        if (shieldEnabled)
        {
            shieldEnabled = false;
        }
        else
        {
            shieldEnabled = true;
        }
    }

    public void coinDebuff(bool enabled)
    {
       
        if (enabled)
        {
            eternalDebuff = debuffMultiplier;
            _rb.mass = 1.9f;
            Debug.Log("et debuff: " + eternalDebuff);
        }
        else
        {
            Debug.Log("debuff end");
            eternalDebuff = 1;
            _rb.mass = 1.6f;
        }

    }

    

}
