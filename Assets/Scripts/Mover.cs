using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    ObjectLooper ob;
    private void Start()
    {
        ob = FindObjectOfType<ObjectLooper>();
        
    }
    void Update()
    {
        if (ob.enabled)
        {
            transform.Translate(0, 0, -ob.speed * Time.deltaTime);
            if (transform.position.z < -60)
            {
                gameObject.SetActive(false);
            }
        }
        

    }

    private void OnDisable()
    {
        GameManager.Instance.EnqueGameObject(tag, gameObject);
    }
}
