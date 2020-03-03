using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characther
{
    // Start is called before the first frame update
    public float timeDelayStune;
    public float jampDivide;
    public float substractTimeStune;
    public float delayUseDash = 1.5f;
    public float auxDelayUseDash = 1.5f;
    [Header("Controles Jugador")]
    public KeyCode JumpMovement;
    public KeyCode leftMovement;
    public KeyCode rightMovement;
    public KeyCode keyAccion;
    public KeyCode keyAttack;
    public KeyCode dash;

    [Header("Velocidades del Jugador")]
    public float QuickSpeed; //Rapido.
    public float NormalSpeed;//Normal.
    public float SlowlySpeed;//Lento.
    //public float dashSpeed;

    [Header("Relacion Vida/Velocidad")]
    public int lifeQuickSpeed;
    public int lifeNormalSpeed;
    public int lifeSlowlySpeed;

    [HideInInspector]
    public Rigidbody2D rigidbody;
    public TypeMovement typeMovement;
    Vector2 pos = new Vector2(10, 30);
    Vector2 size = new Vector2(100, 30);
    public float barDisplay;
    public float auxLife;
    private Direction direction;
    private bool dashing;
    public float delayDash;
    public float auxDelayDash;
    public float dashSpeed;

    private int timeStartDashing;

    [Header("Armas Melee")]
    public MeleeWeapon DefaultWeapon;
    public MeleeWeapon BrazosDeShokerWeapon;
    public MeleeWeapon SableAssasinWeapon;

    [Header("Armas a Distancia")]
    public DistanceWeapon RifleTrackerWeapon;

    public WeaponsPlayer currentWeapon;
    public DistanceWeapon distaceWeapon;

    private StateMovement stateMovement;
    private Item currentItem;
    public EquipedWeapon equipedWeapon;

    [Header("Animacion")]
    public Animator animator;

    private bool inMovement;
    private bool jumping;
    private bool topHigh;
    private bool jumpEnded;
    private bool isAttacking;

    private HealthBar healthBar;
    private enum Direction
    {
        Left,
        Right,
    }
    public enum EquipedWeapon
    {
        Default,
        BrazoDeShoker,
        RifleTracker,
        SableAssasin,
    }
    public enum TypeMovement
    {
        Force,
        Position,
    }
    public enum StateMovement
    {
        QuickMovement, //Movimiento Rapido.
        NormalMovement,//Movimiento Normal.
        SlowlyMovement,//Movimiento Despacio.

    }
    public enum StatePlayer
    {
        none,
        Stunt,
    }
    public StatePlayer statePlayer;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        equipedWeapon = EquipedWeapon.Default;
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<HealthBar>(); ;

    }
    private void OnGUI()
    {
        GUI.Box(new Rect(pos, size), "");
        GUI.Box(new Rect(pos.x, pos.y,life, size.y), "");
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckDie();
        CheckStatePlayer();
        CheckWeapon();
        update_life_bar();
        updateAnimator();
        if (currentItem != null)
        {
            CheckItem();
        }
        if (equipedWeapon == EquipedWeapon.RifleTracker)
        {
            if (Input.GetKeyDown(keyAttack))
            {
                distaceWeapon.ShootWeapon();
                currentWeapon.CountUse--;
                distaceWeapon.CountUse--;
                if (currentWeapon.CountUse <= 0 || distaceWeapon.CountUse <= 0)
                {
                    equipedWeapon = EquipedWeapon.Default;
                }
            }
        }
    }
    public void CheckWeapon()
    {
        if (currentWeapon != null)
        {

            if (Input.GetKey(keyAttack) && inFloor)
            {
                switch (equipedWeapon)
                {
                    case EquipedWeapon.Default:

                        DefaultWeapon.gameObject.SetActive(true);
                        BrazosDeShokerWeapon.gameObject.SetActive(false);
                        SableAssasinWeapon.gameObject.SetActive(false);
                        RifleTrackerWeapon.gameObject.SetActive(false);
                        distaceWeapon = null;
                        currentWeapon = DefaultWeapon;
                        break;
                    case EquipedWeapon.BrazoDeShoker:
                        DefaultWeapon.gameObject.SetActive(false);
                        BrazosDeShokerWeapon.gameObject.SetActive(true);
                        SableAssasinWeapon.gameObject.SetActive(false);
                        RifleTrackerWeapon.gameObject.SetActive(false);

                        distaceWeapon = null;
                        currentWeapon = BrazosDeShokerWeapon;
                        break;
                    case EquipedWeapon.SableAssasin:
                        DefaultWeapon.gameObject.SetActive(false);
                        BrazosDeShokerWeapon.gameObject.SetActive(false);
                        SableAssasinWeapon.gameObject.SetActive(true);
                        RifleTrackerWeapon.gameObject.SetActive(false);

                        distaceWeapon = null;
                        currentWeapon = SableAssasinWeapon;
                        break;
                    case EquipedWeapon.RifleTracker:
                        DefaultWeapon.gameObject.SetActive(false);
                        BrazosDeShokerWeapon.gameObject.SetActive(false);
                        SableAssasinWeapon.gameObject.SetActive(false);
                        RifleTrackerWeapon.gameObject.SetActive(true);

                        currentWeapon = RifleTrackerWeapon;
                        distaceWeapon = RifleTrackerWeapon;
                        break;
                }
                if (currentWeapon.CountUse <= 0)
                {
                    equipedWeapon = EquipedWeapon.Default;
                }
                if (distaceWeapon != null)
                {
                    if (distaceWeapon.CountUse <= 0)
                    {
                        equipedWeapon = EquipedWeapon.Default;
                    }
                }
            }
        }
    }
    public void CheckStatePlayer()
    {
        if (statePlayer == StatePlayer.Stunt)
        {
            if (timeDelayStune > 0)
            {
                timeDelayStune = timeDelayStune - Time.deltaTime;
            }
            else if (timeDelayStune <= 0)
            {
                statePlayer = StatePlayer.none;   
            }
        }
    }
    public void CheckDie()
    {
        CheckDead();
        if (die)
        {
            gameObject.SetActive(false);
            transform.position = new Vector3(-1000, -1000, -1000);
        }
    }
    public override void Movement()
    {
        //float vertical = Input.GetAxis("Vertical");
        //Debug.Log(speedMovement);
        CheckSpeed();
        CheckAnimationMovement();
        chekDashing();
        chekMovementForAnimation();
        if (statePlayer != StatePlayer.Stunt)
        {
            CheckSpeed();
            CheckAnimationMovement();
            if (Input.GetKey(rightMovement))
            {
                moveRight();
            }
            else if (Input.GetKey(leftMovement))
            {
                moveLeft();
            }
            bool dashido = Input.GetKeyDown(dash);
            if (dashing)
            {
                animator.Play("Player_Dash");
                if (direction.Equals(Direction.Right))
                {
                    rigidbody.AddForce(Vector2.right * speedJump * dashSpeed, ForceMode2D.Impulse);
                    speedMovement = speedMovement / jampDivide;
                }
                else
                {
                    rigidbody.AddForce(Vector2.left * speedJump * dashSpeed, ForceMode2D.Impulse);
                    speedMovement = speedMovement / jampDivide;
                }
                gameObject.layer = 9;
            }
            else if (gameObject.layer != 0)
            {
                gameObject.layer = 0;
            }
            if (delayUseDash <= 0)
            {
                if (dashido && inFloor || dashing)
                {
                    timeStartDashing = System.DateTime.Now.Second;
                    if (dashido)
                    {
                        dashing = true;
                        dashido = false;
                    }
                    delayUseDash = auxDelayUseDash;
                }
            }
            else if (delayUseDash > 0)
            {
                delayUseDash = delayUseDash - Time.deltaTime;
            }
            if (Input.GetKeyUp(rightMovement) || Input.GetKeyUp(leftMovement))
            {
                this.inMovement = false;
            }

            if (Input.GetKeyDown(JumpMovement) && inFloor)
            {
                rigidbody.AddForce(Vector2.up * speedJump, ForceMode2D.Impulse);
                speedMovement = speedMovement / jampDivide;
                inFloor = false;
                //Debug.Log("Up");
            }
            else if (inFloor && !Input.GetKeyDown(JumpMovement))
            {
                speedMovement = auxSpeedMovement;
            }
            if (!jumping && !dashing && !Input.GetKey(leftMovement) && !Input.GetKey(rightMovement) && (!Input.GetKey(keyAttack) && !isAttacking) && inFloor && !Input.GetKey(dash))
            {
                animator.Play("Player_idle");
            }
        }
        else
        {
            if (Input.GetKeyDown(keyAccion))
            {
                //Debug.Log(timeDelayStune);
                timeDelayStune = timeDelayStune - substractTimeStune;
            }
        }
        
    }

    private void chekMovementForAnimation()
    {
        if(Input.GetKeyDown(JumpMovement) && inFloor)
        {
            this.jumping = true;
            animator.SetTrigger("Jump");
            animator.Play("Player_Jump");
        }

        if (Input.GetKeyDown(rightMovement) && !jumping)
        {
            this.inMovement = true;
        }
        else if (Input.GetKeyDown(leftMovement) && !jumping)
        {
            this.inMovement = true;
        }
        if (Input.GetKeyDown(keyAttack))
        {
            this.isAttacking = true;
        }
    }

    private void moveRight()
    {
        if (typeMovement == TypeMovement.Force)
        {
            direction = Direction.Right;
            rigidbody.AddForce(Vector2.right * speedMovement, ForceMode2D.Force);
            if (inFloor && !jumping && !isAttacking)
            {
                animator.Play("Player_Run");
            }
        }
        else if (typeMovement == TypeMovement.Position)
        {
            direction = Direction.Right;
            if (inFloor && !isAttacking)
            {
                RightMovement(false);
            }
            else if (!inFloor && isAttacking)
            {
                RightMovement(false);
            }
            else if (!inFloor && !isAttacking)
            {
                RightMovement(false);
            }
            else if (Input.GetKeyDown(JumpMovement))
            {
                this.jumping = true;
                animator.SetTrigger("Jump");
                animator.Play("Player_Jump");
            }
            if (inFloor && !jumping && !isAttacking)
            {
                animator.Play("Player_Run");
            }
        }
    }

    private void moveLeft()
    {
        if (typeMovement == TypeMovement.Force)
        {
            rigidbody.AddForce(Vector2.left * speedMovement, ForceMode2D.Force);
            direction = Direction.Left;
            if (inFloor && !jumping && !isAttacking)
            {
                animator.Play("Player_Run");
            }
        }
        else if (typeMovement == TypeMovement.Position)
        {
            if (inFloor && !isAttacking)
            {
                LeftMovement(false);
            }
            else if (!inFloor && isAttacking)
            {
                LeftMovement(false);
            }
            else if (!inFloor && !isAttacking)
            {
                LeftMovement(false);
            }
            else if (Input.GetKeyDown(JumpMovement))
            {
                this.jumping = true;
                animator.SetTrigger("Jump");
                animator.Play("Player_Jump");
            }
            direction = Direction.Left;
            if (inFloor && !jumping && !isAttacking)
            {
                animator.Play("Player_Run");
            }
        }
    }
    public void CheckAnimationMovement()
    {
        switch (stateMovement)
        {
            case StateMovement.QuickMovement:
                break;
            case StateMovement.NormalMovement:
                break;
            case StateMovement.SlowlyMovement:
                break;
        }
    }
    public void CheckSpeed()
    {
        if (life >= lifeQuickSpeed)
        {
            stateMovement = StateMovement.QuickMovement;
        }
        else if (life < lifeQuickSpeed && life <= lifeNormalSpeed && life > lifeSlowlySpeed)
        {
            stateMovement = StateMovement.NormalMovement;
        }
        else if (life <= lifeSlowlySpeed)
        {
            stateMovement = StateMovement.SlowlyMovement;
        }
        if (inFloor)
        {
            switch (stateMovement)
            {
                case StateMovement.QuickMovement:
                    speedMovement = QuickSpeed;
                    auxSpeedMovement = QuickSpeed;
                    break;
                case StateMovement.NormalMovement:
                    speedMovement = NormalSpeed;
                    auxSpeedMovement = NormalSpeed;
                    break;
                case StateMovement.SlowlyMovement:
                    speedMovement = SlowlySpeed;
                    auxSpeedMovement = SlowlySpeed;
                    break;
            }
        }
    }
    public void SetStateMovement(StateMovement _stateMovement)
    {
        stateMovement = _stateMovement;
    }
    public StateMovement GetStateMovement()
    {
        return stateMovement;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            inFloor = true;
            this.jumping = false;
        }
    }

    private void oncollisionenter2d(Collision2D collision)
    {
        /*if (collision.gameObject.tag == "Floor")
        {
            this.jumping = false;
        }*/

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            inFloor = false;
            this.topHigh = false;
        }
    }

    public void killPlayer()
    {
        die = true;
    }

    public void CheckItem()
    {
        if (currentItem != null && Input.GetKeyDown(keyAccion))
        {
            switch (currentItem.typeItem)
            {
                case Item.TypeItem.BrazoDeShoker:
                    
                    equipedWeapon = EquipedWeapon.BrazoDeShoker;
                    Destroy(currentItem.gameObject);
                    break;
                case Item.TypeItem.Default:
                    equipedWeapon = EquipedWeapon.Default;
                    Destroy(currentItem.gameObject);
                    break;
                case Item.TypeItem.RifleTracker:
                    equipedWeapon = EquipedWeapon.RifleTracker;
                    Destroy(currentItem.gameObject);
                    break;
                case Item.TypeItem.SableAssasin:
                    equipedWeapon = EquipedWeapon.SableAssasin;
                    Destroy(currentItem.gameObject);
                    break;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            currentItem = collision.GetComponent<Item>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            currentItem = null;
        }
    }
    public void update_life_bar()
    {
        healthBar.updateLife(this.life);
    }

    private void chekDashing()
    {
        if (dashing)
        {
            delayDash = delayDash - Time.deltaTime;
            if (delayDash <= 0)
            {
                this.rigidbody.velocity = Vector3.zero;
                this.rigidbody.angularVelocity = 0;
                dashing = false;
                //animator.Play("Player_idle");
                delayDash = auxDelayDash;
            }
        }
    }

    public bool isDashing()
    {
        return this.dashing;
    }

    private void updateAnimator()
    {
        /*animator.SetBool("jumping", this.jumping);
        animator.SetBool("dashing", this.dashing);
        animator.SetBool("onMovement",this.inMovement);
        animator.SetBool("topHigh", this.topHigh);
        animator.SetBool("onFloor", this.inFloor);
        */
        if (statePlayer.Equals(StatePlayer.Stunt))
        {
            animator.Play("Player_Stuned");
        }
        /*else
        {
            animator.SetBool("isStunned", false);
        }
        */
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump") && this.jumping || topHigh)
        {
            //this.jumping = false;
            this.topHigh = true;
            animator.Play("Player_OnAir");

        }
        if ((Input.GetKeyDown(keyAttack) || isAttacking) && statePlayer != StatePlayer.Stunt && inFloor)
        {
            animator.Play("Player_Atack");
            this.isAttacking = true;
        }
        /*else if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Atack"))
        {
            animator.Play("Player_idle");
          
        }*/
        
    }
    public void DisableIsAttack()
    {
        isAttacking = false;
    }
    public void DisableJumping()
    {
        jumping = false;
    }
    public void DisableDash()
    {
        dashing = false;
        animator.Play("Player_idle");
    }

}
