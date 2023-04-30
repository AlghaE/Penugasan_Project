using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    public Timer _timer;
    public int HealthPoint;
    public int AttackRange;
    public Image HealthBar;

    public bool CanMove = true;

    public Transform Model;

    [Range(20f, 80f)]
    public float RotationSpeed = 20f;

    private Camera MainCamera;
    private Animator Anim;
    private Vector3 StickDirection;
    private float CurrentHealth;

    
    
    private bool CanRotate = true;
    private bool IsTargeting = false;
    // Start is called before the first frame update
    void Start()
    {
        
        CurrentHealth = HealthPoint;
        MainCamera = Camera.main;
        Anim = Model.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = CurrentHealth / HealthPoint;
        if (!CanMove) return;
        StickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        HandleInputData();
        if (IsTargeting) FindClosestEnemy();
        else HandleStandardLocomotionRotation();
    }
    private void HandleStandardLocomotionRotation()
    {
        if (!CanRotate) return;
        Vector3 rotationOffset = MainCamera.transform.TransformDirection(StickDirection);
        rotationOffset.y = 0;
        Model.forward += Vector3.Lerp(Model.forward, rotationOffset, Time.deltaTime * RotationSpeed);
    }
    private void HandleInputData()
    {
        Anim.SetFloat("Speed", Vector3.ClampMagnitude(StickDirection, 1).magnitude);
        CanRotate = Anim.GetBool("CanRotate");
        IsTargeting = Anim.GetBool("IsTargeting");
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
        if(distancetoClosestEnemy <= AttackRange)
        {
            if (!CanRotate) return;
            Vector3 rotationOffset = closestEnemy.transform.position - Model.position;
            rotationOffset.y = 0;
            Model.forward += Vector3.Lerp(Model.forward, rotationOffset, Time.deltaTime * RotationSpeed);
            //Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        }



    }
    public void GetDamage()
    {
        CurrentHealth--;
        //HealthBar.fillAmount = CurrentHealth / HealthPoint;
        if (CurrentHealth <= 0 && CanMove)
        {
            _timer.LosePlaye();
            Anim.SetTrigger("IsDead");
            CanMove = false;

        }
    }
}
