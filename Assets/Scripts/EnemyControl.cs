using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class EnemyControl : MonoBehaviour
{
    public int rutine;

    public float cronometro;
    public float grade;
    public float health;
    public float maxHealth;
    

    public bool attacking;

    public Quaternion angle;
    
    private GameObject player;
    public GameObject healthBarUI;

    public Slider slider;

    private Animator animEnemy;

    private Rigidbody enemyRb;

    private NavMeshAgent enemyAgent;

    private AudioSource enemyAudioSource;
    public AudioClip[] audioClips;


    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();

        enemyAudioSource = GetComponent<AudioSource>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyRb = GetComponentInChildren<Rigidbody>();
        animEnemy = GetComponent<Animator>();

        player = GameObject.Find("Player");
    }

   

    private void Update()
    {
        slider.value = CalculateHealth();

        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }


        ComportamientoEnemigo();
    }

    float CalculateHealth()
    {
        return health/maxHealth;
    }

    public void ComportamientoEnemigo()
    {
        //if(Vector3.Distance(transform.position, player.transform.position) > 50)
        //{
        //    cronometro += 1 * Time.deltaTime;
        //    if (cronometro >= 4)
        //    {
        //        rutine = Random.Range(0, 2);
        //        cronometro = 0;
        //    }
        //    switch (rutine)
        //    {
        //        case 0:
        //            animEnemy.SetBool("walking", false);                      
        //            break;

        //        case 1:
        //            grade = Random.Range(0, 360);
        //            angle = Quaternion.Euler(0, grade, 0);
        //            rutine++;
        //            break;

        //        case 2:
        //            transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
        //            transform.Translate(Vector3.forward * 1 * Time.deltaTime);
        //            animEnemy.SetBool("walking", true);
        //            break;

        //    }
        //}
        //else
        //{
        //    if(Vector3.Distance(transform.position, player.transform.position) > 5 && !attacking)
        //    {
                //var lookPos = player.transform.position - transform.position;
                //lookPos.y = 0;
                //var rotation = Quaternion.LookRotation(lookPos);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            enemyAgent.SetDestination(player.transform.position);
            animEnemy.SetBool("walking", true);
            animEnemy.SetBool("attack", false);
            //}
            //else
            if ( Vector3.Distance(transform.position, player.transform.position) <5 )
            {
                //enemyAudioSource.clip = audioClips[1];
                //enemyAudioSource.Play();
                animEnemy.SetBool("walking", false);
                animEnemy.SetBool("attack", true);
                attacking = true;
            }
         
        //}
       
    }

    public void EndAnimation()
    {
        animEnemy.SetBool("walking", true);
        animEnemy.SetBool("attack", false);
        attacking = false;
    }

    private void Step()
    {
        enemyAudioSource.clip = audioClips[0];
        enemyAudioSource.Play();
    }

    private void Death()
    {
        enemyAudioSource.clip = audioClips[2];
        enemyAudioSource.Play();
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
        GameManager.Instance.enemyCount();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            
            health = health - PlayerControl.Instance.damage;

            if(PlayerControl.Instance.damage == 100)
            {
                PlayerControl.Instance.damage = 25;
            }
            else
            {
                PlayerControl.Instance.UpdateProgress();
            }

            if (health <= 0)
            {
                enemyAgent.isStopped = true;
                healthBarUI.SetActive(false);
                animEnemy.SetTrigger("dying");
                Invoke("DestroyEnemy", 3f);
            }

            Destroy(collision.gameObject);
        }
    }
}
