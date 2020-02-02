using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuHandler : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("IntroDialogue");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}