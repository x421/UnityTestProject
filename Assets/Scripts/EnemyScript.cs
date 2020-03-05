using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool isDestroyed = false;
    private Vector3 direction;
    private float speed = 0.01f;
    private SpriteRenderer ren;
    private GameObject player;
    private Color colOne, colTwo;
    void Start()
    {
        direction.x = Random.Range(-0.3f, +0.3f);
        direction.y = Random.Range(-0.3f, +0.3f);
        direction.z = 0;

        ren = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        ren.color = Color.Lerp(colOne, colTwo, transform.localScale.x / player.transform.localScale.x);

        if (isDestroyed)
            Destroy(this.gameObject);
        transform.position = transform.position + direction * speed;
    }

    public void Remove()
    {
        isDestroyed = true;
    }

    public void SetColors(ColorSettings.EnemyColor colors)
    {
        colOne = new Color(colors.color1[0] / 255.0f, colors.color1[1] / 255.0f, colors.color1[2] / 255.0f);
        colTwo = new Color(colors.color2[0] / 255.0f, colors.color2[1] / 255.0f, colors.color2[2] / 255.0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("Border"))
        {
            direction = -direction;
        }
    }
}
