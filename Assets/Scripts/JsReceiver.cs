using TMPro;
using UnityEngine;

public class JsReceiver: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    public void MyMethod(string message)
    {
        textMeshPro.text = message;
    }
}
