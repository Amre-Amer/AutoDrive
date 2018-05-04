using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorClass {
    public GameObject goRange;
    public bool isPowerOn;
    public float range;
    public VehicleClass vehicleOwner;
    public SceneClass scene;
    public DataClass data;
    public bool ynPower;
    public SensorClass(VehicleClass vehicleOwner0, SceneClass scene0, bool ynPower0) {
        vehicleOwner = vehicleOwner0;
        scene = scene0;
        ynPower = ynPower0;
        range = 3;
        goRange = GameObject.CreatePrimitive(PrimitiveType.Cube);
        goRange.transform.position = vehicleOwner.go.transform.position;
        float sx = scene.go.transform.localScale.x * 1f;
        float sy = .1f;
        float sz = range * 2;
        if (ynPower == false) {
            sx = .1f;
            sz = .1f;
        }
        goRange.transform.localScale = new Vector3(sx, sy, sz);
        MakeMaterialTransparent(goRange.GetComponent<Renderer>().material);
        goRange.GetComponent<Renderer>().material.color = new Color(0, 1, 0, .25f);
    }
    public DataClass Scan() {
        data = InitData();
        foreach (VehicleClass vehicle in scene.vehicles) {
            if (vehicle != vehicleOwner) {
                if (IsVehicleInRange(vehicle)) {
                    if (vehicle.go.name == "v1") {
                        data.isVehicle1 = true;                        
                    }
                    if (vehicle.go.name == "v2")
                    {
                        data.isVehicle2 = true;
                    }
                    if (vehicle.go.name == "b")
                    {
                        data.isBicycle = true;
                    }
                }
            }    
        }
        return data;        
    }
    public bool IsVehicleInRange(VehicleClass vehicle) {
        // 2d distance later
        float diff = vehicle.go.transform.position.z - vehicleOwner.go.transform.position.z;
        if (Mathf.Abs(diff) <= range && diff >= 0) {
            return true;
        }
        return false;
    }
    public DataClass InitData()
    {
        data = new DataClass();
        data.isVehicle1 = false;
        data.isVehicle2 = false;
        data.isBicycle = false;
        data.slow = true;
        return data;
    }
    void MakeMaterialTransparent(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }
}
