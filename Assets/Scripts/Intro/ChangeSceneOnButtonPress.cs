using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheRoom.Intro
{
    public class ChangeSceneOnButtonPress : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
