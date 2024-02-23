using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateChunk : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;
    [SerializeField] private Animator _animator;

    public void UpdateChunkObjects()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("UpdateAnimator");
        }

        foreach (var gameObject in _gameObjects)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(true);
                gameObject.TryGetComponent<FireTarget>(out FireTarget fireTarget);
                if (fireTarget != null)
                {
                    fireTarget.UpdateHealth();
                }
            }
        }
    }
}
