using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsoleItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleField;
    [SerializeField] private TMP_Text _descriptonField;

    public void SetItem(string title, string description)
    {
        _titleField.text = title;
        _descriptonField.text = description;
    }
}
