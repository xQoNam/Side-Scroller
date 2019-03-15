using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #pragma warning disable 0649

    private PlayerController controller;
    private Animator anim;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;
    void Start()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove =Input.GetAxisRaw("Horizontal") * runSpeed;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetBool("isJumping", true);
        }
        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
       
    }
    public void OnLanding()
    {
        anim.SetBool("isJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        anim.SetBool("isCrouching", isCrouching);
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, crouch, jump);
        jump = false;
    }
}
