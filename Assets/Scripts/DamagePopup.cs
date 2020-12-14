using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    // public int damage;
    TextMeshPro textMesh;

    public float disappearTime;
    Color textColor;

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void SetDamage(int damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        disappearTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float moveYSpeed = 3;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTime -= Time.deltaTime;
        if (disappearTime < 0)
        {
            float disappearSpeed = 3;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
                Destroy(gameObject);
        }
    }
}
