using UnityEngine;
using TMPro;
public class AddMapPrompt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI input;
    [SerializeField] MapBrowser parent;
    public void Submit() => parent.NewWorkspace(input.text);
}