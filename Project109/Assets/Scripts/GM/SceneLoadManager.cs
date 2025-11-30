using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance { get; private set; }

    [SerializeField] private Button activeButtonAfterLoadAsset;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //처음 시작할 때 버튼 비활성화
        activeButtonAfterLoadAsset.interactable = false;

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }

    //에셋 로드가 끝난 후에 버튼 활성화
    //임시로 만든 함수
    public void ActiveStartButton()
    {
        if(activeButtonAfterLoadAsset == null)
            return;

        activeButtonAfterLoadAsset.interactable = true;
    }
}
