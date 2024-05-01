using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class C_Soldado : MonoBehaviour
{

    public Vector3 destination;
    [SerializeField] Color ColorNormal;

    [SerializeField] Color ColorSelect= Color.red;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        ColorNormal = GetComponent<MeshRenderer>().materials[0].color;
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    
    public void MoverPersonaje(Vector3 target){
        if (Vector3.Distance(destination, target) > 1.0f){
                    destination = target;
                    agent.destination = destination;
        }
        //Follow();
    }

    public void Select(){
        GetComponent<MeshRenderer>().materials[0].color = ColorSelect; //Cambiar el color del material del soldado
    }

    public void Deselect(){
        GetComponent<MeshRenderer>().materials[0].color = ColorNormal;
    }
    
    public void Follow(){
        CameraController.instance.followEsbirro = transform;
    }
    
}

