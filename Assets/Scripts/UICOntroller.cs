using UnityEngine;
using TMPro;

public class UICOntroller : MonoBehaviour
{
    public static UICOntroller instance;
    void Awake()
    {
        instance = this;
    }
    public TMP_Text playerManaText, playerHealthText, enemyHealthText, enemyManaText;
    public GameObject manaWarning;
    public float manaWarningTime;
    private float manaWarningCounter;
    public GameObject drawCardButton, endTurnButton;
    public UIDamageIndicator playerDamage, enemyDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (manaWarningCounter > 0)
        {
            manaWarningCounter -= Time.deltaTime;
            if (manaWarningCounter <= 0)
            {
                manaWarning.SetActive(false);
            }
        }
    }
    public void SetPlayerManaText(int manaAmount)
    {
        playerManaText.text = "Mana: " + manaAmount;
    }
    public void SetPlayerHealthText(int healthAmount)
    {
        playerHealthText.text = "Player Health: " + healthAmount;
    }
    public void SetEnemyHealthText(int healthAmount)
    {
        enemyHealthText.text = "Enemy Health: " + healthAmount;
    }
    public void ShowManaWarning()
    {
        manaWarning.SetActive(true);
        manaWarningCounter = manaWarningTime;
    }
    public void DrawCard()
    {
        DeckController.instance.DrawCardForMana();
    }
    public void EndPlayerTurn()
    {

        BattleController.instance.EndPlayerTurn();
    }
    public void SetEnemyManaText(int manaAmount)
    {
        enemyManaText.text = "Mana: " + manaAmount;
    }
}
