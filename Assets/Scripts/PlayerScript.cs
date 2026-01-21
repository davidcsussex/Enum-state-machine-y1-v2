using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum States // used by all logic
{
    None,
    Idle,
    Walk,
    Jump,
};

public class PlayerScript : MonoBehaviour
{
    States state;


    Rigidbody rb;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        state = States.Idle;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        DoLogic();
    }

    private void LateUpdate()
    {
        grounded = false;

    }


    void DoLogic()
    {
        if( state == States.Idle )
        {
            PlayerIdle();
        }
        
        if( state == States.Jump )
        {
            PlayerJumping();
        }

        if( state == States.Walk )
        {
            PlayerWalk();
        }
    }


    void PlayerIdle()
    {
        if( Input.GetKeyDown(KeyCode.Space))
        {
            // simulate jump
            state = States.Jump;
            rb.linearVelocity = new Vector3( 0,10,0);
        }

        if( Input.GetKey("left"))
        {
            transform.Rotate( 0, 0.5f, 0, Space.Self );

        }
        if( Input.GetKey("right"))
        {
            transform.Rotate( 0,-0.5f, 0, Space.Self );
        }

        if( Input.GetKey("up"))
        {
            state = States.Walk;
        }

    }

    void PlayerJumping()
    {
        // player is jumping, check for hitting the ground
        if( grounded == true )
        {
            //player has landed on floor
            state = States.Idle;
        }
    }

    void PlayerWalk()
    {
        Vector3 vel;

        //magnitude = the player's speed
        float magnitude = rb.linearVelocity.magnitude;

        //move forward and preserve original y velocity
        vel = transform.forward * 5f;
        rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);
    }


    void OnCollisionEnter( Collision col )
    {
        if( col.gameObject.tag == "Floor")
        {
            grounded=true;
            print("landed!");
        }
    }


    //Output debug info to canvas
    private void OnGUI()
    {
        float mag = rb.linearVelocity.magnitude;

        mag = Mathf.Round(mag*100)/100;

        //debug text
        string text = "Left/Right arrows = Rotate\nSpace = Jump\nUp Arrow = Forward\nCurrent state=" + state;
        text += "\nmag=" + mag;

        // define debug text area
        GUILayout.BeginArea(new Rect(10f, 450f, 1600f, 1600f));
        GUILayout.Label($"<size=16>{text}</size>");
        GUILayout.EndArea();
    }
}
