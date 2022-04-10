using UnityEngine;
using TMPro;

public class MapSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameText, _descriptionText;
    [HideInInspector]public string path, _name, description;
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
}