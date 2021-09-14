using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Animator))]

public class CharactorMovement : MonoBehaviour
{
    bool onAndroid = false;

    public int maxHealth = 28;
    public int currentHealth;
    public float movementSpeed = 1.0f;

    public bool rifle = false;

    public HealthBar healthBar;
    public Camera cam;
    public GameObject head;
    public GameObject winScreen;
    public GameObject oldScore;
    public ThumbStick leftStick;
    public ThumbStick rightStick;
    public GunShoot gs;
    

    Animator animator;
    Rigidbody rb;
  

    Vector3 worldMousePos = Vector3.zero;

    float timer = 0.0f;
    float gunTimer = 5.0f;

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

        if (currentHealth < 1 )
        {
            gs.enabled = false;
        }


        WalkControls();
        LookControlsMouse();
        CharacterAnimations();

        if (rifle == true)
        {
            if (Input.GetButton("Fire1"))
            {
                gs.Shoot();
            }

            gunTimer -= Time.deltaTime;

            if (gunTimer < 0)
            {
                rifle = false;
                gunTimer = 5.0f;
            }
            
        }
    }

    void PlayerDeath()
    {
        winScreen.SetActive(true);
        oldScore.SetActive(false);


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
        Vector3 movement = Vector3.zero;


        if (currentHealth > 0)
        {
            //moves player
            if (onAndroid)
            {
                movement.x = -leftStick.Direction.x;
                movement.z = -leftStick.Direction.y;

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
                movement.z = -Input.GetAxis("Vertical");
                movement.x = -Input.GetAxis("Horizontal");
            }

            localVel.x = movement.x * movementSpeed;
            localVel.z = movement.z * movementSpeed;
            
            rb.velocity = localVel;

            //Debug.Log(rb.velocity.magnitude);
        }
    }

    void LookControlsMouse()
    {
        if (!onAndroid && currentHealth > 0)
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

    void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("HealthPack") && currentHealth < maxHealth)
        {
            currentHealth += Random.Range(1,3) * 4;
            healthBar.SetHealth(currentHealth);

            Score.instance.UpdatePowerUpCount();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("GunPack"))
        {
            //do the power up

            
            rifle = true;

            Score.instance.UpdatePowerUpCount();
            Destroy(other.gameObject);
        }

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, worldMousePos);
    }
}
