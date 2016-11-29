using UnityEngine;
using System.Collections;

public class scroll_texture : MonoBehaviour {

    public MeshRenderer meshrender;
    float offset = 0;
    Vector2 vec;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            offset += 0.03f * 0.01f ;
            
            vec.x = offset;
            vec.y = 0;
            meshrender.material.SetTextureOffset("_MainTex", vec);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            offset -= 0.03f * 0.01f;

            vec.x = offset;
            vec.y = 0;
            meshrender.material.SetTextureOffset("_MainTex", vec);
        }
	}
}
