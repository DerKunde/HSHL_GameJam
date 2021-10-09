using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetGameManager : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake()
    {
        StartCoroutine(SafeAwake());
    }

    IEnumerator SafeAwake()
    {
        var scene = SceneManager.GetSceneAt((int) SceneIndices.Root);
        yield return new WaitUntil(() => scene.isLoaded);
        
        var gameController = scene.GetRootGameObjects()[0];
        gameManager = gameController.GetComponent<GameManager>();
    }
}
