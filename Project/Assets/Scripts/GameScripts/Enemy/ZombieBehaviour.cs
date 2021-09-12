using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieBehaviour : MonoBehaviour
{
    public float lookRadius = 10.0f;
    public int maxHealth = 100;
    public int currentHealth;
    public float maxZombieSize;
    public int dealDamage = 4;
    public float timer = 0.6f;

    public Text text;
    public Transform zombieBody;
    public HealthBar healthBar;
    

    float damageTimer;

    Animator animator;
    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        animator = GetComponent<Animator>();
        animator.speed = maxZombieSize - zombieBody.transform.localScale.y;

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        damageTimer = timer;
    }

    void Update()
    {
        animator.SetBool("isDead", currentHealth < 1);

        WalkControls();
    }

    void DestroyZombie()
    {
        Score.instance.UpdateScore(Random.Range(1,6));

        //deletes zombie 
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {        
            TakeDamage(20);
        } 
    }

    void OnCollisionStay(Collision collision)
    {   
        if (collision.collider.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= timer && currentHealth > 0)
            {
                collision.collider.GetComponent<CharactorMovement>().TakeDamage(dealDamage);
                damageTimer = 0.0f;
            }           
        }       
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            damageTimer = timer;
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void WalkControls()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        animator.SetBool("isWalking", distance <= lookRadius && currentHealth > 1);

        if (currentHealth > 0 && distance <= lookRadius)
        {        
                agent.SetDestination(target.position);  
                
                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }            
        }    
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
        
        //todo: play an animations of attacks
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
