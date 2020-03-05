using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Vector3 Direction = new Vector3(0, 0, 0);
    private SpriteRenderer ren;
    private Color col;
    public float speed = 0;
    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
    }

    public void AddSpeed(float add)
    {
        if (speed < 2.0f)
            speed += add;
    }

    public void SetDirection(Vector3 dir)
    {
        Direction = dir;
    }

    void Update()
    {
        ren.color = col;
        
        if (speed > 0)
        {
            transform.position = transform.position + Vector3.Normalize(Direction) * speed; // умножить на дельта тайм
            speed -= Time.deltaTime/10.0f;
        }
    }

    public void SetColor(int[] color)
    {
        col = new Color(color[0] / 255.0f, color[1] / 255.0f, color[2] / 255.0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("Border"))
        {
            Direction = -Direction;
        }
        else if (col.gameObject.transform.localScale.x > transform.localScale.x)
        {
            CrossSceneInfo.GameStatus = false;
            SceneManager.LoadScene("Lost");
        }
        else
        {
            col.gameObject.SendMessage("Remove");

            transform.localScale += col.gameObject.transform.localScale;
        }
    }
}
