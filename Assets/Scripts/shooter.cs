using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject chooseshootingmaterial; // Mermi prefab'? için referans
    public float bulletspeed = 15f; // Mermi h?z?
    Animator animator;
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
        GameObject bulletvar = Instantiate(chooseshootingmaterial, spawnPosition,spawnRotation); // Mermiyi olu?tur
        Rigidbody rb = bulletvar.AddComponent<Rigidbody>(); // Rigidbody bile?eni ekle
        rb.useGravity = false; // Shootng material'? yerçekimi uygulanmas?n
        rb.velocity = transform.forward * bulletspeed; // Shootng material'? ileri do?ru hareket ettir

        // Shootng material'? belirli bir süre sonra yok etmek (e?er chooseabullet derseniz sorun oluyor dersten hat?rlarsan?z. Çünkü olu?turan? de?il, runtime esnas?nda olu?an? silece?iz.)
        Destroy(bulletvar, 2f); // 2 saniye sonra mermiyi yok et
    }
}
