using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene(1);
    }
}
