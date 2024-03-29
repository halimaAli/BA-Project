using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public bool playerDetected;
   

    [SerializeField] protected Transform player;
    [SerializeField] private float chaseSpeed = 4.0f;
    [SerializeField] private float returnSpeed = 15.0f;

    private float treshold;
    private float distance;
    private float attackDistance;
    private bool atStartPos;
    private bool isAttackAnimationComplete;
    private Vector3 startPosition;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool active;


    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        atStartPos = true;
        playerDetected = false;
        isAttackAnimationComplete = true;
        attackDistance = 2.0f;
        treshold = 0.4f;
        EnemyController controller = GetComponent<EnemyController>();
        controller.mushroom = this;
        active = true;
    }

    void Update()
    {
        if (!active)
        {
            return;
        }

        if (!playerDetected && isAttackAnimationComplete)
        {
            if (!atStartPos)
            {
                ReturnToStartPosition();
            }
            
            return;
        }
        else
        {
            distance = Vector3.Distance(player.position, transform.position);

            if (distance <= attackDistance)
            {
                AttackPlayer();
            } else
            {
                if (isAttackAnimationComplete)
                {
                    ChasePlayer();
                }
                
            }
        }     
    }

    private void ReturnToStartPosition()
    {
        spriteRenderer.flipX = false;
        transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);

        bool isCloseToStart = Vector3.Distance(transform.position, startPosition) < treshold; //due to float accuracy, I will just check if the gameObject is close enough to the startPos

        UpdateAnimatorState(isCloseToStart, !isCloseToStart, false);

        if (isCloseToStart)
        {
            spriteRenderer.flipX = true;
            atStartPos = true;
        }
    }


    private void ChasePlayer()
    {
        atStartPos = false;
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        spriteRenderer.flipX = true;
        UpdateAnimatorState(false, true, false);
    }

    private void AttackPlayer()
    {
        UpdateAnimatorState(false, false, true);
        isAttackAnimationComplete = false;
    }

    public void AttackAnimationEnded()
    {
        // Check if the player is still close after the attack animation
        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= attackDistance + treshold)
        {
            //PlayerController.instance.Die(false);
            PlayerController.instance.TakeDamage();
             playerDetected = false;
        }

        UpdateAnimatorState(true, false, false);
        isAttackAnimationComplete = true;
    }

    private void UpdateAnimatorState(bool isStanding, bool isWalking, bool isAttacking)
    {
        animator.SetBool("isStanding", isStanding);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isAttacking", isAttacking);
    }
}
