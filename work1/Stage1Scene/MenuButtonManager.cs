using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    GameObject childButtonImage;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        childButtonImage = transform.GetChild(0).gameObject;
        image = childButtonImage.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CursorEnter()
    {
        image.color = new Color(255, 255, 255, 255);
    }

    public void CursorExit()
    {
        image.color = new Color(255, 255, 255, 0);
    }
}
