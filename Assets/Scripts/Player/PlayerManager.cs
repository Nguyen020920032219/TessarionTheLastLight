using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float hp = 50f;
    [SerializeField] private float mp = 50f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float defend = 5f;
    [SerializeField] private float strength = 5f;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image mpBar;
    [SerializeField] private float manaRegenRate = 1f;

    private GameObject player;
    private PlayerController playerController;
    private float maxHp;
    private float maxMp;
    private float currentHp;
    private float currentMp;
    private float currentSpeed;
    private float currentDefend;
    public float currentStrength;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();

            currentHp = hp;
            maxHp = hp;
            currentMp = mp;
            maxMp = mp;
            currentSpeed = speed;
            currentDefend = defend;
            currentStrength = strength;
            UpdateHpBar();
            UpdateMpBar();
            StartCoroutine(RegenerateMana());
        }
    }

    public void TakeDamage(float damage)
    {
        float realDamage = Mathf.Max(damage - damage * (currentDefend / 100), 1);
        currentHp -= realDamage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            playerController.Die();
        }
    }

    private void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    private void UpdateMpBar()
    {
        if (mpBar != null)
        {
            mpBar.fillAmount = currentMp / maxMp;
        }
    }

    public void ResetPlayerStats()
    {
        currentHp = hp;
        currentMp = mp;
        currentSpeed = speed;
        currentDefend = defend;
        currentStrength = strength;
        maxHp = hp;
        maxMp = mp;
    }

    public void ResetDefend()
    {
        currentDefend = defend;
    }

    public void AddDefend(float defendPercent)
    {
        currentDefend += defendPercent;
    }

    public void AddHealth(float healthPercent)
    {
        maxHp += maxHp * (healthPercent / 100);
        UpdateHpBar();
    }

    internal bool CanUseSkill(float manaUsing)
    {
        if (manaUsing <= currentMp)
        {
            currentMp -= manaUsing;
            UpdateMpBar();
            return true;
        }
        else
        {
            return false;
        }
    }

    internal float GetSpeed()
    {
        return currentSpeed;
    }

    internal void AddSpeed(float percentBuff)
    {
        currentSpeed += currentSpeed * (percentBuff / 100);
    }

    internal void AddStrength(float percentBuff)
    {
        currentStrength += percentBuff;
    }

    internal void AddMana(float percentBuff)
    {
        maxMp += maxMp * (percentBuff / 100);
        UpdateMpBar();
    }

    internal float GetStrenght()
    {
        return currentStrength;
    }

    internal float GetCurrentMana()
    {
        return currentMp;
    }

    internal float GetMaxMana()
    {
        return maxMp;
    }

    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            if (currentMp < maxMp)
            {
                currentMp += manaRegenRate;
                UpdateMpBar();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}