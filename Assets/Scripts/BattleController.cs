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
    public int playerMana;
    private int currentPlayerMaxMana;
    public int startingCardsAmount = 5;
    public int cardsToDrawPerTurn = 2;
    public enum TurnOrder { playerActive, playerCardAttacks, enemyActive, enemyCardAttacks }
    public TurnOrder currentPhase;
    void Start()
    {
        //playerMana = startingMana;
        //UICOntroller.instance.SetPlayerManaText(playerMana);
        currentPlayerMaxMana = startingMana;
        FillPlayerMana();
        DeckController.instance.DrawMultipleCards(startingCardsAmount);
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
                Debug.Log("Skipping player card attacks");
                AdvanceTurn();
                break;
            case TurnOrder.enemyActive:
                Debug.Log("Skipping enemy actions");
                AdvanceTurn();
                break;
            case TurnOrder.enemyCardAttacks:
                Debug.Log("Skipping enemy card attacks");
                AdvanceTurn();
                break;
        }
    }

    public void EndPlayerTurn()
    {
        UICOntroller.instance.endTurnButton.SetActive(false);
        UICOntroller.instance.drawCardButton.SetActive(false);
        AdvanceTurn();
    }

}
