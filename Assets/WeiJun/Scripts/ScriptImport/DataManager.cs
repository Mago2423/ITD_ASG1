using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("countries") //loading all countries
            .OrderByChild("alpha2")
            .EqualTo("SG")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                    return;
                }
                else if (task.IsCanceled)
                {
                    Debug.Log("Load Countries canceled.");
                    return;
                }

                var snapshot = task.Result;
                //check if data retrieved has multiple records
                if (snapshot.HasChildren)
                {
                    //loop through every child
                    foreach (var child in snapshot.Children)
                    {
                        var population = child.Child("population").Value;
                        var cityName = child.Child("name").Value;
                        Debug.Log($"{population} {cityName}");
                    }
                }
            });
    }


}
