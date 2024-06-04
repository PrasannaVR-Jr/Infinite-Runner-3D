using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLooper : MonoBehaviour
{
    public LoopObjects[] loopObjects;
    public float speed=4;
    void Update()
    {
        
        for(int i = 0; i < loopObjects.Length; i++)
        {
            loopObjects[i].MovingObjectTransform.Translate(0,0,-speed*Time.deltaTime);
            if(loopObjects[i].MovingObjectTransform.position.z < -loopObjects[i].RepeatDistance)
            {
                loopObjects[i].MovingObjectTransform.position = Vector3.zero;
            }
        }

        
    }
}
