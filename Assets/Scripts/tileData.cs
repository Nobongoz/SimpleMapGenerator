using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileData : MonoBehaviour
{
    public float tileSpeed;
    public Vector2 tilePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Pos: " + tilePos);
    }
}
