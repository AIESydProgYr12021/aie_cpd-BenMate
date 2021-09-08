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

    public HealthBar healthBar;
    public Camera cam;
    public GameObject head;
    public GameObject winScreen;
    public ThumbStick leftStick;
    public ThumbStick rightStick;
    public GunShoot gs;

    Animator animator;
    Rigidbody rb;
    ZombieBehaviour zb;


    Vector3 worldMousePos = Vector3.zero;

    float timer = 0.0f;

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
        timer += Time.deltaTime;

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
        if (currentHealth > 0)
        {
            if (onAndroid)
            {
                localVel.x = -leftStick.Direction.x * movementSpeed;
                localVel.z = -leftStick.Direction.y * movementSpeed;

                //rotates player
                if (rightStick.Direction != Vector2.zero)
                {
                    transform.LookAt(transform.position + new Vector3(-rightStick.Direction.x, 0, -rightStick.Direction.y), Vector3.up);

                    //shoots
                    if (timer > 0.3f)
                    {
                        gs.Shoot();
                        timer = 0.0f;
                    }
                }
                //when you release stick 
                else
                {
                    timer = 0.0f;
                }
            }
            //not on android
            else
            {
                localVel.z = -Input.GetAxis("Vertical") * movementSpeed;
                localVel.x = -Input.GetAxis("Horizontal") * movementSpeed;
            }

            rb.velocity = localVel;
        }
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
