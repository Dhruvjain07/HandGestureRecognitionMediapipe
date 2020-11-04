using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

    [RequireComponent(typeof(ARRaycastManager))]
    public class ARSpawn : MonoBehaviour
    {
        private ARPlaneManager arPlaneManager;
        private ARRaycastManager arRaycastManager;
        private GameObject spawnedObject;

        [SerializeField]
        GameObject PlacablePrefab, canvas;

        static List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
        private bool placed = false;

        // private bool isTrackableDetected = false;

        void Awake()
        {
            arPlaneManager = GetComponent<ARPlaneManager>();
            arRaycastManager = GetComponent<ARRaycastManager>();
        }

        bool tryGetTouchPosition(out Vector2 touchPosition)
        {
            if(Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
            touchPosition = default;
            return false;
        }

        void Update()
        {
            if(!placed)
            {
                if(!tryGetTouchPosition(out Vector2 touchPosition))
                {
                    return;
                }
                if (arRaycastManager.Raycast(touchPosition, hitResults, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = hitResults[0].pose;
                    spawnedObject = Instantiate(PlacablePrefab, hitPose.position, hitPose.rotation);
                    spawnedObject.transform.Rotate(0f,180f,0f);
                foreach (var plane in arPlaneManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                arPlaneManager.enabled = false;
                placed = true;
                canvas.SetActive(true);
                }
            }
        }
    }