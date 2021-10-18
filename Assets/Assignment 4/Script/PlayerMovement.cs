using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer myRenderer;
    public LayerMask collisionLayer;
    public GameObject hitbox;
    //[SerializeField] private Sprite[] WalkSprites;
    //[SerializeField] private Sprite[] JumpSprites;
    //[SerializeField] private Sprite[] IdleSprites;

    [Space]
    [Header("Stats")]
    public float animationSpeed = 0.3f;
    public float timer;
    public int currentSpriteIndex = 0;
    [Space]
    public float walkSpeed = 10;
    public float attackLag = 0.1f;
    public float jumpForce = 10;
    public float dashSpeed = 20;
    public float dashLag = 0.1f;
    public float slideSpeed = 5;
    public float gravityScaler = 10;
    [Space]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    [Space]
    public Vector2 bottomOffset, rightOffset1, rightOffset2, leftOffset1, leftOffset2;
    public float collisionRadius = 0.25f;
    public float AutoClimbHeight1 = 0.5f;
    public float AutoClimbHeight2 = -0.5f;
    public float autoClimbDelay = 0.5f;

    [Space]
    [Header("Booleans")]
    public bool canMove = true;
    public bool isAttacking;
    public bool isJumping;
    public bool wallJumped;
    public bool isDashing;
    public bool hasDashed;
    public bool wallGrab;
    public bool wallSlide;
    public bool refreshed;
    public bool VariableJump;
    [Space]
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public bool AutoClimableRight;
    public bool AutoClimableLeft;
    [Space]
    public int side = 1;
    public int wallSide;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        collisionCheck();

        inputCheck();

        variableJump();

        timer += Time.deltaTime;
        //Debug.Log(rb.gravityScale);

    }
    private void collisionCheck()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset + new Vector2(0.75f, 0), collisionRadius, collisionLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset + new Vector2(-0.75f, 0), collisionRadius, collisionLayer);

        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset1, collisionRadius, collisionLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset2, collisionRadius, collisionLayer);
        AutoClimableLeft = !Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(leftOffset1.x, 0) + new Vector2(0, AutoClimbHeight1), collisionRadius, collisionLayer)
            && Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(leftOffset1.x, 0) + new Vector2(0, AutoClimbHeight2), collisionRadius, collisionLayer);
        
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset1, collisionRadius, collisionLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset2, collisionRadius, collisionLayer);
        AutoClimableRight = !Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(rightOffset1.x, 0) + new Vector2(0, AutoClimbHeight1), collisionRadius, collisionLayer)
            && Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(rightOffset1.x, 0) + new Vector2(0, AutoClimbHeight2), collisionRadius, collisionLayer);

        onWall = onRightWall || onLeftWall;
        wallSide = onRightWall ? -1 : 1;
    }

    private void variableJump()
    {
        if (!isDashing && VariableJump)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.C))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }
    private void inputCheck()
    {
        if (onGround && !refreshed && !isDashing)
        {
            Refresh();
            refreshed = true;
        }

        if (refreshed)
        {
            refreshed = false;
        }

        if (onGround && !isDashing)
        {
            wallJumped = false;
        }

        Walk(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.V))
        {

            Attack();

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //anim.SetTrigger("jump");

            if (onGround)
            {
                Jump((Vector2.up), false);
            }
            else
            {
                if (onRightWall)
                {
                    StopCoroutine(DisableMovement(0));
                    StartCoroutine(DisableMovement(0.1f));
                    if (Input.GetAxisRaw("Horizontal") >= 0 && rb.velocity.y <= 0 && wallGrab)
                    {
                        Jump((Vector2.up), true);
                    }
                    else {
                        if (wallGrab || wallSlide)
                        {
                            Jump((Vector2.up + Vector2.left), true);
                            wallJumped = true;
                        }
                        else
                        {
                            Jump((Vector2.up / 2f + Vector2.left / 2f), true);
                            wallJumped = true;
                        }
                    }
                }
                else if (onLeftWall)
                {
                    StopCoroutine(DisableMovement(0));
                    StartCoroutine(DisableMovement(0.1f));
                    if (Input.GetAxisRaw("Horizontal") <= 0 && rb.velocity.y <= 0 && wallGrab)
                    {
                        Jump((Vector2.up), true);
                    }
                    else
                    {
                        if (wallGrab || wallSlide)
                        {
                            Jump((Vector2.up + Vector2.right), true);
                            wallJumped = true;
                        }
                        else
                        {
                            Jump((Vector2.up / 2f + Vector2.right / 2f), true);
                            wallJumped = true;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.X) && !hasDashed)
        {

            if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") == 0)
            {
                Dash(0, Input.GetAxisRaw("Vertical"));
            }
            else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") != 0)
            {
                Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
            else if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0)
            {
                Dash(Input.GetAxisRaw("Horizontal") / 1.1f, Input.GetAxisRaw("Vertical") / 1.1f);
            }
            else
            {
                Dash(side * 1.5f, Input.GetAxisRaw("Vertical"));
            }

        }

        if (onWall && Input.GetKey(KeyCode.Z) && canMove)
        {
            //if (side != wallSide)
                //anim.Flip(side * -1);
            wallGrab = true;
            wallSlide = false;
            WallGrab();
        }

        if (Input.GetKeyUp(KeyCode.Z) || !onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
            if (!isDashing)
            {
                rb.gravityScale = gravityScaler;
            }
        }

        if (onWall && !onGround)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }
        else { 
            wallSlide = false;
        }

        if (wallGrab || wallSlide || !canMove)
            return;

        if (Input.GetAxisRaw("Horizontal")>0)
        {
            side = 1;
            //anim.Flip(side);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            side = -1;  
            //anim.Flip(side);
        }


    }

    private void Walk(float dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {   
            if (!(onLeftWall && dir < 0) && !(onRightWall && dir > 0))
                rb.velocity = new Vector2(dir * walkSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir * walkSpeed/2f, rb.velocity.y)), 10 * Time.deltaTime);
        }

    }

    public void Attack()
    {
        if (!canMove || isAttacking)
            return;
        hitbox.transform.position = transform.position + new Vector3(side * 2, 0, 0);
        hitbox.SetActive(true);
        StartCoroutine(Attacking());
    }
    IEnumerator Attacking()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.1f);
        hitbox.SetActive(false);
        yield return new WaitForSeconds(attackLag);
        isAttacking = false;
    }
public void Jump(Vector2 dir, bool wall)
    {
        //if (!canMove)
            //return;

        rb.velocity = new Vector2(0, 0);
        rb.velocity = dir * jumpForce;
        //StartCoroutine(Jumping());
    }
    IEnumerator Jumping()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.05f);
        isJumping = false;
    }

    private void Dash(float x, float y)
    {
        if (!canMove)
            return;

        hasDashed = true;

        //anim.SetTrigger("dash");

        rb.velocity = Vector2.zero;
        GetComponent<TrailRenderer>().enabled = true;
        rb.velocity = new Vector2(x, y) * dashSpeed;
        StartCoroutine(Dashing());
    }

    IEnumerator Dashing()
    {
        if (onGround)
        {
            //hasDashed = false;
        }
        rb.gravityScale = 0;
        isDashing = true;
        wallJumped = true;
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashLag); 
        rb.gravityScale = gravityScaler;
        isDashing = false;
        wallJumped = false;
        yield return new WaitForSeconds(0.0f);
        GetComponent<TrailRenderer>().enabled = false;
    }
    private void WallGrab()
    {
        if (onGround && !Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
        }

        rb.gravityScale = 0;
        if (Input.GetKey(KeyCode.UpArrow))
            rb.velocity = new Vector2(rb.velocity.x, walkSpeed * 1.0f);
        else if (Input.GetKey(KeyCode.DownArrow))
            rb.velocity = new Vector2(rb.velocity.x, walkSpeed * -1.25f);
        else if (!onGround && !isDashing && !wallJumped)
            rb.velocity = Vector2.zero;

        StartCoroutine(AutoClimb());
    }

    private void WallSlide()
    {
        if (wallSide != side)
            //anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if ((Input.GetAxisRaw("Horizontal") > 0 && onRightWall) || (Input.GetAxisRaw("Horizontal") < 0 && onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;
        if (rb.velocity.y <= 0)
            rb.velocity = new Vector2(push, -slideSpeed);
    }
    IEnumerator AutoClimb()
    {
        if (Input.GetKey(KeyCode.UpArrow)) 
        { 
            //canMove = false;
            if (onLeftWall && AutoClimableLeft)
            {
                rb.gravityScale = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                yield return new WaitForSeconds(autoClimbDelay);
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y + 0.25f, transform.position.z);
                rb.gravityScale = gravityScaler;
            }
            if (onRightWall && AutoClimableRight)
            {
                rb.gravityScale = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                yield return new WaitForSeconds(autoClimbDelay);
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y + 0.25f, transform.position.z);
                rb.gravityScale = gravityScaler;
            }
            //canMove = true;
        }
    }
    public IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void Refresh()
    {
        isDashing = false;
        hasDashed = false;
        isJumping = false;
        wallJumped = false;
        VariableJump = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset + new Vector2(0.75f, 0), collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset + new Vector2(-0.75f, 0), collisionRadius);

        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset1, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset1, collisionRadius);

        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset2, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset2, collisionRadius);

        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(rightOffset1.x, 0) + new Vector2(0, AutoClimbHeight1), collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(rightOffset1.x, 0) + new Vector2(0, AutoClimbHeight2), collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(leftOffset1.x, 0) + new Vector2(0, AutoClimbHeight1), collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(leftOffset1.x, 0) + new Vector2(0, AutoClimbHeight2), collisionRadius);
    }

    /*private void PlayerAnimation(Sprite[] currentSprite)
    {
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer = 0;
            currentSpriteIndex++;
            currentSpriteIndex %= currentSprite.Length;
        }
        myRenderer.sprite = currentSprite[currentSpriteIndex];
    }
    */
}
