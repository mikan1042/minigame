using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    #region Singleton
    private static SceneSwitcher instance = null;
    public static SceneSwitcher Instance
    {
        get
        {
            GetInstance();
            return instance;
        }
    }

    public static SceneSwitcher GetInstance()
    {
        if (instance == null)
        {
            instance = new SceneSwitcher();
        }
        return instance;

    }
    #endregion
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void SceneChange(int sceneNum) => SceneManager.LoadScene(sceneNum);
}
