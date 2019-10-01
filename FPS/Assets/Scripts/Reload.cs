using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{

    private Animator fpsAnimator;
    //bool isReloading = false;
    // Start is called before the first frame update
    void Awake()
    {
        fpsAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //isReloading = GetComponent<Gun>().reload;
        //if (isReloading)
            fpsAnimator.SetTrigger("Reload");
    }
}