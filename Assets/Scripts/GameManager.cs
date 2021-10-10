using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneIndices
{
    Root = 0,
    MainMenu = 1,
    ExampleLevel = 2
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStateSO playerState;
    [SerializeField] private PlayerStateSO saveState;

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    private List<SceneIndices> loadedScenes = new List<SceneIndices>();
    public List<SceneIndices> LoadedScenes => loadedScenes;

    public GameObject Character;
    
    private void Awake()
    {
        loadedScenes.Add(SceneIndices.Root);
        LoadLevel(new List<int>(){(int) SceneIndices.MainMenu});

        
    }
    
    public void LoadLevel(List<int> loadLevels)
    {
        
            var unloadLevels = GetLoadedScenes();
            StartLoading(loadLevels, unloadLevels);

        
        
    }
    
    private void StartLoading(List<int> loadLevels, List<int> unloadLevels)
    {
        //Unload levels
        foreach (var unloadLevel in unloadLevels)
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(unloadLevel));
            var scene = (SceneIndices) Enum.GetValues(typeof(SceneIndices)).GetValue(unloadLevel);
            loadedScenes.Remove(scene);
        }
        
        //Load levels
        foreach (var loadLevel in loadLevels)
        {
            scenesLoading.Add(SceneManager.LoadSceneAsync(loadLevel, LoadSceneMode.Additive));
            var scene = (SceneIndices) Enum.GetValues(typeof(SceneIndices)).GetValue(loadLevel);
            loadedScenes.Add(scene);
        }
        
        StartCoroutine(WaitForLoadingToFinish());
    }
    
    private IEnumerator WaitForLoadingToFinish()
    {
        for (var i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        scenesLoading.Clear();
    }
    
    private List<int> GetLoadedScenes()
    {
        List<int> list = new List<int>();
        foreach (var scene in loadedScenes)
        {
            if(scene != SceneIndices.Root)
                list.Add((int) scene);
        }

        return list;
    }
    
    public static void PlayerDied()
    {
        //ToDo: Do whatever happens when Player dies
    }

    public void GetDamage()
    {
        int currHealth = playerState.GetCurrHealth();
        playerState.SetHealth(currHealth-1);
        
    }

    public bool negativeCoinEffected()
    {
        //Debug.Log("goes into method");
        if (playerState.negativeCoinEffected)
            return true;
        else return false;
        
    }

    public void getCoins(int coins)
    {
        int currCoins = playerState.GetCurrCoins();
        playerState.SetCoins(currCoins + coins);

        if (playerState.negativeCoinEffected)
        {
            Character = GameObject.Find("Character");
            Character.GetComponent<Movement>().coinDebuff(true);
        }
    }

    public void looseCoins(int coins)
    {
        int currCoins = playerState.GetCurrCoins();
        playerState.SetCoins(currCoins - coins);

        if (!playerState.negativeCoinEffected)
        {
            Character = GameObject.Find("Character");
            Character.GetComponent<Movement>().coinDebuff(false);
        }
    }
}
