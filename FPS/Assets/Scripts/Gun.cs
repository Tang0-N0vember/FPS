using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Text amo;
    public Text weaponName;

    [SerializeField]
    [Range(0.1f, 1.5f)] private float fireRate = 0.5f;
    [SerializeField]
    [Range(1, 31)] private int maxWeaponCapacity = 31;
    public int maxWeaponAmo = 62;
    [SerializeField]
    [Range(1f, 10f)] private int damage = 1;
    public int currentAmo;
    public ParticleSystem muzzleParticleFPS;
    //public ParticleSystem muzzleParticleTPS;
    public AudioSource gunFireSource;

    public GameObject player;
    public GameObject impactEffect;
    //public GameObject bloodEffect;

    public Vector3 weaponAdsPos;

    private float timer=0;

    public new string name;

    public bool isEquipped=false;
    private bool fire;
    public bool reload;
    private Animator animator;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        amo = GameObject.Find("Amo").GetComponent<Text>();
        weaponName = GameObject.Find("WeaponTyp").GetComponent<Text>();
        animator = player.GetComponent<Animator>();
        currentAmo = 0;
    }
   private void Update()
    {
        if (isEquipped)
        {
            timer += Time.deltaTime;
            if (timer >= fireRate)
            {
                if (currentAmo > 0)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        currentAmo--;
                        timer = 0;
                        FireGun();
                    }
                }
                if (Input.GetKey("r") && currentAmo < maxWeaponCapacity && maxWeaponAmo>0)
                {
                    ReloadGun();
                }
            }
        }
    }

    private void ReloadGun()
    {
        reload = true;
        maxWeaponAmo = maxWeaponAmo - (maxWeaponCapacity-currentAmo);
        currentAmo = maxWeaponCapacity;
    }

    private void FireGun()
    {
        //animator.SetTrigger("Fire");
        muzzleParticleFPS.Play();
        //muzzleParticleTPS.Play();
        gunFireSource.Play();

        Ray ray =Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

        RaycastHit hitinfo;

        if (Physics.Raycast(ray, out hitinfo, 100))
        {
            var health=hitinfo.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                //GameObject bloodGO = Instantiate(bloodEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                //Destroy(bloodGO, 1);
            }
            else
            {
                GameObject impactGO = Instantiate(impactEffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(impactGO, 2);
            }
        }
    }
}