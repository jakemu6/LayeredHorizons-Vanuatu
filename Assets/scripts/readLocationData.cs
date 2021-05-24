using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Ali making an edit :) 

public class readLocationData : MonoBehaviour
{
    // file name of location json data - must be in streaming assets folder
    public string filename = "locationData.json";

    // array to read json location data into
    private locationData[] mylocationData;



    [Header("GO to represent PPLC(capital city)")]
    public GameObject PPLC;

    [Header("YPos of PPLC")]
    public int YPosPPLC;

    [Header("GO to represent PPLA(major city)")]
    public GameObject PPLA;

    [Header("YPos of PPLA")]
    public int YPosPPLA;

    [Header("GO to represent PPL(named locations)")]
    public GameObject PPL;

    [Header("YPos of PPL")]
    public int YPosPPL;

    [Header("GO to represent PPLQ(abandoned location)")]
    public GameObject PPLQ;

    [Header("YPos of PPLQ")]
    public int YPosPPLQ;

    [Header("GO to represent Missing DSG Code(check data for missing DSG value")]
    public GameObject MissingDSGCode;

    // public GameObject precipCube;
    // public TextMesh textOnCube;
    // public LineRenderer line1;

    // holders for the map scale set in start method
    private int scaleX; 
    private int scaleY; 



    // Use this for initialization
    void Start()
    {
        // grab world scale from the commonData script
        // set in the inspector
        scaleX = (int)commonData.mapScale.x;
        scaleY = (int)commonData.mapScale.y;

        //Invoke("loadData", 2.0f);
        //print("called!");

        loadData();
    }

    private void loadData()
    {
        // create the file path to the json data as a string
        string dataFilePath = Path.Combine(Application.streamingAssetsPath, filename);

        // error check (make sure there actually is a file
        if (File.Exists(dataFilePath))
        {
            // read the data in
            string dataAsJson = File.ReadAllText(dataFilePath);

            // format the data into the 'loadedData' array using JsonHelper
            locationData[] loadedData = JsonHelper.FromJson<locationData>(dataAsJson);

            // todo: lineIndex is unused - remove completely
            // int lineIndex = 0;

            for (int i = 0; i < loadedData.Length; i++)
            {
                // In the dat NT refers to 'name type'
                // N: approved (The BGN-approved local official name for a geographic feature)
                // V: Variant: A former name, name in local usage, or other spelling found on various sources.
                // for our current purpouse using the 'N' version only if a 'V' exists ignore it
                // todo: This avoids doubles, but we should look at the data and see if this is the approapriate way to deal with this data
                if (loadedData[i].NT == "N")
                {
                    // convert from lat/long to world units
                    // using the helper method in the 'helpers' script
                    float[] thisXY = helpers.getXYPos(loadedData[i].lat, loadedData[i].lon, scaleX, scaleY);

                    // check no other point exists here as a quick and dirty way of avoiding overlap
                    // todo: we should eventually base this on a heirachy of location size rating
                    bool somethingInMySpot = false;
                    if (Physics.CheckSphere(new Vector3(thisXY[0], 0, thisXY[1]), 0.09f))
                    {
                        somethingInMySpot = true;
                    }

                    // in the data dsg refers to DSG: Feature Designation Code
                    // PPL: populated place
                    // PPLQ: populated place abandoned
                    // PPLC: capital
                    // PPLA: first oder administrative division (major cities and the like)

                    if (loadedData[i].dsg == "PPLC")
                    {
                        GameObject PPLCMarker = Instantiate(PPLC, new Vector3(thisXY[0], YPosPPLC, thisXY[1]), Quaternion.Euler(0, 0, 0));
                    }
                    else if (loadedData[i].dsg == "PPLA")
                    {
                        GameObject PPLAMarker = Instantiate(PPLA, new Vector3(thisXY[0], YPosPPLA, thisXY[1]), Quaternion.Euler(0, 0, 0));
                    }
                    else if (loadedData[i].dsg == "PPL")
                    {
                        GameObject PPLMarker = Instantiate(PPL, new Vector3(thisXY[0], YPosPPL, thisXY[1]), Quaternion.Euler(0, 0, 0));
                        //send the lattitude and longitude to the getImage so that you can get map data from the API's.
                        //needs to access the cvhild of the gameobject because that's where I put the script
                        PPLMarker.GetComponentInChildren<getImageMaterial>().SetValues(loadedData[i].lat, loadedData[i].lon);

                    }
                    else if (loadedData[i].dsg == "PPLQ")
                    {
                        GameObject PPLQMarker = Instantiate(PPLQ, new Vector3(thisXY[0], YPosPPLQ, thisXY[1]), Quaternion.Euler(0, 0, 0));
                    }
                    else
                    {
                        GameObject MissingDSGCodeMarker = Instantiate(MissingDSGCode, new Vector3(thisXY[0], 0, thisXY[1]), Quaternion.Euler(0, 0, 0));
                    }

                    //// PPL & PPLQ are displayed using the populatedPlaceMarker gameObject
                    //if (loadedData[i].dsg != "PPLC" && loadedData[i].dsg != "PPLA" && !somethingInMySpot)
                    //{
                    //    GameObject thisCube = Instantiate(PPLC, new Vector3(thisXY[0], YPosPPLC, thisXY[1]), Quaternion.Euler(0, 0, 0));
                    //    TextMesh nameText = thisCube.GetComponentInChildren<TextMesh>();
                    //    nameText.text = loadedData[i].fullnamero;
                    //    //lineIndex++;
                    //}
                    //// PPLC & PPLA are displayed namedLocationMarker above a populatedPlaceMarker whose color is changed to pink
                    //// todo: This pink color needs changing as it is confusing to users between these markers and the language location markers
                    //// which are currently the same color
                    //else if (loadedData[i].dsg == "PPLC" || loadedData[i].dsg == "PPLA")
                    //{
                    //    // I don't think we want a cube here, just the populated place marker
                    //    //GameObject thisCube = Instantiate(populatedPlaceMarker, new Vector3(thisXY[0], 0, thisXY[1]), Quaternion.Euler(90, 0, 0));
                    //    //Material cubeMaterial = thisCube.GetComponent<Renderer>().material;
                    //    //cubeMaterial.color = new Color(1, 0, 0.8666f);
                    //    GameObject thisMarker = Instantiate(PPL, new Vector3(thisXY[0], YPosPPL, thisXY[1]), Quaternion.Euler(0, 0, 0));
                    //    //print(loadedData[i].fullnamero);
                    //    TextMesh nameText = thisMarker.GetComponentInChildren<TextMesh>();
                    //    nameText.text = loadedData[i].fullnamero;
                    //    thisMarker.transform.name = loadedData[i].fullnamero;
                    //    //lineIndex++;
                    //}
                }
            }

           // print("line index is: " + lineIndex);
        }
    }
}







