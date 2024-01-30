using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _radius;

    private void Update()
    {
        Ray ray = _camera.ViewportPointToRay(Vector2.one / 2);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, _radius))
        {
            if(hit.transform.TryGetComponent(out IWall wall))
            {
                Time.timeScale = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out IGameOverTrigger wall))
        {
            Time.timeScale = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out IGameOverTrigger wall))
        {
            Time.timeScale = 0f;
        }
    }
}
