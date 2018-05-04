using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneClass
{
    public GameObject go;
    //public Vector3 center;
    //public Vector3 scale;
    public int numX;
    public int numY;
    public int frame;
    public int frameMax;
    public ScriptClass script;
    public List<VehicleClass> vehicles;
    public SceneManagerClass sceneManager;
    public SceneClass(string nam, Vector2 pos, SceneManagerClass sceneManager0)
    {
        sceneManager = sceneManager0;
        numX = 5;
        numY = 24;
        frameMax = 24;
        script = new ScriptClass();
        vehicles = new List<VehicleClass>();
        go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = nam;
        go.transform.position = new Vector3(pos.x, -.05f, pos.y);
        go.transform.localScale = new Vector3(numX, .1f, numY);
        go.GetComponent<Renderer>().material.color = Color.gray;
    }
    public void Advance()
    {
        if (frame <= frameMax)  // 10 + 4      h + range 
        {
            if (frame > 0)
            {
                MoveVehicles();
            }
            script.ApplyScriptLineForFrame(frame, this);
            UpdateVehicles();
            frame++;
        }
    }
    public void UpdateVehicles()
    {
        foreach (VehicleClass vehicle in vehicles)
        {
            if (vehicle.autoDrive.ynPower == true)
            {
                vehicle.Drive();
            } else {
                vehicle.Move();
            }
        }
    }
    public void MoveVehicles()
    {
        foreach (VehicleClass vehicle in vehicles)
        {
            if (vehicle.autoDrive.ynPower == true)
            {
                vehicle.Move();
            }
        }
    }
    public VehicleClass GetOrAddVehicleByName(string nam, SceneClass scene)
    {
        VehicleClass vehicle = GetVehicleByName(nam);
        if (vehicle == null)
        {
            vehicle = new VehicleClass(nam, scene);
            vehicles.Add(vehicle);
        }
        return vehicle;
    }
    public VehicleClass GetVehicleByName(string nam)
    {
        foreach (VehicleClass vehicle in vehicles)
        {
            if (vehicle.go.name == nam)
            {
                return vehicle;
            }
        }
        return null;
    }
    //public void UpdateVehicle(VehicleClass vehicle)
    //{
    //    //        Debug.Log("Scene:UpdateVehicle...\n");
    //    if (vehicle.go.name == "a")
    //    {
    //        vehicle.Drive();
    //    }
    //}
    public Vector3 GetCenterBottom()
    {
        Vector3 pos = go.transform.position + go.transform.forward * -1 * go.transform.localScale.z / 2;
        return pos;
    }
}
