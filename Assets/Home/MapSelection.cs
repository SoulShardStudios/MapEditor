using UnityEngine;
using TMPro;

public class MapSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameText, _descriptionText;
    [HideInInspector]public string path, name, description;
    public void Init(string name, string path, string description)
    {
        _nameText.text = name;
        _descriptionText.text = description;
        this.path = path;
        this.name = name;
        this.description = description;
    }
}