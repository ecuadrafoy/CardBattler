using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    void Awake()
    {
        instance = this;
    }
    public int startingMana = 4, maxMana = 12;
    public int playerMana;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMana = startingMana;
        UICOntroller.instance.SetPlayerManaText(playerMana);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpendPlayerMana(int amountToSpend)
    {
        playerMana = playerMana - amountToSpend;
        if (playerMana < 0)
        {
            playerMana = 0;
        }
        UICOntroller.instance.SetPlayerManaText(playerMana);
    }
}
