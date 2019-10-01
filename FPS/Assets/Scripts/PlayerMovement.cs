using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;


    private Transform spine;

    public Animator tpsAnimator;

    public Interactable focus;

    private GameObject head;
    public GameObject attach;

    private Vector3 headOldPos;
    private Vector3 headCurrentPos;

    public float sensitivity = 100f;
    public float forwardMovemSpeed = 2f;
    public float backwardMoveSpeed = 1.5f;
    public float sidewaysMoveSpeed = 1.5f;
    public float sprintMoveSpeed = 2.5f;

    public bool showInfo = false;
    public bool noMoney = false;

    private float mouseY = 0;
    private float mouseX = 0;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        tpsAnimator = GetComponentInChildren<Animator>();
        spine = tpsAnimator.GetBoneTransform(HumanBodyBones.Spine);
        head = GameObject.Find("FPS");
        headOldPos = head.transform.localPosition;
        headCurrentPos = headOldPos;
        headCurrentPos.y = -0.5f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        var sprint = Input.GetButton("Sprint");
        spine.transform.Rotate(Vector3.right, mouseY);

        mouseX += Input.GetAxis("Mouse X") * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * Time.deltaTime;

        float deg = mouseY * Mathf.Rad2Deg;

        if (mouseY < -0.83f)
        {
            mouseY = -0.83f;
        }
        if (mouseY > 0.83f)
        {
            mouseY = 0.83f;
        }
        tpsAnimator.SetFloat("InputX", vertical);
        tpsAnimator.SetFloat("InputY", horizontal);
        transform.localRotation = Quaternion.AngleAxis(mouseX * sensitivity, Vector3.up);
        if (Input.GetButton("Crouch"))
        {
            tpsAnimator.SetBool("Crouch", true);
            head.transform.localPosition = Vector3.Slerp(head.transform.localPosition, headCurrentPos, 1.5f * Time.deltaTime);
        }
        else
        {
            tpsAnimator.SetBool("Crouch", false);
            head.transform.localPosition = Vector3.Slerp(head.transform.localPosition, headOldPos, 1.5f * Time.deltaTime);
        }

        if (vertical != 0)
        {
            if (vertical > 0)
            {
                float moveSpeedToUse = forwardMovemSpeed;
                if (sprint)
                {
                    tpsAnimator.SetBool("Run", true);

                    characterController.SimpleMove(transform.forward * moveSpeedToUse * vertical * 2.5f);
                }
                else
                {
                    tpsAnimator.SetBool("Run", false);
                    characterController.SimpleMove(transform.forward * moveSpeedToUse * vertical);
                }
            }
            else
            {
                float moveSpeedToUse = backwardMoveSpeed;
                tpsAnimator.SetBool("Run", false);
                characterController.SimpleMove(transform.forward * moveSpeedToUse * vertical);
            }
        }
        if (horizontal != 0)
        {
            characterController.SimpleMove(transform.right * horizontal);

        }

        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        Debug.DrawRay(ray.origin, ray.direction * 2, Color.blue, 2f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                showInfo = true;
                if (Input.GetKey("e"))
                {
                    SetFocus(interactable);
                    if (interactable.isAmoVendor)
                    {
                        Debug.Log("wann buy some stuff?");
                        if (GameObject.Find("GameManager").GetComponent<Gamemanger>().credits >= 200)
                        {
                            Debug.Log("you can buy stuff");
                            if (GetComponentInChildren<Inventory>().alreadyWeaponEquipped)
                            {
                                switch (GetComponentInChildren<Gun>().name)
                                {
                                    case "M4":
                                        GameObject.Find("GameManager").GetComponent<Gamemanger>().credits -= 300;
                                        GetComponentInChildren<Gun>().maxWeaponAmo += 30;
                                        break;
                                    case "AKM":
                                        GameObject.Find("GameManager").GetComponent<Gamemanger>().credits -= 200;
                                        GetComponentInChildren<Gun>().maxWeaponAmo += 30;
                                        break;
                                    case "Kar98":
                                        GameObject.Find("GameManager").GetComponent<Gamemanger>().credits -= 400;
                                        GetComponentInChildren<Gun>().maxWeaponAmo += 5;
                                        break;
                                }


                            }

                        }
                        else
                        {
                            noMoney = true;
                            //Debug.Log("you can NOT buy stuff!!!");
                        }

                    }
                    if (interactable.isWeapon)
                    {
                        if (GetComponent<Inventory>().alreadyWeaponEquipped)
                        {
                            Debug.Log("inventory full");
                        }
                        else
                        {
                            //interactable.GetComponent<Rigidbody>().useGravity = false;
                            interactable.transform.position = attach.transform.position;
                            interactable.transform.rotation = attach.transform.rotation;
                            interactable.transform.parent = attach.transform;
                            interactable.GetComponentInParent<Rigidbody>().isKinematic = true;
                            interactable.GetComponentInChildren<Gun>().isEquipped = true;
                        }
                    }
                }
                else
                {
                    RemoveFocus();
                }
            }
            else
            {
                showInfo = false;
            }


        }
        if (Input.GetKey("g"))
        {
            if (GetComponent<Inventory>().alreadyWeaponEquipped)
            {
                Debug.Log("weapon droped");
                //GameObject.Find("Weapon").GetComponentInChildren<Rigidbody>().useGravity = true;
                //GameObject.Find("Weapon").GetComponentInChildren<Rigidbody>().transform.parent = null;
                GameObject.Find("Weapon").GetComponentInChildren<Gun>().isEquipped = false;
                GameObject.Find("Weapon").GetComponentInChildren<Rigidbody>().isKinematic = false;
                GameObject.Find("Weapon").transform.DetachChildren();
                GetComponent<Inventory>().alreadyWeaponEquipped = false;
            }
        }
    }
    private void LateUpdate()
    {

        spine.transform.Rotate(Vector3.right, -mouseY * sensitivity);

    }
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDeFocused();
            }
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDeFocused();
        }
        focus = null;
    }
}