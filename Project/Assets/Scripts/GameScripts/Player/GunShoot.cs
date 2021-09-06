
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public float damage = 10.0f;
    
    public Transform firePoint;
    public GameObject bulletPreFab;
    public Camera cam;
    public float bulletForce = 20.0f;

    Vector3 worldMousePos = Vector3.zero;

    public bool onAndorid = false;


    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        onAndorid = true;
#endif
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
        if (onAndorid)
        {
            //todo: rotate and shoot using joystick.


        }
        //pc build
        else
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, int.MaxValue))
            {
                worldMousePos = hit.point;
            }

            if (Time.timeScale > 0)
            {
                firePoint.transform.LookAt(new Vector3(worldMousePos.x, firePoint.transform.position.y, worldMousePos.z));
            }
            //shots when you press mouse1
            if (Input.GetButtonDown("Fire1"))
            {
                if (Time.timeScale > 0)
                {
                    Shoot();
                }

            }


        }        
    }
}
