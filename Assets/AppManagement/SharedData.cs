using UnityEngine;
using System.Collections.Generic;
public class SharedData : MonoBehaviour
{
    public Dictionary<string,object> data = new Dictionary<string,object>();
    public static SharedData Instance { get; private set; }
    private void OnEnable() => Instance = this;
}