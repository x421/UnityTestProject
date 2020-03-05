using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txt;
    void Start()
    {
        if (CrossSceneInfo.GameStatus)
            txt.text = "Вы выиграли";
        else
            txt.text = "Вы проиграли";
    }
}
