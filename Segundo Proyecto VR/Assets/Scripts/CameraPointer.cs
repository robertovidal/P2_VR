using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    public float _maxDistance = 5;
    public float _maxDistanceCloser = 1;
    private GameObject _gazedAtObject = null;
    private GameObject _gazedAtObjectCloser = null;

    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        RaycastHit hitCloser;
        
        if (Physics.Raycast(transform.position, transform.forward, out hitCloser, _maxDistanceCloser))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObjectCloser != hitCloser.transform.gameObject)
            {
                _gazedAtObjectCloser?.SendMessage("OnPointerExitCloser", SendMessageOptions.DontRequireReceiver);
                _gazedAtObjectCloser = hitCloser.transform.gameObject;
                _gazedAtObjectCloser?.SendMessage("OnPointerEnterCloser", SendMessageOptions.DontRequireReceiver);
            }
            else{
                _gazedAtObjectCloser?.SendMessage("OnPointerEnterCloser", SendMessageOptions.DontRequireReceiver);
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                _gazedAtObject?.SendMessage("OnPointerExit", SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject?.SendMessage("OnPointerEnter", SendMessageOptions.DontRequireReceiver);
                UnityEngine.Debug.Log(_gazedAtObject.name);
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit", SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }
    }
}
