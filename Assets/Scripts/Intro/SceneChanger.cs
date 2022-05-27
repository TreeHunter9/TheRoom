using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheRoom.Intro
{
    public class SceneChanger : MonoBehaviour
    {
        public void ChangeScene(int indexScene)
        {
            SceneManager.LoadScene(indexScene);
        }
    }
}