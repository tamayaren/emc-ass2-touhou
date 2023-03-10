using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectAutoRestart : MonoBehaviour
{
    [SerializeField] private GameObject TrackedGameObject;
    [SerializeField] private float GameBorder = -5f;

    void Update()
    {
        if (ReferenceEquals(TrackedGameObject, null)) return;
        Transform trackedTransform = TrackedGameObject.transform;

        if (trackedTransform.position.y <= GameBorder)
            SceneManager.LoadScene(0);
    }
}
