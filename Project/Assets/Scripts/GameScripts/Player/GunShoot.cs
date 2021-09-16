
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public float damage = 10.0f;
    public float bulletForce = 20.0f;
   
    public Transform firePoint;
    public GameObject bulletPreFab;
    public Camera cam;

    bool onAndorid = false;

    void Start()
    {
#if UNITY_ANDROID
        onAndorid = true;
#endif
     
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LookAndShootControls(); 
        
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }

    void LookAndShootControls()
    {
        if (!onAndorid)
        {        
            //shots when you press mouse1
            if (Input.GetButtonDown("Fire1"))
            {
                if (Time.timeScale > 0)
                {
                    FindObjectOfType<AudioManager>().Play("ShootSound");
                    Shoot();
                }

            }
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(firePoint.position, firePoint.forward);
    }
}
