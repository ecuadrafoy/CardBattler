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

    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public float moveSpeed = 5f, rotateSpeed = 540f;
    public bool inHand;
    public int handPosition;

    private HandController handController;
    void Start()
    {
        SetupCard();
        handController = FindObjectOfType<HandController>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
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

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotationToMatch)
    {
        targetPoint = pointToMoveTo;
        targetRotation = rotationToMatch;
    }
    private void OnMouseOver()
    {
        if (inHand)
        {
            MoveToPoint(handController.cardPosition[handPosition] + new Vector3(0f, 1f, 0.5f), Quaternion.identity);
            Debug.Log("Mouse Detected");
        }
    }
    private void OnMouseExit()
    {
        if (inHand)
        {
            MoveToPoint(handController.cardPosition[handPosition], handController.minPosition.rotation);
        }
    }
}
