using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;

    public List<GameObject> enemyList = new List<GameObject>();
    public Vector3[] enemiesPosition;

    float remainingTime;
    public int totalEnemies;

    public TextMeshProUGUI timeLeft;
    public TextMeshProUGUI enemiesLeft;

    
    private static GameManager _instance;
    public static GameManager Instance
    {
        get 
        {
            if(_instance == null)
            {
                Debug.LogError("Instance exists");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        gameStart();    
    }

    void Update()
    {
        if(remainingTime == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        if(totalEnemies == 0)
        {
            SceneManager.LoadScene("Win");
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

        totalEnemies = enemyList.Count;
        enemiesLeft.text = $"Enemies: {totalEnemies}";

        StartCoroutine(startTime(15));
       
    }

    public void enemyCount()
    {
        totalEnemies--;
        enemiesLeft.text = $"Enemies: {totalEnemies}";
    }


    public IEnumerator startTime(float timeValue = 15)
    {
        remainingTime = timeValue;
        while (remainingTime > 0)
        {
            timeLeft.text = $"Time: {remainingTime}";
            yield return new WaitForSeconds(1.0f);
            remainingTime--;           
        }
    }
}
