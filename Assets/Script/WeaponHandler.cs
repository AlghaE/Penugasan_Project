using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Transform Weapon;
    public Transform WeaponHandle;
    public Transform WeaponRestPos;

    public GameObject Splash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(Splash).transform.position = this.transform.position;
            other.gameObject.GetComponent<EnemyHandler>().GetHit();
        }
    }
}
