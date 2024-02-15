using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLook_Basic : MonoBehaviour
{

    private Vector2 currentLook;
    private Transform myBody;
    // Start is called before the first frame update
    void Start()
    {
        myBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateLook();
    }

    void UpdateLook()
    {

        Vector2 newLook = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentLook += newLook;
        transform.localRotation = Quaternion.AngleAxis(-currentLook.y, Vector3.right);
        myBody.localRotation = Quaternion.AngleAxis(currentLook.x, Vector3.up);
    }
}
