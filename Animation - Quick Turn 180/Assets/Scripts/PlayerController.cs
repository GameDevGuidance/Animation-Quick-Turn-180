using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    private bool is_crouching = false;
    public bool animation_busy = false;

    public Quaternion desired_rotation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        float move_amount = Mathf.Clamp01(Mathf.Abs(movement.x) + Mathf.Abs(movement.z));

        animator.SetFloat("MoveAmount", move_amount, .25f, Time.deltaTime);

        

        if (Input.GetKey(KeyCode.LeftShift) && move_amount > 0f)
        {
            animator.SetBool("IsRunning", true);
            is_crouching = false;
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (animation_busy) { return; }

        if (move_amount > 0f)
        {
            Vector3 cam = Camera.main.transform.forward;
            movement = Quaternion.LookRotation(new Vector3(cam.x, 0f, cam.z)) * movement;

            float turn_angle = Mathf.Abs(Vector3.SignedAngle(transform.forward, movement, Vector3.up));
            
            if(turn_angle >= 165 && is_crouching == false)
            {
                //animator.SetTrigger("Turn180");
                if(animator.GetBool("IsRunning"))
                {
                    animator.CrossFade("Sprint Turn 180", .25f);
                }
                else
                {
                    animator.CrossFade("Walk Turn 180", .15f);
                }

                Vector3 anim_rotation = animator.rootRotation.eulerAngles;
                desired_rotation = Quaternion.Euler(new Vector3(anim_rotation.x, anim_rotation.y + 180, anim_rotation.z));
            }

            Quaternion target_rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, 500 * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            is_crouching = !is_crouching;

            if(is_crouching == true)
            {
                animator.CrossFade("Locomotion_Crouch", .25f);
            }
            else
            {
                animator.CrossFade("Locomotion", .15f);
            }
        }

        
    }
}
