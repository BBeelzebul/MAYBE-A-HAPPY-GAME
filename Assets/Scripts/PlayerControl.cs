using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float velocityMovement = 10.0f;
    public Camera firstPersonCamera;

    public GameObject Bullet;
    public float bulletForce;
    public Transform spawnPosition;

    public bool viewCursor = true;
    public bool isGrounded;

    public float health;
    public float maxHealth;
    public float jump;

    private Rigidbody playerRb;

    public GameObject healthBarUI;

    public Slider slider;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();

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

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jump);
            isGrounded = false;
        }

        slider.value = CalculateHealth();

        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            health = health - 25;
        }
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
}
