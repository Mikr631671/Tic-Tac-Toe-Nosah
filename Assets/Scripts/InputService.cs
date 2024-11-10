using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    public event Action<Vector2> Click;

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Click?.Invoke(Input.mousePosition);
        }
    }
}
