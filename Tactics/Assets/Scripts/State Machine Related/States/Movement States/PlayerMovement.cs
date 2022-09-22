using UnityEngine;

public class PlayerMovement : EntityMovement
{
    public PlayerMovement(GameObject gameObject) : base(gameObject){}
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    // public Transform cam;
    public override void enter()
    {
        controller = gameObject.GetComponent<CharacterController>();
        groundCheck = gameObject.transform.GetChild(0);
        groundMask = LayerMask.GetMask("Ground");
    }
    public float speed = 6;
    // public float gravity = -9.81f;
    public float gravity = -30;
    public float jumpHeight = 2;
    Vector3 velocity;
    public bool isGrounded;

    public float groundDistance = 0.4f;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    // Update is called once per frame
    public override void Tick()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        //experiment increase gravity after peak of jump
        if (velocity.y < 0 && !isGrounded){
            velocity.y += gravity*Time.deltaTime*Time.deltaTime;
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; 
            // + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(gameObject.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            gameObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            character.unitAnimator.SetFloat("Speed", 1);
        }
        else{
            character.unitAnimator.SetFloat("Speed", 0);
        }
    }
}
