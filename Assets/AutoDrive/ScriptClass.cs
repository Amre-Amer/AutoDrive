using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptClass {
    public List<string> scriptLines;
    public ScriptClass() {
        scriptLines = new List<string>();        
    }
    public bool AddScriptLine(string txt) {
        scriptLines.Add(txt);
        return true;        
    }
    public void Show() {
        foreach (string txt in scriptLines) {
            Debug.Log(txt + "\n");
        }
    }
    public string GetScriptLineName(string txtLine) {
        return GetScriptLineKeyValue("name", txtLine);
    }
    public Vector2 GetScriptLinePos(string txtLine)
    {
        string pair = GetScriptLineKeyValue("pos", txtLine);
        string[] items = pair.Split(',');
        if (items.Length == 2)
        {
            float x = float.Parse(items[0]);
            float y = float.Parse(items[1]);
            return new Vector2(x, y);
        }
        return Vector2.zero;
    }
    public int GetScriptLineFrame(string txtLine)
    {
        string txt = GetScriptLineKeyValue("frame", txtLine);
        return int.Parse(txt);
    }
    public Vector2 GetScriptLineDirection(string txtLine)
    {
        string pair = GetScriptLineKeyValue("direction", txtLine);
        string[] items = pair.Split(',');
        if (items.Length == 2)
        {
            float x = float.Parse(items[0]);
            float y = float.Parse(items[1]);
            return new Vector2(x, y);
        }
        return new Vector2(-1, -1);
    }
    public float GetScriptLineSpeed(string txtLine)
    {
        string txt = GetScriptLineKeyValue("speed", txtLine);
        return float.Parse(txt);
    }
    // name=a ; pos=2,0 ; frame=0 ; direction=0,1 ; speed=1
    public string GetScriptLineKeyValue(string key, string txt)
    {
        string[] pairs = txt.Split(';');
        foreach(string pair in pairs) {
            if (pair.Contains(key)) {
                string[] items = pair.Split('=');
                if (items.Length == 2) {
                    return items[1].Trim();
                }
            }
        }
        return "-1";
    }
    public bool ApplyScriptLineForFrame(int frame, SceneClass scene)
    {
        foreach (string txtLine in scriptLines)
        {
            int frameLine = GetScriptLineFrame(txtLine);
            if (frameLine == frame)
            {
                ApplyScriptLine(txtLine, scene);
            }
        }
        return true;
    }
    // name=a ; pos=2,0 ; frame=0 ; direction=0,1 ; speed=1
    public bool ApplyScriptLine(string txtLine, SceneClass scene)
    {
        VehicleClass vehicle = GetScriptLineVehicleByName(txtLine, scene);
        if (vehicle != null) {
            ApplyScriptLinePos(txtLine, vehicle);
            ApplyScriptLineDirection(txtLine, vehicle);
            ApplyScriptLineSpeed(txtLine, vehicle);
        }
        return true;
    }
    public VehicleClass GetScriptLineVehicleByName(string txtLine, SceneClass scene)
    {
        string nam = GetScriptLineName(txtLine);
        VehicleClass vehicle = scene.GetOrAddVehicleByName(nam, scene);
        return vehicle;
    }
    public void ApplyScriptLinePos(string txtLine, VehicleClass vehicle)
    {
        Vector2 pos = GetScriptLinePos(txtLine);
        if (pos != Vector2.zero)
        {
            vehicle.MoveToPos(pos);
        }
    }
    public void ApplyScriptLineDirection(string txtLine, VehicleClass vehicle)
    {
        Vector2 direction = GetScriptLineDirection(txtLine);
        if (direction != new Vector2(-1, -1))
        {
            vehicle.direction = direction;
        }
    }
    public void ApplyScriptLineSpeed(string txtLine, VehicleClass vehicle)
    {
        float speed = GetScriptLineSpeed(txtLine);
        if (speed != -1)
        {
            vehicle.speed = speed;
        }
    }
}
