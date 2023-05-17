using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    public class MousePointer : MonoBehaviour
    {
        private Vector2 mousePosition;
        [SerializeField] private Camera cam;
bool dis = false;
        // Update is called once per frame
        void Update()
        {
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            mousePosition = cam.transform.position;
            mousePosition.x -= (width / 1.5f);
            // mousePosition = cam.ScreenToWorldPoint(mousePosition);
            transform.position = mousePosition;
        }
    }

}