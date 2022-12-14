using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class InputCameraController : MonoBehaviour
{
    public InputModule ChooseModule;

    public GameObject CamHelper;

    public float CameraSoftRotation = 0.03f;
    private float size = 1;
    private float CuretSize = 1;

    public float SpeedSizing = 0.1f;
    public float MinSize = 0.5f;
    public float MaxSize = 100;
    public float SizeSoftSizing = 0.01f;
    
    public LayerMask gridLayer;
    public LayerMask ModuleLayer;
    public LayerMask SelectedModuleLayer;


    private Vector2 _mousePosition = new Vector2(0, 0);
    private bool _mouseIsDown = false;

    private Vector3 CameraAngle = new Vector3(0, 0, 0);
    private Vector3 _CameraCurentAngle = new Vector3(0, 0, 0);


    void Update()
    {


        if (ChooseModule != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, gridLayer))
            {
                var G = hit.collider.gameObject.transform.parent;
                ChooseModule.transform.position = ray.direction * hit.distance * 0.7f + transform.GetChild(0).position;
                ChooseModule.ConnectModule(G.GetComponent<InputGridElement>().position);
            }
            else
            {
                ChooseModule.gridPos.x = -1;
                ChooseModule.gridPos.y = -1;

                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit1;
                if (Physics.Raycast(ray1, out hit1, 100, ModuleLayer))
                    ChooseModule.transform.position = ray1.direction * hit1.distance * 0.7f + transform.GetChild(0).position;

                else
                    ChooseModule.transform.position = ray.direction * 9 + transform.GetChild(0).position;
            }

            if (Input.GetMouseButtonDown(0))
            {
                ChooseModule.transform.GetChild(0).gameObject.layer = (int)Mathf.Log(ModuleLayer.value,2);
                ChooseModule = null;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, ModuleLayer))
                {
                    ChooseModule = hit.collider.transform.parent.GetComponent<InputModule>();
                    ChooseModule.transform.GetChild(0).gameObject.layer = (int)Mathf.Log(SelectedModuleLayer.value, 2);
                }

            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            _mouseIsDown = true;
            _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(1))
        {
            _mouseIsDown = false;
        }
        if (_mouseIsDown)
        {
            Vector2 mouseDelta = _mousePosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            CameraAngle = new Vector3(mouseDelta.y / 10, -mouseDelta.x / 10, 0) + CameraAngle;
            CameraAngle.x = CameraAngle.x < -22 ? -22 : (CameraAngle.x > 44 ? 44 : CameraAngle.x);
            _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        var deltaCamAngle = (CameraAngle - _CameraCurentAngle) * CameraSoftRotation;
        _CameraCurentAngle += deltaCamAngle;
        CamHelper.transform.rotation = Quaternion.Euler(deltaCamAngle + _CameraCurentAngle);

        Camera.main.transform.rotation = CamHelper.transform.rotation * CamHelper.transform.GetChild(0).localRotation;
        Camera.main.transform.position = CamHelper.transform.GetChild(0).position;
        


        float mw = Input.GetAxis("Mouse ScrollWheel");
        size += mw * -10 * SpeedSizing * size;

        if (size < MinSize)
            size = MinSize;
        else if (size > MaxSize)
            size = MaxSize;
        CuretSize += (size - CuretSize) * SizeSoftSizing;
        CamHelper.transform.localScale = new Vector3(1, CuretSize, CuretSize);

    }

}
