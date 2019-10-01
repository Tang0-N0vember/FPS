using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool alreadyWeaponEquipped=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (alreadyWeaponEquipped)
        {
            //Debug.Log(GameObject.Find("Weapon").GetComponentInChildren<Gun>().name);
        }
        if (GameObject.Find("Weapon").GetComponentInChildren<Gun>() != null)
        {
            alreadyWeaponEquipped = true;
        }
    }
}
