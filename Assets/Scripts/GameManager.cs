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
    private bool oleada1 = false;
    private bool oleada2 = false;
    private bool oleada3 = false;
    private bool dialogo2 = false;
    private bool dialogo3 = false;

    public TextMeshProUGUI timeLeft;
    public TextMeshProUGUI enemiesLeft;

    private AudioSource gmAudioSource;
    public AudioClip[] audioClips;
    
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
        gmAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        gameStart();
        gmAudioSource.clip = audioClips[0];
        gmAudioSource.Play();
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
        if(totalEnemies == 30)
        {
            SceneManager.LoadScene("Win");
        }

        if(remainingTime <= 170f && !oleada1)
        {
            oleada1 = true;
            for (int i = 0; i < 10; i++)
            {
                enemyList.Add(Instantiate(Enemy, enemiesPosition[i], Quaternion.identity));
            }
        }

        if(totalEnemies == 10 && !dialogo2)
        {
            dialogo2 = true;
            gmAudioSource.clip = audioClips[1];
            gmAudioSource.Play();
        }

        if (remainingTime <= 120f && !oleada2)
        {
            oleada2 = true;
            for (int i = 0; i < 10; i++)
            {
                enemyList.Add(Instantiate(Enemy, enemiesPosition[i], Quaternion.identity));
            }
        }

        if(totalEnemies == 20 && !dialogo3)
        {
            dialogo3 = true;
            gmAudioSource.clip = audioClips[2];
            gmAudioSource.Play();
        }

        if(remainingTime <= 70f && !oleada3)
        {
            oleada3 = true;
            for (int i = 0; i < 10; i++)
            {
                enemyList.Add(Instantiate(Enemy, enemiesPosition[i], Quaternion.identity));
            }
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

        totalEnemies = 0;
        enemiesLeft.text = $"{totalEnemies}/30";
       
    }

    public void enemyCount()
    {
        totalEnemies++;
        enemiesLeft.text = $"{totalEnemies}/30";
    }

}
