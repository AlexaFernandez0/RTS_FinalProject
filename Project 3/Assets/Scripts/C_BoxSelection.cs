using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BoxSelection : MonoBehaviour
{
    Camera mainCamera;
    private RectTransform BoxSelectionUI;
    private Rect BoxSelection;
    Vector2 PosicionA_BX;
    Vector2 PosicionB_BX;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
        PosicionA_BX = UnityEngine.Vector2.zero;
        PosicionB_BX = UnityEngine.Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
