using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //sound effect
    SoundEffectsPLayer soundEffectsplayer;

    private void Awake()
    {
        soundEffectsplayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundEffectsPLayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 40 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            soundEffectsplayer.PlaySFX(soundEffectsplayer.collect);

            PlayerMotor.numOfCoins++;
            Destroy(gameObject);
        }
    }
}
