using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetModule : MonoBehaviour
{
    [SerializeField] int m_randomXCoordinateRange, m_randomYCoordinateRange, m_randomZCoordinateRange;


    // Update is called once per frame
    public void ReCoordinate()
    {
        this.gameObject.transform.position = 
            new Vector3(Random.Range(-this.m_randomXCoordinateRange, this.m_randomXCoordinateRange), 
                Random.Range(1, this.m_randomYCoordinateRange), 
                    Random.Range(-this.m_randomZCoordinateRange, this.m_randomZCoordinateRange));
    }
}
