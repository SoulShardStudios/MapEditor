using UnityEngine;
public class ToolManager : MonoBehaviour
{
    [SerializeField] ToolInfo[] tool_uis;
    [SerializeField] GameObject toolButtonPrefab;
    ToolSelectButton[] buttons;
    string _current;
    private void OnEnable()
    {
        buttons = new ToolSelectButton[tool_uis.Length];
        for (int i = 0; i < tool_uis.Length; i++)
        {
            GameObject g = Instantiate(toolButtonPrefab, transform);
            var button = g.GetComponent<ToolSelectButton>();
            buttons[i] = button;
            button.setIconImage(tool_uis[i].image);
            button.toolName = tool_uis[i].name;
            button.onclick = 
                (string s) => 
                {
                    setSelect(button, s);
                };
        }
    }

    void setSelect(ToolSelectButton b, string name)
    {
        if (_current == name)
        {
            _current = null;
            b.SetSelected(false);
        }
        else
        {
            DeselectAll();
            b.SetSelected(true);
            _current = name;
        }
    }

    void DeselectAll()
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].SetSelected(false);
    }
}

[System.Serializable]
struct ToolInfo
{
    public string name;
    public Sprite image;
}
