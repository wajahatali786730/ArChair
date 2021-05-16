using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ArTapToPlaceObjects : MonoBehaviour
{
    public GameObject gameObjectToInstantiate;
    private GameObject spawnOjbect;
    private ARRaycastManager _arRayCastManager;
    private Vector2 touchPosition;


    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRayCastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;

            return true;
        }

        else
            touchPosition = default;
        return false;
    }

   
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;
        ;
        if(_arRayCastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if(spawnOjbect==null)
            {
                spawnOjbect = Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnOjbect.transform.position = hitPose.position;
            }
        }

    }
}
