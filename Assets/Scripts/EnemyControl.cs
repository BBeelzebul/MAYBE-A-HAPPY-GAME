using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public int rutine;
    public int velocity;

    public float cronometro;
    public float grade;
    public float health;
    public float maxHealth;

    public Quaternion angle;

    private GameObject player;
    public GameObject healthBarUI;

    public Slider slider;

    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();

        player = GameObject.Find("Player");
    }

   

    private void Update()
    {
        slider.value = CalculateHealth();

        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (health <= 0)
        {
            GameManager.Instance.enemyCount();
            Destroy(gameObject);
        }

        ComportamientoEnemigo();
    }

    float CalculateHealth()
    {
        return health/maxHealth;
    }

    public void ComportamientoEnemigo()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > 50)
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutine = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutine)
            {
                case 0:
                    break;

                case 1:
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grade, 0);
                    rutine++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    break;

            }
        }
        else
        {
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            transform.Translate(Vector3.forward * 4 * Time.deltaTime);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            health = health - 25;
            Destroy(collision.gameObject);
        }
    }
}
