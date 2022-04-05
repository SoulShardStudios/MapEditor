using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AddWorkspacePromt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI input;
    [SerializeField] SelectWorkspace parent;
    public void Submit() => parent.NewWorkspace(input.text);
}