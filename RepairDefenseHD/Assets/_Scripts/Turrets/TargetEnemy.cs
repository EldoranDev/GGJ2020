using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetEnemy : MonoBehaviour
{
    public static int DEATH_COUNT = 0;

    [SerializeField]
    public TMP_Text counterText;

    [SerializeField]
    GameObject ammunition;

    [SerializeField]
    float cooldownBetweenShots;

    GameObject loadedProjectile;
    Quaternion projectileRotation;
    Vector3 projectilePosition;
    float currentCooldown = 0;
    GameObject currentTarget;
    bool loaded = true;
    private void Awake()
    {
        currentCooldown = cooldownBetweenShots;
        loadedProjectile = GetComponentInChildren<Projectile>().gameObject;
        projectilePosition = loadedProjectile.transform.position;
        projectileRotation = loadedProjectile.transform.rotation;
    }
    private void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= cooldownBetweenShots/3 && !loaded)
        {
            loadedProjectile = Instantiate(ammunition, projectilePosition, projectileRotation, transform);
            loaded = true;
        }
        if (currentCooldown >= cooldownBetweenShots)
        {
            currentCooldown = 0;
            Shoot();
            loaded = false;
        }
    }
    private void Shoot()
    {
        Debug.Log(currentTarget.transform.position);
        loadedProjectile.GetComponent<Projectile>().Fire(currentTarget.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (currentTarget == null)
            {
                currentTarget = other.gameObject;
                currentTarget.GetComponent<Enemy>().deathEvent.AddListener(OnEnemyDeath);
            }
        }
    }
    void OnEnemyDeath()
    {
        DEATH_COUNT = 0;

        counterText.text = DEATH_COUNT.ToString();

        currentTarget.GetComponent<Enemy>().deathEvent.RemoveAllListeners();
        Collider[] allOverallpingColliders = Physics.OverlapSphere(GetComponent<SphereCollider>().center, GetComponent<SphereCollider>().radius);
        float bestDistance = 9999f;
        Collider bestEnemy = null;
        foreach (Collider col in allOverallpingColliders)
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestEnemy = col;
            }
        }
        currentTarget = bestEnemy.gameObject;
    }
}
