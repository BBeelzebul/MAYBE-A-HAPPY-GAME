using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float velocityMovement = 10.0f;
    public Camera firstPersonCamera;

    public GameObject Bullet;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float movementX = Input.GetAxis("Vertical") * velocityMovement;
        float movementZ = Input.GetAxis("Horizontal") * velocityMovement;

        movementX *= Time.deltaTime;
        movementZ *= Time.deltaTime;

        transform.Translate(movementZ, 0, movementX);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = firstPersonCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            GameObject pro;
            pro = Instantiate(Bullet, ray.origin, transform.rotation);

            Rigidbody rb = pro.GetComponent<Rigidbody>();
            rb.AddForce(firstPersonCamera.transform.forward * 15, ForceMode.Impulse);

            Destroy(pro, 5);

            if((Physics.Raycast(ray, out hit) == true) && hit.distance < 5)
            {

                if(hit.collider.name.Substring(0, 3) == "Enemy")
                {
                    GameObject touchedObject = GameObject.Find(hit.transform.name);
                    EnemyControl scriptTouchedObject = (EnemyControl)touchedObject.GetComponent(typeof(EnemyControl));

                    if(scriptTouchedObject != null)
                    {
                        scriptTouchedObject.reciveDmg();
                    }

                }
            }
        }

    }
}
