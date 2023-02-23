using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPathToStation : MonoBehaviour
{
    public TrainStation from;
    public string destination;

    int currentLayer = 0;
    int maxDepth = 500;


    public class StationElement
    {
        public TrainStation station;
        public StationElement parent;

        public StationElement(TrainStation station, StationElement parent)
        {
            this.station = station;
            this.parent = parent;
        }
    }



    List<List<StationElement>> tree = new List<List<StationElement>>();

    //private void Start()
    //{
    //    print("Path from " + from.IndexName + " -> " + destination);
    //
    //    List<StationElement> stations = GetPathTo(destination);
    //
    //    if (stations != null)
    //    {
    //        foreach (var stationElement in stations)
    //        {
    //            print(stationElement.station.IndexName);
    //        }
    //        print("The Next Step Would be to go to -> " + GetNextStation().IndexName);
    //        //Waiting for Leon
    //        //print("For that you need to use the -> " + from.GetTrain(GetNextStation().IndexName));
    //    }
    //}

    public string GetDirectionU()
    {
        return from.GetTrain(GetNextStation().IndexName);
    }

    public string GetDirection()
    {
        return GetNextStation().IndexName;
    }

    public TrainStation GetNextStation()
    {
        return GetPathTo(destination)[0].station;
    }

    public List<StationElement> GetPathTo(string name)
    {
        CreateTree();
        foreach (var stationElementList in tree)
        {
            foreach (var stationElement in stationElementList)
            {
                if (stationElement.station.IndexName == name)
                {
                    StationElement element = stationElement;
                    List<StationElement> path = new List<StationElement>();

                    path.Add(element);

                    while (element.parent != null)
                    {
                        path.Add(element.parent);
                        element = element.parent;
                    }

                    /*Delete Destination out of Path*/
                    path.RemoveAt(path.Count-1);

                    /*Reverse Path*/
                    List<StationElement> returnPath = new List<StationElement>();
                    for (int i = path.Count-1; i >= 0; i--)
                    {
                        returnPath.Add(path[i]);
                    }

                    DeleteTree();
                    return returnPath;
                }
            }
        }
        DeleteTree();
        return null;
    }

    void CreateTree()
    {
        tree.Add(new List<StationElement>());
        StationElement station = new StationElement(from, null);
        tree[0].Add(station);
        currentLayer = 1;

        while (currentLayer < maxDepth)
        {
            SearchNextLayer();
        }
    }

    void DeleteTree()
    {
        tree = new List<List<StationElement>>();
    }

    void SearchNextLayer()
    {
        if (tree[currentLayer-1].Count == 0)
        {
            tree.RemoveAt(currentLayer - 1);
            currentLayer = maxDepth;
            return;
        }
        tree.Add(new List<StationElement>());
        foreach (var stationElement in tree[currentLayer-1])
        {
            foreach (var subStation in stationElement.station.TargetStaionList)
            {
                if (IsNotInTree(subStation.IndexName))
                {
                    StationElement station = new StationElement(subStation, stationElement);
                    tree[currentLayer].Add(station);
                }
            }
        }
        currentLayer++;
    }

    bool IsNotInTree(string name)
    {
        foreach (var layer in tree)
        {
            foreach (var stationElement in layer)
            {
                if (stationElement.station.IndexName == name)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void PrintTree()
    {
        for (int i = 0; i < tree.Count; i++)
        {
            print("-------------------");
            print("Layer: " + i);
            foreach (var child in tree[i])
            {
                print(child.station.IndexName);
            }
        }
    }
}
