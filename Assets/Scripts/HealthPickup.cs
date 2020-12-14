using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthPickup : MonoBehaviour
{
    public int healAmt;
    public float lifeTime;
    public float rotateTime;

    AudioManager audioManager;
    public GameObject clip;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Heal(healAmt);

            audioManager.SetAudioObj(clip);
            audioManager.PlayOneShotRandomPitch(14);

            Destroy(gameObject);
        }
    }
}
