using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate180deg : MonoBehaviour
{
    //ЭТО КОСТЫЛЬ!!!!!!!!!!!!!!!!!!!!!!!!

    private Vector3 startRotation;
    [SerializeField] private Transform compareTo;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        startRotation = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        if (compareTo)
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, startRotation.z + 180 * (Xcond() ? 0 : 1));
    }

    bool Xcond()
    { 
        Vector3 mpos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (mpos.x - compareTo.position.x > 0)
            return true;
        else
            return false;
    }
}
