using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private UpdateGameLevel _updateGameLevel;
    [SerializeField] private float _radius;

    private void Update()
    {
        Ray ray = _camera.ViewportPointToRay(Vector2.one / 2);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, _radius) && _updateGameLevel.IsGameActive)
        {
            if(hit.transform.TryGetComponent(out IWall wall))
            {
                _updateGameLevel.GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out IGameOverTrigger wall) && _updateGameLevel.IsGameActive)
        {
            _updateGameLevel.GameOver();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out IGameOverTrigger wall) && _updateGameLevel.IsGameActive)
        {
            _updateGameLevel.GameOver();
        }
    }
}
