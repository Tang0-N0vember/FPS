using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanger : MonoBehaviour
{
    public Text currentCredits;
    public Text currentWave;

    public List<GameObject> allEnemys;
    public GameObject prefebEnemy;

    public GameObject player;

    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;
    public GameObject SpawnPoint4;
    public GameObject SpawnPoint5;
    public GameObject SpawnPoint6;
    public GameObject SpawnPoint7;
    public GameObject SpawnPoint8;
    public GameObject SpawnPoint9;

    public int credits = 0;

    private int waveCounter=0;
    private int lastWave=-1;
    public int enemeyCounter=0;
    private int enemeysToSpawn=0;
    private int maxRandom=2;

    public GameObject Gate1;
    public GameObject Gate2;

    private bool gate1open=false;
    private bool gate2open=false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentCredits = GameObject.Find("Credits").GetComponent<Text>();
        currentWave = GameObject.Find("Wave").GetComponent<Text>();
        
}

    void Update()
    {
        if (player.GetComponent<Health>().currentHealth<=0)
        {
            PlayerPrefs.SetInt("HighScore", waveCounter);
            SceneManager.LoadScene(2);
        }
        currentCredits.text ="$ "+ credits.ToString();
        currentWave.text = "Wave: " + waveCounter.ToString();
        if (waveCounter>lastWave) {
            if (enemeyCounter <1)
            {
                lastWave = waveCounter;
                waveCounter++;
                enemeysToSpawn++;
                SpawnEnemys();
            }
        }
        if (waveCounter > 2)
        {
            if (!gate1open)
            {
                maxRandom+=2;
                gate1open = true;
            }
            Destroy(Gate1);
        }
        if (waveCounter > 4)
        {
            if (!gate2open)
            {
                maxRandom+=2;
                gate2open = true;
            }
            Destroy(Gate2);
        }
    }
    void SpawnEnemys()
    {
        for (int i = 0; i < enemeysToSpawn; i++)
        {
            int randomInt = Random.Range(0, maxRandom);
            switch(randomInt)
            {
                case 0:
                    Instantiate(prefebEnemy, SpawnPoint1.transform.position, Quaternion.identity);
                    enemeyCounter++;
                    break;
                case 1:
                    Instantiate(prefebEnemy, SpawnPoint2.transform.position, Quaternion.identity);
                    enemeyCounter++;
                    break;
                case 2:
                    Instantiate(prefebEnemy, SpawnPoint3.transform.position, Quaternion.identity);
                    enemeyCounter++;
                    break;
                case 3:
                    Instantiate(prefebEnemy, SpawnPoint4.transform.position, Quaternion.identity);
                    enemeyCounter++;
                    break;
                case 4:
                    Instantiate(prefebEnemy, SpawnPoint5.transform.position, Quaternion.identity);
                    enemeyCounter++;
                    break;
                case 5:
                    Instantiate(prefebEnemy, SpawnPoint6.transform.position, Quaternion.identity);
                    enemeyCounter++;
                    break;
                case 6:
                    Instantiate(prefebEnemy, SpawnPoint7.transform.position, Quaternion.identity);
                    enemeyCounter++;
                    break;
                case 7:
                    Instantiate(prefebEnemy, SpawnPoint8.transform.position, Quaternion.identity);
                    enemeyCounter++;
                    break;
                case 8:
                    Instantiate(prefebEnemy, SpawnPoint9.transform.position, Quaternion.identity);
                    enemeyCounter++;
                    break;
            }
            Debug.Log(randomInt);
        }
    }
}
