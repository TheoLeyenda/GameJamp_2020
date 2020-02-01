using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characther
{
    // Start is called before the first frame update

    [Header("Controles Jugador")]
    public KeyCode JumpMovement;
    public KeyCode leftMovement;
    public KeyCode rightMovement;

    [Header("Velocidades del Jugador")]
    public float QuickSpeed; //Rapido.
    public float NormalSpeed;//Normal.
    public float SlowlySpeed;//Lento.

    [Header("Relacion Vida/Velocidad")]
    public int lifeQuickSpeed;
    public int lifeNormalSpeed;
    public int lifeSlowlySpeed;

    public TypeMovement typeMovement;
    
    private StateMovement stateMovement;
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

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckDie();
    }
    public void CheckDie()
    {
        CheckDead();
        if (die)
        {
            gameObject.SetActive(false);
        }
    }
    public override void Movement()
    {
        //float vertical = Input.GetAxis("Vertical");
        //Debug.Log(speedMovement);
        CheckSpeed();
        CheckAnimationMovement();
        if (Input.GetKey(rightMovement))
        {
            if (typeMovement == TypeMovement.Force)
            {
                rig2D.AddForce(Vector2.right * speedMovement, ForceMode2D.Force);
            }
            else if (typeMovement == TypeMovement.Position)
            {
                transform.position = transform.position + new Vector3(speedMovement, 0, 0) * Time.deltaTime;
            }
            //Debug.Log("right");
        }
        else if (Input.GetKey(leftMovement))
        {
            if (typeMovement == TypeMovement.Force)
            {
                rig2D.AddForce(Vector2.left * speedMovement, ForceMode2D.Force);
            }
            else if (typeMovement == TypeMovement.Position)
            {
                transform.position = transform.position - new Vector3(speedMovement, 0, 0) * Time.deltaTime;
            }
            //Debug.Log("left");
        }

        if (Input.GetKeyDown(JumpMovement) && inFloor)
        {
            rig2D.AddForce(Vector2.up * speedJump, ForceMode2D.Impulse);
            speedMovement = speedMovement / 2f;
            inFloor = false;
            //Debug.Log("Up");
        }
        else if(inFloor && !Input.GetKeyDown(JumpMovement))
        {
            speedMovement = auxSpeedMovement;
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
    private void OnCollisionEnter2D(Collision2D collision)
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

}
