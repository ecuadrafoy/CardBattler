using UnityEngine;
using UnityEngine.SceneManagement;
public class BattleSelectButton : MonoBehaviour
{
    public string levelToLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectBattle()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
