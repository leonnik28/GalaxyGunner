using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameActions : MonoBehaviour
{
    [SerializeField] private GameSession _gameSession;
    [SerializeField] private UpdateGameLevel _updateGameLevel;
    [SerializeField] private Shop _shop;

    [SerializeField] private GameObject _actionObject;
    [SerializeField] private GameObject _actionList;

    private Credits _credits;

    private List<GameObject> _gameActionList;

    private void Awake()
    {
        _gameActionList = new List<GameObject>();
        _credits = _gameSession.GetComponent<Credits>();

        _gameSession.OnSuccessLogin += SuccessCreatedAccount;
        _credits.OnChangeCredits += CreditsChange;
        _updateGameLevel.OnChangeTopScore += TopScoreChange;
        _shop.OnGunBuy += NewGunBuy;
    }

    private void OnEnable()
    {
        if(_gameActionList.Count >= 9) {
            Destroy(_gameActionList[0].gameObject);
            _gameActionList.Remove(_gameActionList[0]);
        }
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

    private void TopScoreChange(int topScore)
    {
        string currentText = "New top score: " + topScore.ToString();
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
        _gameActionList.Add(actionObject);
    }
}
