using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    private bool canSlash = true, canShoot = true, canFireBall = true;
    public bool zapStatus;

    private string id;

    [SerializeField]
    private float slashDamage = 5f, zapDamage = 3f, attackRange = 0.5f;

    private Slashing weapon;
    private CloneSlash reap;
    private Splash zap;
    private Fire fire;
    private PlayerSounds sounds;

    [SerializeField]
    private Transform attackPoint, zapPoint, stablePoint, firePoint;

    [SerializeField]
    private LineRenderer zapLine;

    [SerializeField]
    private LayerMask enemyLayers;

    [SerializeField]
    private GameObject zapEffect, slashEffect, fireball, zapSwitchEffect, fireballSwitchEffect;


    void Awake()
    {
        slashEffect.SetActive(false);
        zapSwitchEffect.SetActive(false);
        fireballSwitchEffect.SetActive(false);
        zapLine.enabled = false;
        weapon = GetComponentInChildren<Slashing>();
        reap = GetComponentInChildren<CloneSlash>();
        zap = GetComponentInChildren<Splash>();
        fire = GetComponentInChildren<Fire>();
        sounds = GetComponent<PlayerSounds>();
        id = gameObject.tag;
    }

    private void OnFire()
    {
                        if (!PauseMenuController.isPaused)
        {

        if (canSlash)
        {
            slashEffect.SetActive(true);
            if (id == "Player")
            {
                sounds.PlayAudio("ps");
                weapon.Attack();
            }
            else
            {
                sounds.PlayAudio("cs");
            }

            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D obj in hits)
            {
                if (obj.transform.tag != "Collides")
                {
                    EnemyHealthSystem enemy = obj.gameObject.GetComponent<EnemyHealthSystem>();
                    enemy.TakeDamage(slashDamage);

                    if (id == "Clone")
                    {
                        reap.Attack(enemy.soulFrag);
                    }
                }
            }

            canSlash = false;
            StartCoroutine(DelayAttack(1));
        }
        }
    }

    private void OnRanged()
    {
                        if (!PauseMenuController.isPaused)
        {

        if (zapStatus && canShoot)
        {
            // track first target hit
            RaycastHit2D[] hits = Physics2D.LinecastAll(stablePoint.position, zapPoint.position + zapPoint.right * 20, enemyLayers);
            
            if (hits.Length != 0)
            {
                // aoe at where the target was hit
                foreach (RaycastHit2D obj in hits)
                {
                    if (obj.transform.tag != "Collides")
                    {
                        EnemyHealthSystem enemy = obj.transform.GetComponent<EnemyHealthSystem>();
                        enemy.TakeDamage(zapDamage);
                    }
                }

                // zap line
                zapLine.SetPosition(0, stablePoint.position);
                zapLine.SetPosition(1, hits[0].point);
                // zap mini explosion area
                Instantiate(zapEffect, hits[0].point, Quaternion.identity);
            }
            else
            {
                Debug.Log("Didn't hit");
                zapLine.SetPosition(0, stablePoint.position);
                zapLine.SetPosition(1, zapPoint.position + zapPoint.right * 12);
            }

            sounds.PlayAudio("z");
            zapLine.enabled = true;

            canShoot = false;
            StartCoroutine(DelayAttack(2));
        }

        else if (!zapStatus && canFireBall)
        {
            sounds.PlayAudio("fb");
            GameObject ball = Instantiate(fireball, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.AddForce(fire.fireDir * 3f, ForceMode2D.Impulse);
            canFireBall = false;
            StartCoroutine(DelayAttack(3));
        }
        }

    }

    private void OnSwitch()
    {
                        if (!PauseMenuController.isPaused)
        {

        Debug.Log("switching");
        if (zapStatus)
        {
            Debug.Log("switched to fire");
            fireballSwitchEffect.SetActive(true);
            sounds.PlayAudio("fbs");
            zapStatus = false;
            //play switch to fire power anim
        }
        else
        {
            Debug.Log("switched to light");
            zapSwitchEffect.SetActive(true);
            sounds.PlayAudio("zs");
            zapStatus = true;
            //play switch to lightning power anim
        }
        }
    }

    private IEnumerator DelayAttack(int type)
    {
        if (type == 1)
        {
            yield return new WaitForSeconds(0.5f);
            slashEffect.SetActive(false);
            canSlash = true;
        }
        else if (type == 2)
        {
            yield return new WaitForSeconds(0.3f);
            zapLine.enabled = false;
            yield return new WaitForSeconds(1f);
            canShoot = true;
        }
        else if (type == 3)
        {
            yield return new WaitForSeconds(1f);
            canFireBall = true;
        }
    }

    public void resetCD()
    {
        // tempory fix to ienumerator no running and thus make skills "stuck in cd"
        // when setting clone inactive
        canFireBall = true;
        canShoot = true;
        canSlash = true;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        
    }
}
