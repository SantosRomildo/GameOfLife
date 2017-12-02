using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPan : MonoBehaviour {

    public Button plus;
    public Button minus;
    private float addZoon = .5f;

    void Start () {
		plus.onClick.AddListener(() =>
        {
            if(Camera.main.orthographicSize - addZoon > 0)
            {
                Camera.main.orthographicSize -= addZoon;
            }
        });

        minus.onClick.AddListener(() =>
        {
            Camera.main.orthographicSize += addZoon;
        });
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(x, y, 0) * 4f * Time.deltaTime);
    }
}
