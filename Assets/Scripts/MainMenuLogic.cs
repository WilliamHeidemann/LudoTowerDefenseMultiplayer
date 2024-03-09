using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void Join()
    {
        NetworkManager.Singleton.StartClient();
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
