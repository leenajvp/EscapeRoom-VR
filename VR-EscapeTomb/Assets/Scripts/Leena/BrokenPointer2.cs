using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BrokenPointer2 : MonoBehaviour
{
    public EventSystem eventSystem = null;
    public StandaloneInputModule inputModule = null;
    public float rayLenght;
    private LineRenderer lineRenderer = null;

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] RectTransform canvasRect;
    private Vector3 touchPos;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        UpdateLenght();
    }

    private void UpdateLenght()
    {
        lineRenderer.SetPosition(0,new Vector3(transform.position.x,transform.position.y, transform.position.z - 0.2f));
        lineRenderer.SetPosition(1, CalculateHitPoint());
    }


    private Vector3 CalculateHitPoint()
    {
        RaycastHit hit = Raycast();
        Vector3 endPos = CalculateEnd(rayLenght);


        if (hit.collider)
        {
            Buttons canvas = hit.collider.gameObject.GetComponent<Buttons>();
            if (canvas != null)
            {
                canvas.Hovered();
                Debug.Log("hover");
            }

            endPos = hit.point;
           // Debug.Log("other");
        }


        return endPos;
    }


    private RaycastHit Raycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);



        Physics.Raycast(ray, out hit, rayLenght);
        return hit;
    }

    private Vector3 CalculateEnd(float lenght)
    {
        return transform.position + (transform.forward * lenght);
    }
}
