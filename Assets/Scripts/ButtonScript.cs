using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void changescene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }
}
