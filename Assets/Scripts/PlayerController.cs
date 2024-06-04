using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float[] PlayerPosZ = { -0.8f, 0, 0.8f };
    public float jumpSpeed=5f;
    
    int index=1;
    public float movSpeed = 5;
    Animator an;
    public LayerMask GroundLayer;
    public Transform GroundRefTransform;

    bool isGrounded;

    private void Start()
    {
        an = GetComponent<Animator>();
    }
    void Update()
    {
        if (GameManager.Instance.canStartRunning)
        {
            #region free movement
            /*if (transform.position.x <= 0.88f && transform.position.x>=-0.88f)
            {
                transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime,0,0);
            }
            else
            {
                float resetPosVal = (transform.position.x > 0) ? 0.88f : -0.88f;
                transform.position = new Vector3(resetPosVal, transform.position.y,transform.position.z);
            }*/
            #endregion
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(index<2)
                    index++;
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(index>0)
                index--;
            }

            if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0,jumpSpeed,0),ForceMode.Impulse);
                an.SetTrigger("Jump");
            }

            var targetPos = new Vector3(PlayerPosZ[index], transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position,targetPos,movSpeed*Time.deltaTime);


            isGrounded = Physics.OverlapSphere(GroundRefTransform.position, .05f, GroundLayer).Length>0;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundRefTransform.position, .05f);
    }

    public void StartRunning()
    {
        
        an.SetTrigger("Run");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            an.SetBool("Fall", true);

            GameManager.Instance.GameOver();
        }
    }
    
}
