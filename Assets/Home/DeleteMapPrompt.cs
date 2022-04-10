using UnityEngine;
public class DeleteMapPrompt : MonoBehaviour
{
    [SerializeField] MapBrowser mapBrowser;
    [HideInInspector] public string current;
    public void Init(string name) => current = name;
    public void Delete()
    {
        if (current == null)
            return;
        mapBrowser.DeleteWorkspace(current);
    }
}
