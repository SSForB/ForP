using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VirtualPlayerController : MonoBehaviour
{
    //移動スピード
    public float speed = 7.0f;

    float axisH;
    float axisV;

    Rigidbody2D rbody;
    
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");

        if ((axisH != 0) && (axisV != 0)) 
        {
            rbody.velocity = (new Vector2(axisH, axisV)) * speed * 0.7071f;
        }
        else
        {
            rbody.velocity = (new Vector2(axisH, axisV)) * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Guild")
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (collision.gameObject.tag == "WorkShop")
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (collision.gameObject.tag == "House")
        {
            SceneManager.LoadScene("TitleScene");
        }

        if (collision.gameObject.tag == "Store")
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
