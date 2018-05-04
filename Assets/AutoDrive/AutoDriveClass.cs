using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDriveClass {
    public int numFramesLookAhead;
    public SensorClass sensor;
    public VehicleClass vehicleOwner;
    public SceneClass scene;
    public DataClass data;
    public List<DataClass> frames;
    public bool ynPower;
    public GameObject parentAutoDrive;
    public AutoDriveClass(VehicleClass vehicleOwner0, SceneClass scene0) {
        scene = scene0;
        vehicleOwner = vehicleOwner0;
        frames = new List<DataClass>();
        parentAutoDrive = new GameObject("parentAutoDrive");
        if (vehicleOwner != null)
        {
            if (vehicleOwner.go.name == "a")
            {
                ynPower = true;
            }
            sensor = new SensorClass(vehicleOwner, scene, ynPower);
        }
    }
    public bool Drive() {
        data = sensor.Scan();
        Decide();
        DecideLookAhead();
        UpdateVehicleSlow();
        AddFrame();
        return true;        
    }
    public void DecideLookAhead() {
        //if (data.isVehicle2 == false && data.isVehicle1 == false && data.isBicycle == false) {
        //    return;
        //}
        DataClass dataLookAhead = scene.sceneManager.autoDrivehistory.LookAhead(data);
        //Debug.Log("LookAhead:" + dataLookAhead + "\n");
        if (dataLookAhead != null) {
            if (dataLookAhead.slow == true) {
                //Debug.Log(data.slow + " xLookAhead:" + dataLookAhead.slow + "\n");
                //if (data.slow == false) {
                    Debug.Log(data.slow + " LookAhead:" + dataLookAhead.slow + "\n");
                    //data.slow = 1;
                    data.lookAhead = dataLookAhead.slow;
                //}
            } 
        } else {
            Debug.Log("not found...\n");
        }
    }
    public void AddFrame() {
        frames.Add(data);
        float w = 6;
        float h = .75f + .25f;
        int f = frames.Count - 1;
        Vector3 pos = scene.GetCenterBottom() + scene.go.transform.right * (scene.go.transform.localScale.x * .65f + w / 2);
        pos += Vector3.forward * h * (f + .5f);
        ShowFrame(frames, f, pos, scene.go.name);
    }
    public void UpdateVehicleSlow() {
        vehicleOwner.slow = data.slow;
        if (data.slow == true) {
            vehicleOwner.go.GetComponent<Renderer>().material.color = Color.yellow;
        } else {
            vehicleOwner.go.GetComponent<Renderer>().material.color = Color.white;
        }
    }
    public DataClass MatchHistory() {
        return new DataClass();
    }
    public DataClass Decide() {
        // safety default
        // two vehicles = slow
        // one bicycle = slow
        data.slow = false;
        int cnt = 0; 
        if (data.isVehicle1 == true){
            cnt++;
        }
        if (data.isVehicle2 == true){
            cnt++;
        }
        if (data.isBicycle == true){
            cnt = 2;
        }
        if (cnt >= 2) {
            data.slow = true;
        }
        return data;
    }
    public void ShowFrame(List<DataClass>frames, int f, Vector3 pos, string txt) {
        if (frames[f].goBase == null) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.parent = parentAutoDrive.transform;
            go.name = "data scene:" + txt + " frame:" + f + "...";
            go.transform.localScale = new Vector3(6, .25f, .75f);
            frames[f].goBase = go;
        }
        GameObject goBase = frames[f].goBase;
        float w = goBase.transform.localScale.x;
        float h = goBase.transform.localScale.z + .25f;
        frames[f].goBase.transform.position = pos;
        if (frames[f].ynHighlight == true)
        {
            frames[f].goBase.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            frames[f].goBase.GetComponent<Renderer>().material.color = Color.white;
        }
        //
        Color col = Color.black;
        if (frames[f].goVehicle1 == null) {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            go.transform.parent = parentAutoDrive.transform;
            go.name = "data scene:" + txt + " frame:" + f + " v1";
            Vector3 posData = goBase.transform.position;
            float s = 0;
            int n = 0;
            posData += goBase.transform.right * (-2.5f + n + s);
            go.transform.position = posData;
            go.transform.localScale = new Vector3(.65f, .25f, .65f);
            frames[f].goVehicle1 = go;
        }
        if (frames[f].isVehicle1 == true)
        {
            col = Color.red;
        }
        else
        {
            col = Color.blue;
        }
        frames[f].goVehicle1.GetComponent<Renderer>().material.color = col;
        //
        if (frames[f].goVehicle2 == null)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            go.transform.parent = parentAutoDrive.transform;
            go.name = "data scene:" + txt + " frame:" + f + " v2";
            Vector3 posData = goBase.transform.position;
            float s = 0;
            int n = 1;
            posData += goBase.transform.right * (-2.5f + n + s);
            go.transform.position = posData;
            go.transform.localScale = new Vector3(.65f, .25f, .65f);
            frames[f].goVehicle2 = go;
        }
        if (frames[f].isVehicle2 == true)
        {
            col = Color.red;
        }
        else
        {
            col = Color.blue;
        }
        frames[f].goVehicle2.GetComponent<Renderer>().material.color = col;
        //
        if (frames[f].goBicycle == null)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            go.transform.parent = parentAutoDrive.transform;
            go.name = "data scene:" + txt + " frame:" + f + " b";
            Vector3 posData = goBase.transform.position;
            float s = 0;
            int n = 2;
            posData += goBase.transform.right * (-2.5f + n + s);
            go.transform.position = posData;
            go.transform.localScale = new Vector3(.65f, .25f, .65f);
            frames[f].goBicycle = go;
        }
        if (frames[f].isBicycle == true)
        {
            col = Color.red;
        }
        else
        {
            col = Color.blue;
        }
        frames[f].goBicycle.GetComponent<Renderer>().material.color = col;
        //
        if (frames[f].goSlow == null)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            go.transform.parent = parentAutoDrive.transform;
            go.name = "data scene:" + txt + " frame:" + f + " slow";
            Vector3 posData = goBase.transform.position;
            float s = .75f;
            int n = 3;
            posData += goBase.transform.right * (-2.5f + n + s);
            go.transform.position = posData;
            go.transform.localScale = new Vector3(.65f, .25f, .65f);
            frames[f].goSlow = go;
        }
        if (frames[f].slow == true)
        {
            col = Color.red;
        }
        else
        {
            col = Color.blue;
        }
        frames[f].goSlow.GetComponent<Renderer>().material.color = col;
        //
        if (frames[f].goLookAhead == null)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            go.transform.parent = parentAutoDrive.transform;
            go.name = "data scene:" + txt + " frame:" + f + " lookAhead";
            Vector3 posData = goBase.transform.position;
            float s = 1f;
            int n = 4;
            posData += goBase.transform.right * (-2.5f + n + s);
            go.transform.position = posData;
            go.transform.localScale = new Vector3(.65f, .25f, .65f);
            frames[f].goLookAhead = go;
        }
        if (frames[f].lookAhead == true)
        {
            col = Color.red;
        }
        else
        {
            col = Color.blue;
        }
        frames[f].goLookAhead.GetComponent<Renderer>().material.color = col;
        //
    }
}
