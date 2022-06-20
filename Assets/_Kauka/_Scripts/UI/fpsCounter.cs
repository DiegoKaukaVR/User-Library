using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fpsCounter : MonoBehaviour
{

    float actuIntervalo = 0.5f;
    int frameCount;
    float frameTime;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }
    private void Update()
    {
        frameCount++;
        frameTime += Time.unscaledDeltaTime;
        if (frameTime > actuIntervalo)
        {
            float fps = frameCount / frameTime;
            text.text = string.Format("{0:F2} FPS", fps);
            frameCount = 0;
            frameTime = 0;
        }
    }

}
