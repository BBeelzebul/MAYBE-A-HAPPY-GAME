using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public Camera firstPersonCamera;

    public GameObject Bullet;
    public GameObject healthBarUI;
    public GameObject footstep;
    public GameObject gun;

    public Transform spawnPosition;

    public bool viewCursor = true;
    public bool isGrounded;

    public float health;
    public float maxHealth;
    public float jump;
    public float cdBullet = 0.25f;
    public float velocityMovement = 10.0f;
    public float bulletForce;

    private Rigidbody playerRb;

    public Slider slider;

    private Animator animGun;

    public AudioSource footstepAudioSource;
    public AudioSource gunAudioSource;

    private void Awake()
    {
        footstepAudioSource = GetComponentInChildren<AudioSource>();
        gunAudioSource = GetComponentInChildren<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        animGun = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();

        footstep.SetActive(false);

        if (viewCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            viewCursor = false;
        }       
    }


    void Update()
    {
        cdBullet -= Time.deltaTime;

        float movementX = Input.GetAxis("Vertical") * velocityMovement; 
        float movementZ = Input.GetAxis("Horizontal") * velocityMovement;

        movementX *= Time.deltaTime;
        movementZ *= Time.deltaTime;

        transform.Translate(movementZ, 0, movementX);

        if ((movementX != 0 || movementZ != 0) && isGrounded)
        {
            footstep.SetActive(true);
        }
        else
        {
            footstep.SetActive(false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jump);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            velocityMovement = velocityMovement * 1.5f;
            footstep.GetComponent<AudioSource>().pitch = 1.5f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            velocityMovement = 10f;
            footstep.GetComponent<AudioSource>().pitch = 1f;
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (viewCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                viewCursor = false;
            }

            

            BulletInstance();
        }

        if (Input.GetKeyDown("escape"))
        {
            if (!viewCursor)
            {
                Cursor.lockState = CursorLockMode.None;
                viewCursor = true;
            }
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

    private void BulletInstance()
    {
        
        if(cdBullet > 0)
        {
            return;
        }

        animGun.Play("recoil");

        gun.GetComponent<AudioSource>().Play();

        GameObject bulletClone = Instantiate(Bullet, spawnPosition.position, spawnPosition.rotation);

        Rigidbody rb = bulletClone.GetComponent<Rigidbody>();

        rb.AddRelativeForce(Vector3.up * bulletForce, ForceMode.Impulse);

        Destroy(bulletClone, 5);

        cdBullet = 0.25f;
       
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
