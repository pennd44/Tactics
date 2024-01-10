using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour
{
    // EntityMovement entityMovement;
    // PlayerMovement playerMovement;
    // AllyMovement allyMovement;
    // EnemyMovement enemyMovement;
    // private void Start(){
    //     if(GetComponent<Player>()){
    //         entityMovement = playerMovement;
    //     }
    //     else if(GetComponent<Ally>()){
    //         entityMovement = allyMovement;
    //     }
    //     else if(GetComponent<Enemy>()){
    //         entityMovement = enemyMovement;
    //     }
    // }
    // private void Update() {
    //     entityMovement.Tick();
    // }
    private Character character;
    private bool partyLeader;
    private bool ally;


    [Header("Ally Movement")]
    public Transform player;
    public NavMeshAgent agent;
       
    void Start () {
        character = GetComponent<Character>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if(GetComponent<Player>()){
           partyLeader = true;
        }
        else if(GetComponent<Ally>()){
           ally = true;
        }
    }
       




    [Header("Player Movement")]
    public CharacterController controller;
    // public Transform cam;

    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    // Update is called once per frame
    public void Update()
    {
        if(partyLeader){

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
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
                character.unitAnimator.SetFloat("Speed", 1);
            }else{
                character.unitAnimator.SetFloat("Speed", 0);
            }
    }
            else if(ally){
            agent.SetDestination(player.position);
            if(Vector3.Distance (transform.position, player.position)>3){
                agent.isStopped = false;
                character.unitAnimator.SetFloat("Speed", 1);
            }else{
                agent.isStopped = true;
                character.unitAnimator.SetFloat("Speed", 0);
            }
        }
    }
    public void exit(){
        character.unitAnimator.SetFloat("Speed", 0);
    }       
}