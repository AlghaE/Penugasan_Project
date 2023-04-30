using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandler : MonoBehaviour
{
    public float radius;

    public float cooldownTime;
    private float nextFireTime;
    public List<Slash> Slashes;
    public NavMeshAgent Enemy;
    private PlayerHandler Player;
    public int HP;
    //Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerHandler>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy.SetDestination(Player.transform.position);
        if(Time.time > nextFireTime)
        {
            explode();
            
            nextFireTime = Time.time + cooldownTime;
        }
    }
    public void GetHit()
    {
        HP--;
        //anim.SetTrigger("OnHit");
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void explode()
    {
        StartCoroutine(SlashAttack());
        Collider[] collidery= Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < collidery.Length; i++)
        {
            if (collidery[i].CompareTag("Player"))
                Player.GetDamage();
        }
    }
    IEnumerator SlashAttack()
    {
        for (int i = 0; i < Slashes.Count; i++)
        {
            yield return new WaitForSeconds(Slashes[i].delay);
            Slashes[i].SlashObt.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        onDisableSlash();
    }
    void onDisableSlash()
    {
        for (int i = 0; i < Slashes.Count; i++)
        {
            Slashes[i].SlashObt.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    [System.Serializable]
    public class Slash
    {
        public GameObject SlashObt;
        public float delay;
    }
}
