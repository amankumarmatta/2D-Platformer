﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool isDead = false;

    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberofflashes;
    [SerializeField] private SpriteRenderer spriteRenderer;
    

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            isDead = false;
            anim.SetTrigger("Hurt");
            StartCoroutine("Invulnerability");
        }
        else
        {
            isDead = true;
            anim.SetTrigger("Dead");
            GetComponent<Movement>().enabled = false;
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < numberofflashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberofflashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberofflashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
