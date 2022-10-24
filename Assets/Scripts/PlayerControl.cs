using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float velocityMovement = 10.0f;
    public Camera firstPersonCamera;

    public GameObject Bullet;
    public float bulletForce;
    public Transform spawnPosition;

    public bool viewCursor = true;


    void Start()
    {
        if (viewCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            viewCursor = false;
        }       
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
            if (!viewCursor)
            {
                Cursor.lockState = CursorLockMode.None;
                viewCursor = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (viewCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                viewCursor = false;
            }

            GameObject bulletClone = Instantiate(Bullet, spawnPosition.position, spawnPosition.rotation);

            Rigidbody rb = bulletClone.GetComponent<Rigidbody>();

            rb.AddRelativeForce(Vector3.up * bulletForce, ForceMode.Impulse);

            Destroy(bulletClone, 5);
        }

    }
}
