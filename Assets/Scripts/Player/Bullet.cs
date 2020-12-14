using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }
    [SerializeField] float lifeTime;

    public GameObject explosion;
    bool explodeOnce;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "M Enemy")
        {
            MeleeEnemy mEnemy = collider.gameObject.GetComponent<MeleeEnemy>();
            mEnemy.TakeDamage(Damage);

            if (!explodeOnce)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                explodeOnce = true;
            }

            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "R Enemy")
        {
            RangeEnemy rEnemy = collider.gameObject.GetComponent<RangeEnemy>();
            rEnemy.TakeDamage(Damage);

            if (!explodeOnce)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                explodeOnce = true;
            }

            Destroy(gameObject);
        }
        else if (collider.gameObject.tag != "Bullet" && collider.gameObject.tag != "Bullet Spawn" && collider.gameObject.tag != "Player" && collider.gameObject.tag != "Pickup")
        {
            Destroy(gameObject);
        }
    }
}
