using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text Score;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Score.text ="You made it to Wave: "+ PlayerPrefs.GetInt("HighScore", 1).ToString(); 
    }
    
}
