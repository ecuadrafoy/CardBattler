using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] SOCard cardSO;
    public int currentHealth;
    public int attackPower, manaCost;

    public TMP_Text healthText, attackText, manaText, nameText, actionDescriptionText, loreText;

    public Image characterImage, backgroundArt;
    void Start()
    {
        SetupCard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupCard()
    {
        currentHealth = cardSO.currentHealth;
        attackPower = cardSO.attackPower;
        manaCost = cardSO.manaCost;

        healthText.text = currentHealth.ToString();
        attackText.text = attackPower.ToString();
        manaText.text = manaCost.ToString();

        nameText.text = cardSO.cardName;
        actionDescriptionText.text = cardSO.actionDescription;
        loreText.text = cardSO.cardLore;

        characterImage.sprite = cardSO.characterSprite;
        backgroundArt.sprite = cardSO.backgroundSprite;
    }
}
