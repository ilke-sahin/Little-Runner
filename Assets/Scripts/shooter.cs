using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    public GameObject chooseshootingmaterial;
    public float bulletspeed = 15f;
    Animator animator;
    //sound effect
    SoundEffectsPLayer soundEffectsplayer;

    private void Awake()
    {
        soundEffectsplayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundEffectsPLayer>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
            soundEffectsplayer.PlaySFX(soundEffectsplayer.attack);
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Attack");
        Vector3 spawnPosition = transform.position + new Vector3(0, 0.4f, 0);
        Quaternion spawnRotation = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 0, 150);
        GameObject bulletvar = Instantiate(chooseshootingmaterial, spawnPosition,spawnRotation);
        Rigidbody rb = bulletvar.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = transform.forward * bulletspeed;

        Destroy(bulletvar, 2f);
    }
}
