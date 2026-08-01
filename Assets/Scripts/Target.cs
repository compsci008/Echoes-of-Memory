using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 5f;

    private float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        currentHealth = maxHealth;

        print(currentHealth);
    }
    
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        print(gameObject);
        print(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
