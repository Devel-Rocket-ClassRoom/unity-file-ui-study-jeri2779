using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GenericWindow[] windows;

    public int currentWindowId;
    public int defaultWindowId;

    private void Awake()
    {
        OptionManager.LoadOptions();  //저장된 옵션 불러오기
        foreach (var window in windows)
        {
            if (window == null) continue;
            window.gameObject.SetActive(false);
            window.Init(this);
        }
        currentWindowId = defaultWindowId;
        windows[currentWindowId].Open();
    }
    public GenericWindow Open(int id)
    {
        windows[currentWindowId].Close();
        currentWindowId = id;
        windows[currentWindowId].Open();

        return windows[currentWindowId];
    }
    
}
//
