using UnityEngine;
using TMPro;
using SoulShard.Utils;

public class MapSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameText, _descriptionText;
    [HideInInspector]public string path, _name, description;
    [SerializeField] string _editScene, _viewScene, _currentScene;
    DeleteMapPrompt deletePrompt;
    public void Init(string name, string path, string description, DeleteMapPrompt deletePrompt)
    {
        _nameText.text = name;
        _descriptionText.text = description;
        this.path = path;
        _name = name;
        this.description = description;
        this.deletePrompt = deletePrompt;
    }
    public void Delete()
    {
        deletePrompt.Init(_name);
        deletePrompt.gameObject.SetActive(true);
    }
    public void OpenToEdit()
    {
        AdvancedSceneManager.Instance.Load(_editScene);
        AdvancedSceneManager.Instance.Unload(_currentScene);
        SharedData.Instance.data["SharedMap"] = _name;
    }
    public void OpenToView()
    {
        AdvancedSceneManager.Instance.Load(_viewScene);
        AdvancedSceneManager.Instance.Unload(_currentScene);
        SharedData.Instance.data["SharedMap"] = _name;
    }
}
