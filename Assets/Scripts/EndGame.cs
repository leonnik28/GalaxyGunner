using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private DeahtReliveUI _deahtReliveUI;
    [SerializeField] private float _radius;

    private bool _gameIsPlay = true;

    private void Update()
    {
        Ray ray = _camera.ViewportPointToRay(Vector2.one / 2);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, _radius) && _gameIsPlay)
        {
            if(hit.transform.TryGetComponent(out IWall wall))
            {
                _deahtReliveUI.GameOver();
                _gameIsPlay = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out IGameOverTrigger wall) && _gameIsPlay)
        {
            _deahtReliveUI.GameOver();
            _gameIsPlay = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out IGameOverTrigger wall) && _gameIsPlay)
        {
            _deahtReliveUI.GameOver();
            _gameIsPlay = false;
        }
    }
}
