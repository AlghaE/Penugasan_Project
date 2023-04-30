using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MelleHandler : MonoBehaviour
{
    public Image CrossAir;
    public int AttackRange;
    public Vector2 UIoffset;


    private PlayerHandler Playerhandle;
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Playerhandle = GetComponent<PlayerHandler>();
        CrossAir.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        if (!Playerhandle.CanMove) return;
        AttackInput();

    }
    void AttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetAttack(1);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            SetAttack(2);
        }
    }
    void FindClosestEnemy()
    {
        float distancetoClosestEnemy = Mathf.Infinity;
        EnemyHandler closestEnemy = null;
        EnemyHandler[] allEnemies = GameObject.FindObjectsOfType<EnemyHandler>();
        foreach (EnemyHandler currentEnemeny in allEnemies)
        {
            float distancetoEnemy = (currentEnemeny.transform.position - this.transform.position).sqrMagnitude;
            if (distancetoEnemy < distancetoClosestEnemy)
            {
                distancetoClosestEnemy = distancetoEnemy;
                closestEnemy = currentEnemeny;
            }

        }
        if (distancetoClosestEnemy <= AttackRange)
        {
            CrossAir.gameObject.SetActive(true);
            CrossAir.transform.position = Camera.main.WorldToScreenPoint(closestEnemy.transform.position + (Vector3)UIoffset);
            //Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        }
        else
        {
            CrossAir.gameObject.SetActive(false);
        }



    }
    private void SetAttack(int attackType)
    {
        if (anim.GetBool("CanAttack"))
        {
            anim.SetTrigger("Attack");
            anim.SetInteger("AttackType", attackType);
        }
    }

}
