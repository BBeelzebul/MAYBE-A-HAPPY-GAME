using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    private List<GameObject> enemyList = new List<GameObject>();
    float remainingTime;
    public Vector3[] enemiesPosition;



    void Start()
    {
        gameStart();    
    }

    void Update()
    {
        if(remainingTime == 0)
        {
            gameStart();
        }
    }

    void gameStart()
    {

        foreach (GameObject item in enemyList)
        {
            if(item != null)
            {
                Destroy(item);
            }         
        }


        enemyList.Add(Instantiate(Enemy, enemiesPosition[0], Quaternion.identity));
        enemyList.Add(Instantiate(Enemy, enemiesPosition[1], Quaternion.identity));
        enemyList.Add(Instantiate(Enemy, enemiesPosition[2], Quaternion.identity));

        StartCoroutine(startTime(30));
       
    }

    public IEnumerator startTime(float timeValue = 30)
    {
        remainingTime = timeValue;
        while (remainingTime > 0)
        {
            Debug.Log("Restan" + remainingTime + "segundos.");
            yield return new WaitForSeconds(1.0f);
            remainingTime--;
        }
    }
}
