using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class ToolSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    bool _selected;
    [SerializeField] Color _selectColor, _unselectColor, _hoverColor;
    [SerializeField] Image _bgImage, _iconImage;
    public Action<string> onclick;
    [HideInInspector] public string toolName;
    void OnEnable() => _bgImage.color = _unselectColor;
    public void SetSelected(bool selected)
    {
        if (_selected == selected)
            return;
        _selected = selected;
        if (selected)
            _bgImage.color = _selectColor;
        else
            _bgImage.color = _unselectColor;
    }
    public void setIconImage(Sprite s) => _iconImage.sprite = s;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_selected)
            _bgImage.color = _hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_selected)
            _bgImage.color = _unselectColor;
    }

    public void OnPointerClick(PointerEventData eventData) => onclick?.Invoke(toolName);
}
