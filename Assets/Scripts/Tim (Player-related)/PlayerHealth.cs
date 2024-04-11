using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool repaired = false;
    public static bool gameOver = false;
    private bool toTick = true ;
    public float hpLossPerSec;
    private int soulCount = 100;
    private int currentSoul = 0;
    private float maxHealth = 30f;
    private float currentHealth;
    private Animator animator;
    [SerializeField] private HealthBar hpBar;
    private PlayerSounds sounds;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sounds = GetComponent<PlayerSounds>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        hpBar.SetMaxHealth(maxHealth);
        hpBar.SetHealth(currentHealth);
        hpLossPerSec = 0.05f;
    }

    void FixedUpdate()
    {


        if (WaveSpawner.currentWave.waveName == "Final Boss") {
            repaired = true;
            }
        
        if (!repaired && toTick)
        {
            toTick = false;
            StartCoroutine(LossTick());
        }
    }

    public void TakeDamage(float dmg, bool hitPlayer=false)
    {
        currentHealth -= dmg;
        hpBar.SetHealth(currentHealth);

            // stop movement or anything else
            if (currentHealth <= 0)
            {
                gameOver = true;
            }
        if (hitPlayer)
        {
            sounds.PlayAudio("hurt");
            animator.SetTrigger("Hurt");
            
        }
    }
    
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        hpBar.SetHealth(currentHealth);
    }
    public void MaxIncrease(float amount)
    {
        maxHealth += amount;
        currentHealth += amount;

        hpBar.SetMaxHealth(maxHealth);
        hpBar.SetHealth(currentHealth);
    }

    public void soulRestore(int amount)
    {
        currentSoul += amount;

        if (currentSoul >= soulCount)
        {
            repaired = true;
        }
    }

    private IEnumerator LossTick()
    {
        TakeDamage(hpLossPerSec);
        yield return new WaitForSecondsRealtime(0.05f);
        toTick = true;
    }
}
