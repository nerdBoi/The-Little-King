using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPoint;
    public GameObject swordAttackPrefab;
    int currentWeapon = 0;
    bool canAttack = true;
    public PlayerMovement playerMovement;
    
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Attack") && canAttack)
        {
            if (currentWeapon == 0)
            {
                SwordSlash();
                playerMovement.animator.SetBool("didAttack", true);
                StartCoroutine(AnimDelay());
            }
            canAttack = false;
            StartCoroutine(AttackDelay());
        }
    }
    void SwordSlash()
    {
        Instantiate(swordAttackPrefab, attackPoint.position, attackPoint.rotation);
    }

    IEnumerator AnimDelay()
    {
        yield return new WaitForSeconds(.15f);
        playerMovement.animator.SetBool("didAttack", false);

    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(.5f);
        canAttack = true;
        
    }


}
