using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerClass : MonoBehaviour {
    public AutoDriveHistoryClass autoDrivehistory;
    public List<SceneClass> scenes;
    public SceneManagerClass() {
        scenes = new List<SceneClass>();
        autoDrivehistory = new AutoDriveHistoryClass();
    }
    public SceneClass AddScene(string nam, Vector2 pos) {
        SceneClass scene = new SceneClass(nam, pos, this);
        scenes.Add(scene);
        return scene;
    }
    public void AdvanceScenes() {
        foreach (SceneClass scene in scenes) {
            scene.Advance();
        }
    }
    public void ResetSceneFrames()
    {
        foreach (SceneClass scene in scenes)
        {
            scene.frame = 0;
            foreach (VehicleClass vehicle in scene.vehicles)
            {
                if (vehicle.autoDrive.ynPower == true)
                {
                    vehicle.autoDrive.frames.Clear();
                    vehicle.ResetPos();
                }
            }
        }
    }
//    public void UpdateSmooth() {
//        foreach (SceneClass scene in scenes)
//        {
//            foreach (VehicleClass vehicle in scene.vehicles)
//            {
//                if (vehicle.autoDrive.ynPower == true)
//                {
//                    vehicle.Smooth();
////                    autoDrivehistory.ImportFrames(vehicle.autoDrive.frames);
    //            }
    //        }
    //    }
    //}
    public void SendFramesToHistory() {
        foreach (SceneClass scene in scenes)
        {
            foreach( VehicleClass vehicle in scene.vehicles) {
                if (vehicle.autoDrive.ynPower == true) {
                    autoDrivehistory.ImportFrames(vehicle.autoDrive.frames);
                }
            }
        }
    }
}
