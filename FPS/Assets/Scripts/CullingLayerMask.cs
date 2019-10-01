using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingLayerMask : MonoBehaviour
{
    private GameObject player;

    public LayerMask maskHideWeapon;
    public LayerMask maskHideKnife;
    public LayerMask FPS;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Inventory>().alreadyWeaponEquipped)
        {
            if (player.GetComponentInChildren<Melee>().meleeUsed)
            {
                
                Camera.main.cullingMask = maskHideWeapon;
            }
            else
            {
                Camera.main.cullingMask = maskHideKnife;
            }
        }
        else
        {
            Camera.main.cullingMask = FPS;
        }
    }
}
