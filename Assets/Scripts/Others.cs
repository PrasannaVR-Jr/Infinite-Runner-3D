using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoopObjects
{
    public float RepeatDistance;
    public Transform MovingObjectTransform;
}

[System.Serializable]
public class ObstaclePool
{
    public string Tag;
    public Queue<GameObject> obstacleGameObjects=new Queue<GameObject>();
}
