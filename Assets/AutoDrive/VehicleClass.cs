using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleClass {
    public GameObject go;
    //public GameObject goSmooth;
    //public Vector3 target;
    public bool isBicycle;
    public bool slow;
    public Vector2 direction;
    public float speed;
    public AutoDriveClass autoDrive;
    public SceneClass scene;
    public VehicleClass(string nam, SceneClass scene0) {
        scene = scene0;
        go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = nam;
        go.transform.localScale = new Vector3(.5f, .5f, 1);
        ResetPos();
        SetColorByName();
        autoDrive = new AutoDriveClass(this, scene);
        //goSmooth = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //goSmooth.transform.position = go.transform.position;
        //target = go.transform.position;
    }
    public bool Drive() {
        autoDrive.Drive();
        return true;
    } 
    public bool Move() {
        float speedFactor = 1f;
        float slowFactor = 1f;
        //if (slow == true) slowFactor = .25f;
        go.transform.position += new Vector3(direction.x, 0, direction.y) * speed * slowFactor * speedFactor;
        //target = go.transform.position;
        AdjustSensorToVehicle();
        return true;
    }
    public void MoveToPos(Vector2 pos)
    {
        Vector3 pos0 = scene.GetCenterBottom() + new Vector3(pos.x, 0, pos.y);
        go.transform.position = AdjustPosToScene(pos0);
        //target = go.transform.position;
        AdjustSensorToVehicle();
    }
    //public void Smooth() {
    //    float smooth = .95f;
    //    goSmooth.transform.position = smooth * goSmooth.transform.position + (1 - smooth) * target;
    //}
    public void ResetPos() {
        go.transform.position = AdjustPosToScene(scene.GetCenterBottom());
    }
    public void SetColorByName() {
        Color col = Color.black;
        if (go.name == "v1") {
            col = Color.gray;
        }
        if (go.name == "v2")
        {
            col = Color.gray;
        }
        if (go.name == "b")
        {
            col = Color.cyan;
        }
        if (go.name == "a")
        {
            col = Color.white;
        }
        go.GetComponent<Renderer>().material.color = col;
    }
    public void AdjustSensorToVehicle() {
        autoDrive.sensor.goRange.transform.position = go.transform.position;
        //autoDrive.sensor.goRange.transform.position += autoDrive.sensor.goRange.transform.forward * autoDrive.sensor.goRange.transform.localScale.z / 2;
    }
    public Vector3 AdjustPosToScene(Vector3 pos) {
        pos.y = scene.go.transform.position.y + scene.go.transform.localScale.y / 2 + go.transform.localScale.y / 2;
        return pos;
    }
}
