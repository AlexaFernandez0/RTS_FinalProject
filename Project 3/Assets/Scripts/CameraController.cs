using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField] float velocidad;
    [SerializeField] float Tiempo;
    [SerializeField] float rotationAmount;
    private  Quaternion nuevaRotacion;
    private Vector3 StartRotation;
    private Vector3 FinalRotation;
    private Vector3 Nuevaposicion;
    private Vector3 RotacionActual;
    public Transform followEsbirro;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Nuevaposicion = transform.position;
        nuevaRotacion = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(followEsbirro != null){
            transform.position = followEsbirro.position;
        } else{
            ControladorMovimiento();
        }

        if(Input.GetKey(KeyCode.Escape)){
            followEsbirro = null;
        }
    }


    void ControladorMovimiento (){
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            Nuevaposicion += (transform.forward * velocidad);
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
            Nuevaposicion += (transform.forward * -velocidad);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            Nuevaposicion += (transform.right * -velocidad);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            Nuevaposicion += (transform.right * velocidad);
        }
        if(Input.GetMouseButtonDown(2)){
            StartRotation = Input.mousePosition;

        }
        if(Input.GetMouseButton(2)){
            RotacionActual = Input.mousePosition;

            Vector3 diferencia = StartRotation - RotacionActual;
            StartRotation = RotacionActual;

            nuevaRotacion *=  Quaternion.Euler(Vector3.up*(-diferencia.x/5));
        }

        transform.position = Vector3.Lerp(transform.position, Nuevaposicion, Time.deltaTime*Tiempo);
        transform.rotation = Quaternion.Lerp(transform.rotation, nuevaRotacion, Time.deltaTime*Tiempo);
    }
}
