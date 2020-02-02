using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characther
{
    // Start is called before the first frame update
    public float timeDelayStune;
    public float jampDivide;
    public float substractTimeStune;
    [Header("Controles Jugador")]
    public KeyCode JumpMovement;
    public KeyCode leftMovement;
    public KeyCode rightMovement;
    public KeyCode keyStune;
    public KeyCode keyAttack;
    public KeyCode dash;

    [Header("Velocidades del Jugador")]
    public float QuickSpeed; //Rapido.
    public float NormalSpeed;//Normal.
    public float SlowlySpeed;//Lento.
    public float dashSpeed;

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
    public float dashMultiplier;
    public double DashingInbulnerabilityTime;
    private int timeStartDashing;

    private StateMovement stateMovement;
    public enum EquipedWeapon
    {
        Default,
        BrazoDeShoker,
        RifleTracker,
        SableAssasin,
    }
    private enum Direction
    {
        Left,
        Right,
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
        auxLife = life;
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
        update_life_bar();
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
        chekDashing();
        CheckAnimationMovement();
        if (statePlayer != StatePlayer.Stunt)
        {
            CheckSpeed();
            CheckAnimationMovement();
            if (Input.GetKey(rightMovement))
            {
                if (typeMovement == TypeMovement.Force)
                {
                    rigidbody.AddForce(Vector2.right * speedMovement, ForceMode2D.Force);
                    direction = Direction.Right;

                }
                else if (typeMovement == TypeMovement.Position)
                {
                    RightMovement(false);
                    direction = Direction.Right;
                }
                //Debug.Log("right");
            }
            else if (Input.GetKey(leftMovement))
            {
                if (typeMovement == TypeMovement.Force)
                {
                    rigidbody.AddForce(Vector2.left * speedMovement, ForceMode2D.Force);
                    direction = Direction.Left;
                }
                else if (typeMovement == TypeMovement.Position)
                {
                    LeftMovement(false);
                    direction = Direction.Left;
                }
                //Debug.Log("left");
            }

            if (Input.GetKeyDown(JumpMovement) && inFloor)
            {
                rigidbody.AddForce(Vector2.up * speedJump, ForceMode2D.Impulse);
                speedMovement = speedMovement / jampDivide;
                inFloor = false;
                //Debug.Log("Up");
            }
            if (Input.GetKeyDown(dash) && inFloor)
            {

                timeStartDashing = System.DateTime.Now.Second;
                dashing = true;
                if (direction.Equals(Direction.Right))
                {
                    rigidbody.AddForce(Vector2.right * speedJump * dashMultiplier, ForceMode2D.Impulse);
                    speedMovement = speedMovement / jampDivide;
                }
                else
                {
                    rigidbody.AddForce(Vector2.left * speedJump * dashMultiplier, ForceMode2D.Impulse);
                    speedMovement = speedMovement / jampDivide;
                }
            }
            else if (inFloor && !Input.GetKeyDown(JumpMovement))
            {
                speedMovement = auxSpeedMovement;
            }
        }
        else
        {
            if (Input.GetKeyDown(keyStune))
            {
                //Debug.Log(timeDelayStune);
                timeDelayStune = timeDelayStune - substractTimeStune;
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
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            inFloor = false;
        }
    }

    public void killPlayer()
    {
        die = true;
    }

    private void checkDash()
    {
        if (Input.GetKey(dash))
        {
            speedMovement = dashSpeed;
        }

    }

    public void update_life_bar()
    {
        barDisplay = life / auxLife;
    }

    private void chekDashing()
    {
        if (dashing)
        {
            float timeDashing = System.DateTime.Now.Second - timeStartDashing;
            if (timeDashing > DashingInbulnerabilityTime)
            {
                this.rigidbody.velocity = Vector3.zero;
                this.rigidbody.angularVelocity = 0;
                dashing = false;
            }
        }
    }

    public bool isDashing()
    {
        return this.dashing;
    }


}