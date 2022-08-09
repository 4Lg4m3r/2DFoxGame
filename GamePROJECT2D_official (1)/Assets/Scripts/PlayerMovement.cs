using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    public Transform GroundCheck;

    public LayerMask groundlayer;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool isgrounded; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isgrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.01f, groundlayer);

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            Debug.Log("IsJumping");
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (isgrounded)
        {
            OnLanding();
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        //Move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false; 
    }
}
