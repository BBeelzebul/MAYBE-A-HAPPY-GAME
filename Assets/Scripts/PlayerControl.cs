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
    //public GameObject footstep;

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

    //public static AudioSource playerAudioSource;

    //public AudioClip footsteps;

    private void Awake()
    {
        //playerAudioSource = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        animGun = GetComponentInChildren<Animator>();
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
        cdBullet -= Time.deltaTime;

        float movementX = Input.GetAxis("Vertical") * velocityMovement; 
        float movementZ = Input.GetAxis("Horizontal") * velocityMovement;

        movementX *= Time.deltaTime;
        movementZ *= Time.deltaTime;

        transform.Translate(movementZ, 0, movementX);

        //if ((movementX != 0 || movementZ != 0) && isGrounded)
        //{
        //    playerAudioSource.Play(footsteps);
        //}
        //else
        //{
        //    playerAudioSource.Stop();
        //}

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jump);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            velocityMovement = velocityMovement * 1.5f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            velocityMovement = 10f;
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
