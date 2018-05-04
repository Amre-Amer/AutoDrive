using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDriveDemo : MonoBehaviour {
    float timeStart;
    public bool ynPlaying;
    public bool ynStep;
    int cntFrames = 0;
    int frameMax = 24;
    int cntTimes;
    float delay = .25f;
    SceneManagerClass sceneManager;

	void Start () {
        Debug.Log("AutoDrive...\n");
        sceneManager = new SceneManagerClass();
        SceneDemo(0);
        SceneDemo(1);
        SceneDemo(2);
        SceneDemo(3);
        SceneDemo(4);
        ynStep = true;
        ynPlaying = true;
        //sceneManager.AdvanceScenes();
	}

    private void Update()
    {
        //sceneManager.UpdateSmooth();
        if (Time.realtimeSinceStartup - timeStart < delay)
        {
            return;
        }
        if (ynPlaying == true)
        {
            sceneManager.AdvanceScenes();
        }
        timeStart = Time.realtimeSinceStartup;
//        Debug.Log(cntTimes + " frame:" + cntFrames + " Update...\n");
        if (cntFrames < frameMax) {
            
        } else {
            cntFrames = 0;
            if (cntTimes == 0)
            {
                sceneManager.SendFramesToHistory();
            }
            sceneManager.ResetSceneFrames();
            //if (cntTimes > 0) {
                //delay = 1f;
            //}
            cntTimes++;
        }
        //sceneManager.UpdateSmooth();
        cntFrames++;
    }

    public void SceneDemo(int xStart) {
        SceneClass scene = sceneManager.AddScene("scene " + xStart, new Vector2(xStart * 17, 10));
        scene.frameMax = frameMax;
        //
        if (xStart == 0)
        {
            scene.script.AddScriptLine("name=v1 ; pos=2,5 ; frame=0");
            scene.script.AddScriptLine("name=v2 ; pos = 2,8 ; frame=0");
            scene.script.AddScriptLine("name=v1 ; move=0,1 ; frame=5");
            scene.script.AddScriptLine("name=b ; pos=2,14 ; direction=0,1 ; speed=.25 ; frame=0");
            scene.script.AddScriptLine("name=a ; pos=0,0 ; frame=0 ; direction=0,1 ; speed=1");
        }
        if (xStart == 1)
        {
            scene.script.AddScriptLine("name=v2 ; pos=2,8 ; direction=0,1 ; speed=1 ; frame=0");
            scene.script.AddScriptLine("name=b ; pos=2,6 ; frame=0");
            scene.script.AddScriptLine("name=a ; pos=0,0 ; frame=0 ; direction=0,1 ; speed=1");
        }
        if (xStart == 2)
        {
            scene.script.AddScriptLine("name=v1 ; pos=2,5 ; frame=0");
            scene.script.AddScriptLine("name=v2 ; move=0,1 ; frame=5");
            scene.script.AddScriptLine("name=b ; pos=2,7 ; frame=0");
            scene.script.AddScriptLine("name=a ; pos=0,0 ; frame=0 ; direction=0,1 ; speed=1");
        }
        if (xStart == 3)
        {
            scene.script.AddScriptLine("name=b ; pos=2,4 ; frame=0");
            scene.script.AddScriptLine("name=a ; pos=0,0 ; frame=0 ; direction=0,1 ; speed=1");
        }
        if (xStart == 4)
        {
            scene.script.AddScriptLine("name=v1 ; pos=2,5 ; frame=0");
            scene.script.AddScriptLine("name=v1 ; move=0,1 ; frame=5");
            scene.script.AddScriptLine("name=a ; pos=0,0 ; frame=0 ; direction=0,1 ; speed=1");
        }
    }
}
