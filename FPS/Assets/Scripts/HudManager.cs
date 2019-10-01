using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public GameObject player;

    public Text amo;
    public Text weaponName;
    public Text playerHealth;
    public Text info;
    public Text noMoneyText;

    private int currentAmo;

    public Canvas bloodScreen;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        amo = GameObject.Find("Amo").GetComponent<Text>();
        weaponName = GameObject.Find("WeaponTyp").GetComponent<Text>();
        playerHealth = GameObject.Find("HP").GetComponent<Text>();
        info = GameObject.Find("Info").GetComponent<Text>();
        noMoneyText = GameObject.Find("MoneyInfo").GetComponent<Text>();
    }

    void Update()
    {
        playerHealth.text ="HP: "+player.GetComponent<Health>().currentHealth.ToString();
        if (player.GetComponent<Health>().playerDamaged)
        {
            bloodScreen.enabled = true;
        }
        else
        {
            bloodScreen.enabled = false;
        }
        if (player.GetComponent<PlayerMovement>().noMoney)
        {
            noMoneyText.text = "you dont have enough Money";
            StartCoroutine(Example());
        }
        else
        {
            noMoneyText.text = "";
        }
        if (player.GetComponent<PlayerMovement>().showInfo)
        {
            info.text = "press [E] to interact";
        }
        else
        {
            info.text = "";
        }
        if (player.GetComponent<Inventory>().alreadyWeaponEquipped)
        {
            weaponName.text = player.GetComponentInChildren<Gun>().name;
            amo.text = player.GetComponentInChildren<Gun>().currentAmo.ToString() + "/" + player.GetComponentInChildren<Gun>().maxWeaponAmo.ToString();
        }
        else
        {
            weaponName.text = "";
            amo.text = "";
        }
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerMovement>().noMoney = false;
    }
}
