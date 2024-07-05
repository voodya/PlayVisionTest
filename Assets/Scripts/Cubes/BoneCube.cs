using System.Collections.Generic;
using UnityEngine;

public class BoneCube : BaseCube
{
    [SerializeField] private LayerMask _mask;

    [SerializeField] private Transform _transform;

    private void OnValidate()
    {
        if (_transform == null)
            _transform = transform;
    }

    public int FindSideVector()
    {
        Dictionary<int, Vector3> points = new Dictionary<int, Vector3>();
        points.Add(1, _transform.position + _transform.right);
        points.Add(2, _transform.position - _transform.right);
        points.Add(3, _transform.position + _transform.forward);
        points.Add(4, _transform.position - _transform.forward);
        points.Add(5, _transform.position + _transform.up);
        points.Add(6, _transform.position - _transform.up);

        Vector3 upDirect = Vector3.up + _transform.position;

        float minDist = 1000f;
        int targetValue = 1;
        foreach (var point in points)
        {
            float dist = Vector3.Distance(point.Value, upDirect);
            if (dist < minDist)
            {
                minDist = dist;
                targetValue = point.Key;
            }
        }

        return targetValue;
    }

    public string FindSideRay()
    {
        if( Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 2, _mask))
        {
             if(hit.rigidbody.TryGetComponent(out SideTrigger side))
             {
                 return side.SideName;
             }
             return "imposible";
        }
         return "imposible";
    }
}
