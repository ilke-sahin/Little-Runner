using UnityEngine;

public class Coin : MonoBehaviour
{
    // sound effect
    SoundEffectsPLayer soundEffectsplayer;
    private CoinCounter coinCounter;

    private void Awake()
    {
        soundEffectsplayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundEffectsPLayer>();
        coinCounter = GameObject.FindObjectOfType<CoinCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 40 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            soundEffectsplayer.PlaySFX(soundEffectsplayer.collect);
            if (coinCounter != null)
            {
                coinCounter.AddCoin();
            }
            Destroy(gameObject);
        }
    }
}
