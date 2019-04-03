using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerHandel : MonoBehaviour
{
    public System.Action action;
    private void OnMouseUpAsButton()
    {
        action?.Invoke();
    }
}
