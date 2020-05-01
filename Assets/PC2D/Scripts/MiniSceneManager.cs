using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniSceneManager : MonoBehaviour
{
    public static MiniSceneManager _instance { get; private set; }

    private int _current_scene_idx = 0;
    private int _total_scene = 0;
    private bool _start_menu = true;

    private void OnGUI()
    {
        if (_start_menu)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 400, 40), "請按任意鍵進入遊戲，幫助忍者取得卷軸解開幻術返回村子吧。");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
        _total_scene = SceneManager.sceneCountInBuildSettings;
    }

    private void Update()
    {
        // 按下任何鍵，載入第一關
        if (Input.anyKey && _start_menu == true)
        {
            _start_menu = false;
            _current_scene_idx++;
            SceneManager.LoadScene(_current_scene_idx, LoadSceneMode.Single);
        }
    }

    public void CheckNextScene()
    {
        if (_current_scene_idx + 1 < _total_scene)
        {
            _current_scene_idx++;
            SceneManager.LoadScene(_current_scene_idx, LoadSceneMode.Single);
        }
        else
        {
            print("恭喜你破關了！");
        }
    }
}
