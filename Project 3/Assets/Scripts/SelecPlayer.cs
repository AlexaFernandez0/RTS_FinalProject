using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SelecPlayer : MonoBehaviour
{
    [SerializeField] RectTransform BoxSelectionUI;
    private Rect BoxSelection;
    public List<C_Soldado> SoldierList = new List<C_Soldado>();
    public List<C_Soldado> SoldierSelected = new List<C_Soldado>();

    RaycastHit hit;
    UnityEngine.Vector3 target;
    UnityEngine.Vector2 PosicionA_BX;
    UnityEngine.Vector2 PosicionB_BX;

    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
        PosicionA_BX = UnityEngine.Vector2.zero;
        PosicionB_BX = UnityEngine.Vector2.zero;
        DrawBox();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 MousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray= mainCamera.ScreenPointToRay(Input.mousePosition);
        UnityEngine.Vector3 mousePosicionDown;
        //UnityEngine.Vector3 mousePosicionUp;

        if(Input.GetMouseButton(0)){
            PosicionB_BX= Input.mousePosition;
            DrawBox();
            SelectionBox();
            if(Physics.Raycast(ray, out hit)){
                //Si seleccionamos un objeto
                if(Input.GetKey(KeyCode.LeftShift)){
                    ShiftSelection(hit.collider.GetComponent<C_Soldado>());
                } else{
                    if(hit.collider.gameObject.CompareTag("Soldado")){
                        AddSoldier(hit.collider.GetComponent<C_Soldado>());
                    } 
                }
                }
        }
        if(Input.GetMouseButton(1)){
            if(Physics.Raycast(ray, out hit)){
                if(hit.collider.gameObject.CompareTag("Piso")){
                    target = hit.point;
                    MoveAll();
                }
                }
        }
        if(Input.GetMouseButtonDown(0)){
            PosicionA_BX = Input.mousePosition;
            BoxSelection = new Rect();
        }
        if(Input.GetMouseButtonUp(0)){
            SelectSoldierBox();
            PosicionA_BX = UnityEngine.Vector2.zero;
            PosicionB_BX = UnityEngine.Vector2.zero;
            DrawBox();
        }

    }

    
    void MoveAll(){
        foreach (C_Soldado unidad in SoldierSelected)
        {
            if (unidad != null) 
            {
                unidad.Deselect();
                unidad.MoverPersonaje(target);
            }
        }
        DeseleccionarTodos();
        
    }

    void AddSoldier(C_Soldado Soldado)
    {
        DeseleccionarTodos();
        if(!SoldierSelected.Contains(Soldado)){
            Soldado.Select();
            SoldierSelected.Add(Soldado);
        }
    }

    void ShiftSelection (C_Soldado Soldado){
        if (!SoldierSelected.Contains(Soldado))
        {
            Soldado.Select();
            SoldierSelected.Add(Soldado);
        } else{
            Soldado.Deselect();
            SoldierSelected.Remove(Soldado);
        }
    }

    void SelectSoldierBox(){
        foreach(C_Soldado unidad in SoldierSelected){
            if(BoxSelection.Contains(mainCamera.WorldToScreenPoint(unidad.transform.position))){
               ShiftSelection(unidad);
            }
        }
    }
    void DeseleccionarTodos(){
        SoldierSelected.Clear();
    }
     void Deseleccionar(C_Soldado Soldado){
        SoldierSelected.Remove(Soldado);
    }


    void DrawBox(){
         UnityEngine.Vector2 boxStart= PosicionA_BX;
         UnityEngine.Vector2 boxEnd= PosicionB_BX;
         UnityEngine.Vector2 boxCenter= (PosicionA_BX + PosicionB_BX)/2;

        BoxSelectionUI.position = boxCenter;
        
        UnityEngine.Vector2 boxSize= new UnityEngine.Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        BoxSelectionUI.sizeDelta = boxSize;
    }

    void SelectionBox(){
        if(Input.mousePosition.x < PosicionA_BX.x){
            BoxSelection.xMin = Input.mousePosition.x;
            BoxSelection.xMax = PosicionA_BX.x;
        } else{
            BoxSelection.xMin = PosicionA_BX.x;
            BoxSelection.xMax = Input.mousePosition.x;
        }

        if(Input.mousePosition.y < PosicionA_BX.y){
            BoxSelection.xMin = Input.mousePosition.y;
            BoxSelection.xMax = PosicionA_BX.y;
        } else{
            BoxSelection.xMin = PosicionA_BX.y;
            BoxSelection.xMax = Input.mousePosition.y;
        }
    }
}
