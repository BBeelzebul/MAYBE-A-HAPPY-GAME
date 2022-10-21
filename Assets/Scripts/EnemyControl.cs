using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private int hp;
    private GameObject player;
    public int velocity;

    void Start()
    {
        hp = 100;
        player = GameObject.Find("Player");
    }

    public void reciveDmg()
    {
        hp = hp - 25;
        if (hp < 0)
        {
            this.enemyDie();
        }
    }

    private void Update()
    {
        transform.LookAt(player.transform);
        transform.Translate(velocity * Vector3.forward * Time.deltaTime);
    }

    private void enemyDie()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            reciveDmg();
            Destroy(collision.gameObject);
        }
    }
}
