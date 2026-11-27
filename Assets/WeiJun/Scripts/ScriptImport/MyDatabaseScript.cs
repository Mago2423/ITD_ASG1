using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;

public class MyDatabaseScript : MonoBehaviour
{   
    public string character;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // //create a new player object with the 
        // //name = GameObject name
        // //character = Script's "character" field
        // //position = game object's position
        // var player = new Player(name, character, transform.position);

        // //convert player object to JSON string
        // var json = JsonUtility.ToJson(player);

        // //print on consol
        // // Debug.Log(json);

        //Save the player to the database
        // FirebaseDatabase
        //     .DefaultInstance
        //     .RootReference
        //     .Child(name) //use the GameObject name as the Child key
        //     .GetValueAsync()// Retrieve 
        //     .ContinueWithOnMainThread(task => {
        //         //get the snapshot of the data at the reference
        //         var snapshot = task.Result;
        //         //Get the JSON string from the snapshot
        //         var json = snapshot.GetRawJsonValue();
        //         //Print it out
        //         Debug.Log(json);
        //     });

        // Debug.Log("World");
    //     FirebaseDatabase
    //         .DefaultInstance
    //         .RootReference
    //         .Child("position")
    //         .ValueChanged += PositionChanged;
        
    // }

    // void PositionChanged(object sender, ValueChangedEventArgs args)
    // {
    //     var snapshot = args.Snapshot;
    //     var json = snapshot.GetRawJsonValue();
    //     var pos = JsonUtility.FromJson<Vector3>(json);
    //     transform.position = pos;
    //     Debug.Log($"new position: {pos}");
    // 
    

    var d = new Dictionary<string, int>();
    d["Justin"] = 10;
    d["Alex"] = 20;
    }
    



    // Update is called once per frame
    void Update()
    {

        
    }
}
