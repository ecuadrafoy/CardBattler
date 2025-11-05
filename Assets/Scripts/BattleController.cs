using Unity.VisualScripting;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    void Awake()
    {
        instance = this;
    }
    public int startingMana = 4, maxMana = 12;
    public int playerMana, enemyMana;
    private int currentPlayerMaxMana, currentEnemyMaxMana;
    public int startingCardsAmount = 5;
    public int cardsToDrawPerTurn = 2;
    public enum TurnOrder { playerActive, playerCardAttacks, enemyActive, enemyCardAttacks }
    public TurnOrder currentPhase;
    public Transform discardPoint;
    public int playerHealth;
    public int enemyHealth;
    void Start()
    {
        //playerMana = startingMana;
        //UICOntroller.instance.SetPlayerManaText(playerMana);
        currentPlayerMaxMana = startingMana;
        FillPlayerMana();
        DeckController.instance.DrawMultipleCards(startingCardsAmount);
        UICOntroller.instance.SetPlayerHealthText(playerHealth);
        UICOntroller.instance.SetEnemyHealthText(enemyHealth);
        currentEnemyMaxMana = startingMana;
        FillEnemyMana();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AdvanceTurn();
        }
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
    public void FillPlayerMana()
    {
        //playerMana = startingMana;
        playerMana = currentPlayerMaxMana;
        UICOntroller.instance.SetPlayerManaText(playerMana);
    }
    public void AdvanceTurn()
    {
        currentPhase++;
        if ((int)currentPhase >= System.Enum.GetValues(typeof(TurnOrder)).Length)
        {
            currentPhase = 0;
        }
        switch (currentPhase)
        {
            case TurnOrder.playerActive:
                UICOntroller.instance.endTurnButton.SetActive(true);
                UICOntroller.instance.drawCardButton.SetActive(true);
                if (currentPlayerMaxMana < maxMana)
                {
                    currentPlayerMaxMana++;
                }
                FillPlayerMana();
                DeckController.instance.DrawMultipleCards(cardsToDrawPerTurn);
                break;
            case TurnOrder.playerCardAttacks:
                //Debug.Log("Skipping player card attacks");
                //AdvanceTurn();
                CardPointsController.instance.PlayerAttack();
                break;
            case TurnOrder.enemyActive:
                //Debug.Log("Skipping enemy actions");
                //AdvanceTurn();
                if (currentEnemyMaxMana < maxMana)
                {
                    currentEnemyMaxMana++;
                }
                FillEnemyMana();
                EnemyController.instance.StartAction();
                break;
            case TurnOrder.enemyCardAttacks:
                //Debug.Log("Skipping enemy card attacks");
                //AdvanceTurn();
                CardPointsController.instance.EnemyAttack();
                break;
        }
    }

    public void EndPlayerTurn()
    {
        UICOntroller.instance.endTurnButton.SetActive(false);
        UICOntroller.instance.drawCardButton.SetActive(false);
        AdvanceTurn();
    }
    public void DamagePlayer(int damageAmount)
    {
        if (playerHealth > 0)
        {
            playerHealth -= damageAmount;
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                // End Battle
            }
        }
        UICOntroller.instance.SetPlayerHealthText(playerHealth);
        UIDamageIndicator damageClone = Instantiate(UICOntroller.instance.playerDamage, UICOntroller.instance.playerDamage.transform.parent);
        damageClone.damageText.text = damageAmount.ToString();
        damageClone.gameObject.SetActive(true);
    }
    public void DamageEnemy(int damageAmount)
    {
        if (enemyHealth > 0)
        {
            enemyHealth -= damageAmount;
            if (enemyHealth <= 0)
            {
                enemyHealth = 0;
                // End Battle
            }
            UICOntroller.instance.SetEnemyHealthText(enemyHealth);
            UIDamageIndicator damageClone = Instantiate(UICOntroller.instance.enemyDamage, UICOntroller.instance.enemyDamage.transform.parent);
            damageClone.damageText.text = damageAmount.ToString();
            damageClone.gameObject.SetActive(true);
        }

    }

    public void SpendEnemyMana(int amountToSpend)
    {
        enemyMana -= amountToSpend;
        if (enemyMana < 0)
        {
            enemyMana = 0;
        }
        UICOntroller.instance.SetEnemyManaText(enemyMana);
    }
    public void FillEnemyMana()
    {
        enemyMana = currentEnemyMaxMana;
        UICOntroller.instance.SetEnemyManaText(enemyMana);
    }

}
