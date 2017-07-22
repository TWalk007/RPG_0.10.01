using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable,
        Layer.RaycastEndStop
    };
    
    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit racastHit;
    public RaycastHit hit
    {
        get { return racastHit; }
    }

    Layer layerHit;
    public Layer currentLayerHit
    {
        get { return layerHit; }
    }

    void Start()
    {
        viewCamera = Camera.main;
    }


    // NOTES FROM LECTURE
    // Ben says that this is "caching" and storing that data which can cause problems.
    // BUT, if we are aware of that we will be okay.

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                racastHit = hit.Value;
                layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit
        racastHit.distance = distanceToBackground;
        layerHit = Layer.RaycastEndStop;
    }


    // The "?" is a "nullable parameter".  In other words if you deleted the "?" it would not let you return null.
    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation

        //Given the mouse postion do a screen point to ray on the camera.  Use the camera to turn the screen point into a ray via the mouse.
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
