using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthPickup : MonoBehaviour
{
    public int healAmt;
    public float lifeTime;
    public float rotateTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Heal(healAmt);

            Destroy(gameObject);
        }
    }
}
