using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventListener : MonoBehaviour
{
    public GameObject enemy;
    private Vector2 size;
    private bool isDown;
    private Vector3 direction;
    public GameObject playerBall;

    void Start()
    {
        var Settings = JsonConvert.DeserializeObject<ColorSettings>((File.ReadAllText(@"./cfg.json")));

        var vec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
        var fieldVec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        vec.x += 0.3f;
        vec.y = 0.0f;
        vec.z = 0.0f;
        fieldVec.z = 0;
        fieldVec *= 2;

        GameObject.Find("BorderWest").transform.position = vec;
        GameObject.Find("BorderEast").transform.position = -vec;
        GameObject.Find("GameField").transform.localScale = fieldVec;

        playerBall = GameObject.Find("Player");
        playerBall.SendMessage("SetColor", Settings.user.color);

        //Instantiate(playerBall, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        //playerBall.SendMessage("SetColor", Settings.user.color);
        
        size = new Vector2(transform.localScale.x, transform.localScale.y);
        
        for(int i = 0; i < Settings.enemySpawnSettings.count.Length; i++)
        {
            for (int j = 0; j < Settings.enemySpawnSettings.count[i];)
            {
                float radius = Settings.enemySpawnSettings.size[i];

                enemy.transform.localScale = new Vector3(radius, radius, 0.0f);

                Vector3 pos = new Vector3(Random.Range(
                    -size.x / 2 + radius,
                    size.x / 2 - radius
                    ), Random.Range(
                        -size.y / 2 + radius,
                        size.y / 2 - radius),
                        0);
                Collider2D[] res = Physics2D.OverlapCircleAll(pos, radius);
                if (!(res.Length > 1))
                {
                    var tmp = Instantiate(enemy, pos, Quaternion.identity);
                    tmp.SendMessage("SetColors", Settings.enemy);
                    j++;
                }
            }
        }
        
    }

    void Update()
    {
        if (isDown)
            playerBall.SendMessage("AddSpeed", Time.deltaTime);

        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Enemy");
        int cnt = 0;
        foreach(var obj in objects)
        {
            if (obj.transform.localScale.x > playerBall.transform.localScale.x)
            {
                cnt++;
            }
        }

        if (cnt == 0)
        {
            CrossSceneInfo.GameStatus = true;
            SceneManager.LoadScene("Lost");
        }
    }

    void OnMouseDown()
    {
        isDown = true;
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerBall.transform.position;
        direction.z = 0;
        playerBall.SendMessage("SetDirection", direction);
    }

    void OnMouseUp()
    {
        isDown = false;
    }
}
