using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour
{
    GameObject rifle;

    public Canvas crosshair;

    public Vector3 oldPos;
    public Vector3 adsPos;
    public float aimSpeed = 5f;

    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (player.GetComponent<Inventory>().alreadyWeaponEquipped)
        {
            if (Input.GetButton("Fire2"))
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, player.GetComponentInChildren<Gun>().weaponAdsPos, aimSpeed * Time.deltaTime);
                crosshair.enabled = false;
            }
            else
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, oldPos, aimSpeed * Time.deltaTime);
                crosshair.enabled = true;
            }
        }
    }


}