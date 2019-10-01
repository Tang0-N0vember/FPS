using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    [Range(1f, 2f)] public int damage = 1;

    public Animator animator;

    public bool meleeUsed=false;

    void Update()
    {

        if (Input.GetKey("v"))
        {
            meleeUsed = true;
            animator.SetTrigger("Melee");
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

            Debug.DrawRay(ray.origin, ray.direction * 1.5f, Color.red, 2f);

            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo, 1.5f))
            {
                var health = hitinfo.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }

            }
            
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("melee"))
        {
            StartCoroutine(Example());
        }

    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(0.6f);
        meleeUsed = false;
    }

}