using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }
    [SerializeField] float lifeTime;

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
        }
        else if (collider.gameObject.tag == "R Enemy")
        {
            RangeEnemy rEnemy = collider.gameObject.GetComponent<RangeEnemy>();
            rEnemy.TakeDamage(Damage);
        }
        else if (collider.gameObject.tag == "Boss")
        {
            /*
            MeleeEnemy mEnemy = collision.gameObject.GetComponent<MeleeEnemy>();
            mEnemy.TakeDamage(Damage);
            */
        }
        else if (collider.gameObject.tag != "Bullet" && collider.gameObject.tag != "Bullet Spawn")
        {
            Destroy(gameObject);
        }
    }
}
