using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Animator))]

public class CharactorMovement : MonoBehaviour
{
    public int maxHealth = 28;
    public int currentHealth;
    public float movementSpeed = 1.0f;
    bool onAndroid = false;

    Animator animator;
    Rigidbody rb;
    ZombieBehaviour zb;

    public HealthBar healthBar;
    public Camera cam;
    public GameObject head;
    public GameObject winScreen;
    public ThumbStick thumbstick;

    Vector3 worldMousePos = Vector3.zero;

    void Start()
    {
#if UNITY_ANDROID
        onAndroid = true;
 #endif

        Time.timeScale = 1.0f; //starts the game off as 1 so the player can be paused.

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        WalkControls();
        LookControlsMouse();

        CharacterAnimations();
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

    public void TakeDamage(int damage)
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
            localVel.x = -thumbstick.Direction.x * movementSpeed;
            localVel.z = -thumbstick.Direction.y * movementSpeed;

            //rotates player
            if (thumbstick.Direction != Vector2.zero)        
                transform.LookAt(transform.position + new Vector3(-thumbstick.Direction.x, 0, -thumbstick.Direction.y), Vector3.up);         
        }
        else
        {
            localVel.z = -Input.GetAxis("Vertical") * movementSpeed;
            localVel.x = -Input.GetAxis("Horizontal") * movementSpeed;
        }
        rb.velocity = localVel;
    }

    void LookControlsMouse()
    {           
        if (!onAndroid)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << 3))
            {
                worldMousePos = hit.point;
                Debug.DrawLine(transform.position, worldMousePos, Color.red);
            }
                

            if (Time.timeScale > 0)
            {
                transform.LookAt(new Vector3(worldMousePos.x, transform.position.y, worldMousePos.z));
                head.transform.LookAt(worldMousePos);
            }
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, worldMousePos);
    }
}
