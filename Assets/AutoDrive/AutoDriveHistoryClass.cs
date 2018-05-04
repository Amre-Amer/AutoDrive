using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDriveHistoryClass{
    public int numScenes = 5;
    public int lastScene; 
    public List<DataClass>[] sceneFrames;
    public AutoDriveClass autoDriveMisc;
    public AutoDriveHistoryClass() {
        sceneFrames = new List<DataClass>[numScenes];
        autoDriveMisc = new AutoDriveClass(null, null);
    }
    public bool ImportFrames(List<DataClass>frames0) {
        if (lastScene >= numScenes) {
            Debug.Log("AutoDriveHistory FULL...\n");
            return false;
        }
        List<DataClass> frames = new List<DataClass>();
        foreach(DataClass data0 in frames0) {
            DataClass data = CopyData(data0);
            frames.Add(data);
            int f = frames.Count - 1;
            Vector3 pos = getPosFrame(lastScene, f);
            autoDriveMisc.ShowFrame(frames, frames.Count - 1, pos, "history");
        }
        sceneFrames[lastScene] = frames;
        lastScene++; 
        return true;
    }
    public DataClass CopyData(DataClass data0) {
        DataClass data = new DataClass();
        data.isVehicle1 = data0.isVehicle1;
        data.isVehicle2 = data0.isVehicle2;
        data.isBicycle = data0.isBicycle;
        data.slow = data0.slow;
        return data;
    }
    public Vector3 getPosFrame(int s, int f) {
        float h = .75f + .25f;
        Vector3 pos = new Vector3(s * 10, 0, 30);
        pos += Vector3.forward * h * (f + .5f);
        return pos;
    }
    public DataClass LookAhead(DataClass dataSearch) {
        DataClass dataResult = null;
        for (int s = 0; s < lastScene; s++) {
            List<DataClass> frames = sceneFrames[s];
            for (int f = 0; f < frames.Count; f++) {
                DataClass data = frames[f];
                bool ynMatch = IsDataMatched(data, dataSearch);
                if (ynMatch == true) {
                    dataResult = CopyData(data);
                    data.ynHighlight = true;
                    Vector3 pos = getPosFrame(s, f);
                    autoDriveMisc.ShowFrame(frames, f, pos, "");
                    Debug.Log(dataSearch.isVehicle1 + " " + dataSearch.isVehicle2 + " " + dataSearch.isBicycle + "\n");
                    Debug.Log("match:s:" + s + " f:" + f + "\n");
                    break;
                }
            }
            if (dataResult != null) {
                break;
            }
        }
        return dataResult;                
    }
    public bool IsDataMatched(DataClass data, DataClass dataSearch) {
        bool ynMatch = true;

        if (data.isVehicle1 != dataSearch.isVehicle1)
        {
            ynMatch = false;
        }
        if (data.isVehicle2 != dataSearch.isVehicle2)
        {
            ynMatch = false;
        }
        if (data.isBicycle != dataSearch.isBicycle)
        {
            ynMatch = false;
        }
        return ynMatch;
    }
}
