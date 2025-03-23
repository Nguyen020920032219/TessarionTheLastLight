using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float hp = 20f;
    [SerializeField] private float mp = 20f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float defend = 0f;
    [SerializeField] private float strength = 20f;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image mpBar;

    private GameObject player;
    private PlayerController playerController;
    private float currentHp;
    private float currentMp;
    private float currentSpeed;
    public float currentDefend;
    private float currentStrength;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();

            currentHp = hp;
            currentMp = mp;
            currentSpeed = speed;
            currentDefend = defend;
            currentStrength = strength;
            UpdateHpBar();
            UpdateMpBar();
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
            hpBar.fillAmount = currentHp / hp;
        }
    }

    private void UpdateMpBar()
    {
        if (mpBar != null)
        {
            mpBar.fillAmount = currentMp / mp;
        }
    }

    public void ResetHp()
    {
        currentHp = hp;
        currentMp = mp;
    }

    public void ResetDefend()
    {
        currentDefend = defend;
    }

    public void AddDefend(float defendPercent)
    {
        currentDefend += defendPercent;
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
            currentMp -= manaUsing;
            currentMp=Mathf.Max(currentMp, 0);
            UpdateMpBar();
            return false;
        }
    }
}