using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int rifleAmt;
    public int shotgunAmt;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
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
            GunManager player = other.gameObject.GetComponent<GunManager>();
            player.GainAmmoOnBackend(rifleAmt, shotgunAmt);

            Destroy(gameObject);
        }
    }
}
