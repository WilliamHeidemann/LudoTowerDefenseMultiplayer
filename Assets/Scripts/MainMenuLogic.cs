using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public async void Host()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
        await Awaitable.NextFrameAsync();
        NetworkManager.Singleton.StartHost();
    }

    public async void Join()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
        await Awaitable.NextFrameAsync();
        NetworkManager.Singleton.StartClient();
    }
}
