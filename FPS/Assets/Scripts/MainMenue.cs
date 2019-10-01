using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenue : MonoBehaviour
{
    public void ChangeToScene(int sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
