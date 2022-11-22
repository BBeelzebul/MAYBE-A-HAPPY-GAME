using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;

    public List<GameObject> enemyList = new List<GameObject>();
    public Vector3[] enemiesPosition;

    public float remainingTime;
    public int totalEnemies;
    private int minutes, seconds;

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
        remainingTime -= Time.deltaTime;

        if(remainingTime < 0) remainingTime = 0;

        minutes = (int)(remainingTime / 60f);
        seconds = (int)(remainingTime - minutes * 60);

        timeLeft.text = string.Format("{0:00}:{1:00}", minutes, seconds);


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

        for (int i = 0; i < 20; i++)
        {
            enemyList.Add(Instantiate(Enemy, enemiesPosition[i], Quaternion.identity));
        }

        totalEnemies = enemyList.Count;
        enemiesLeft.text = $"{totalEnemies}";
       
    }

    public void enemyCount()
    {
        totalEnemies--;
        enemiesLeft.text = $"{totalEnemies}";
    }

}
