using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//#if UNITY_ANDROID || UNITY_EDITOR
// Debug.log("ANDROID INIT");
//#endif

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Animator))]

public class CharactorMovement : MonoBehaviour
{
    public int maxHealth = 28;
    public int currentHealth;
    public float movementSpeed = 1.0f;

    Animator animator;
    Rigidbody rb;

    public HealthBar healthBar;
    public Camera cam;
    public GameObject head;
    public GameObject winScreen;

    Vector3 worldMousePos = Vector3.zero;



    public Joystick joystick;

#if UNITY_ANDROID || UNITY_EDITOR
    public bool onAndroid = true;
#endif

    void Start()
    {
        Time.timeScale = 1.0f; //starts the game off as 1 so the player can be paused.

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {


        WalkControls();
        LookControls();
        CharacterAnimations();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Zombie"))
        {
            TakeDamage(4);   //change from being hardcoded as 4
        }
    }

    void PlayerDeath()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void CharacterAnimations()
    {
        animator.SetBool("isWalking", rb.velocity.magnitude > 0.1f && currentHealth > 1);
        animator.SetBool("isDead", currentHealth < 1);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }

    void WalkControls()
    {
        Vector3 localVel = rb.velocity;

        if (onAndroid)
        {
            localVel.x = -joystick.Horizontal * movementSpeed;
            localVel.z = -joystick.Vertical * movementSpeed;
        }
        else
        {
            
            localVel.z = -Input.GetAxis("Vertical") * movementSpeed;
            localVel.x = -Input.GetAxis("Horizontal") * movementSpeed;
            
        }
        rb.velocity = localVel;
    }

    void LookControls()
    {
        if (onAndroid)
        {

        }
        else
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, int.MaxValue))
                worldMousePos = hit.point;

            if (Time.timeScale > 0)
            {
                transform.LookAt(new Vector3(worldMousePos.x, transform.position.y, worldMousePos.z));
                head.transform.LookAt(worldMousePos);
            }

        } 
    }
}
