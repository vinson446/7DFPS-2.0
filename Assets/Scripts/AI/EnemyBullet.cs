using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }
    [SerializeField] float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Player player = FindObjectOfType<Player>();
        transform.LookAt(player.transform);
        transform.rotation *= Quaternion.Euler(90, 0, 0);
        */

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Player player = collider.gameObject.GetComponentInParent<Player>();
            player.TakeDamage(Damage);

            Destroy(gameObject);
        }
        else if (collider.gameObject.tag != "Bullet" && collider.gameObject.tag != "Bullet Spawn")
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
