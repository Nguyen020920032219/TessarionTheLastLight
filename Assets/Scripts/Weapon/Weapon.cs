using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseSpeed;

    private string[] stones = new string[4];
    private int stoneCount = 0;
    private float nextAttackTime = 0f;
    public float currentDamage;
    private float currentSpeed;
    private GameObject player;
    private PlayerManager playerManager;
    protected Animator animator;
    public bool isHavingSkill = false;

    protected virtual void Awake()
    {
        animator = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerManager = player.GetComponent<PlayerManager>();
        }
        currentDamage = baseDamage;
        currentSpeed = baseSpeed;
    }

    public float GetDamage()
    {
        return (currentDamage + (currentDamage * (playerManager.GetStrenght() / 100)));
    }

    protected bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }

    protected void ResetAttackCooldown()
    {
        nextAttackTime = Time.time + (1f / baseSpeed);
    }

    public void AddStone(string stoneName)
    {
        if (stoneCount <= stones.Length)
        {
            stones[stoneCount] = stoneName;
            stoneCount++;
        }
    }

    public void ResetWeaponStat()
    {
        currentDamage = baseDamage;
        currentSpeed = baseSpeed;
    }

    public void UpdateWeaponStats()
    {
        playerManager.ResetDefend();
        ResetWeaponStat();

        if (stones[0] == "Astral")
        {
            isHavingSkill = true;
        }
        else if (stones[0] != null)
        {
            AddBuff(stones[0], 25);
        }

        for (int i = 1; i < stones.Length; i++)
        {
            if (stones[i] != null)
            {
                if (stones[i] == stones[i - 1])
                {
                    AddBuff(stones[(int)i], 15);
                }
                else
                {
                    AddBuff(stones[(int)i], 25);
                }
                if (i >= 2)
                {
                    if (stones[i] == stones[i - 2])
                    {
                        AddBuff(stones[(int)i], 15);
                    }
                    else
                    {
                        AddBuff(stones[(int)i], 25);
                    }
                }
            }
        }
    }

    private void AddBuff(string stoneName, float percentBuff)
    {
        switch (stoneName)
        {
            case "Ignis":
                Debug.Log("Damage Buff!!!!!");
                currentDamage += currentDamage * (percentBuff / 100);
                playerManager.AddSpeed(percentBuff);
                playerManager.AddStrength(percentBuff);
                break;
            case "Vitalis":
                Debug.Log("Speed Buff!!!!!");
                currentSpeed += currentSpeed * (percentBuff / 100);
                playerManager.AddMana(percentBuff);
                break;
            case "Aegis":
                Debug.Log("Defend Buff!!!!!");
                playerManager.AddDefend(percentBuff);
                playerManager.AddHealth(percentBuff);
                break;
        }
    }

    public abstract void Attack();
    public abstract void StopAttack();
    public abstract void UsingSkill();
}