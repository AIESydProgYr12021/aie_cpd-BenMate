
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

    public GameObject shootbutton;

    void Start()
    {
#if UNITY_ANDROID
        onAndorid = true;
#endif

        if (onAndorid) shootbutton.SetActive(true);
        
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
            //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, int.MaxValue))
            //{
            //    worldMousePos = hit.point;
            //}

            if (Time.timeScale > 0)
            {
                // firePoint.transform.LookAt(new Vector3(worldMousePos.x, firePoint.transform.position.y, worldMousePos.z));
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(firePoint.position, firePoint.forward);
    }
}
