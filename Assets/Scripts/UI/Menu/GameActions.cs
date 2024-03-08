using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameActions : MonoBehaviour
{
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private Credits _credits;
    [SerializeField] private UpdateGameLevel _updateGameLevel;
    [SerializeField] private TopScore _topScore;
    [SerializeField] private Shop _shop;

    [SerializeField] private GameObject _actionObject;
    [SerializeField] private GameObject _actionList;

    private void Awake()
    {
        _gameSession.OnSuccessLogin += SuccessCreatedAccount;
        _credits.OnChangeCredits += CreditsChange;
        _updateGameLevel.OnChangeTopScore += TopScoreChange;
        _shop.OnGunBuy += NewGunBuy;
    }

    private void SuccessCreatedAccount()
    {
        string currentText = "Success Login!";
        CreateActionObject(currentText);
    }

    private void CreditsChange(int amount, string text)
    {
        string currentText = "Credits " + text + amount.ToString();
        CreateActionObject(currentText);
    }

    private void TopScoreChange()
    {
        string currentText = "New top score: " + _topScore.CurrentTopScore.ToString();
        CreateActionObject(currentText);
    }

    private void NewGunBuy(string gunName)
    {
        string currentText = "New weapon: " + gunName;
        CreateActionObject(currentText);
    }

    private void CreateActionObject(string currentText)
    {
        GameObject actionObject = Instantiate(_actionObject, _actionList.transform);
        TextMeshProUGUI textAction = actionObject.GetComponentInChildren<TextMeshProUGUI>();
        textAction.text = currentText;
    }
}
