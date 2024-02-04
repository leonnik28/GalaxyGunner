using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConsoleUI : MonoBehaviour
{
    [SerializeField] private List<ConsoleItemUI> _actionItems = new List<ConsoleItemUI>();
    [SerializeField] private ConsoleItemUI _actionItemPrefab;
    [SerializeField] private Transform _listRoot;
    [SerializeField] private int _maxItems = 15;

    public void AddLog(string title, string description)
    {
        var item = Instantiate(_actionItemPrefab, _listRoot);
        item.SetItem(title, description);
        item.transform.SetSiblingIndex(0);

        _actionItems.Insert(0, item);

        if (_actionItems.Count > _maxItems )
        {
            var deleteItem = _actionItems.Last();
            _actionItems.Remove(deleteItem);

            Destroy(deleteItem.gameObject);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("ADD TEST")]
    private void AddTest()
    {
        AddLog("Test", "Test");
    }
#endif
}
