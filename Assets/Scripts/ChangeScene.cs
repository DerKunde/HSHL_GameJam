using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GetGameManager))]
public class ChangeScene : MonoBehaviour
{
    public SceneIndices sceneToLoad = SceneIndices.ExampleLevel;

    public void LoadScene()
    {
        StartCoroutine(SafeLoadScene());
    }

    IEnumerator SafeLoadScene()
    {
        GetGameManager manager = GetComponent<GetGameManager>();
        yield return new WaitUntil(() => manager.gameManager != null);
        manager.gameManager.LoadLevel(new List<int>() {(int) sceneToLoad});
    }
}
