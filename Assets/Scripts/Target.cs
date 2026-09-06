using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 2f;

    private float currentHealth;

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
            print("You got a target!");
            Destroy(gameObject);
        }
    }

}
