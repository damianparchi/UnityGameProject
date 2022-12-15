using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform gracz;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - gracz.position;
    }

    // Update is called once per frame
    void Update()
    {
        //dla x i y zostawimy je takie same bo nie chcemy aby nasza kamera sie przesuwala razem z graczem
        Vector3 nowaPozycja = new Vector3(transform.position.x, transform.position.y, offset.z+gracz.position.z);
        transform.position = nowaPozycja;
    }
}
