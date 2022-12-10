using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class EnemyControl : MonoBehaviour
{
       
    public float health;
    public float maxHealth;
    
    public bool attacking;
    
    private GameObject player;
    public GameObject healthBarUI;

    public Slider slider;

    private Animator animEnemy;

    private NavMeshAgent enemyAgent;

    private AudioSource enemyAudioSource;
    public AudioClip[] audioClips;


    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();

        enemyAudioSource = GetComponent<AudioSource>();
        enemyAgent = GetComponent<NavMeshAgent>();
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

            enemyAgent.SetDestination(player.transform.position);
            animEnemy.SetBool("attack", false);

            if ( Vector3.Distance(transform.position, player.transform.position) <5 )
            {
                enemyAudioSource.clip = audioClips[1];
                enemyAudioSource.Play();
                animEnemy.SetBool("attack", true);
                attacking = true;
            }
       
    }

    public void EndAnimation()
    {
        animEnemy.SetBool("attack", false);
        attacking = false;
    }


    public void Death()
    {
        enemyAudioSource.clip = audioClips[2];
        enemyAudioSource.Play();
    }

    public void Step()
    {
        enemyAudioSource.clip = audioClips[0];
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
