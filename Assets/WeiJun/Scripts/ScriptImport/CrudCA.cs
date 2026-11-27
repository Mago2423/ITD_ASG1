using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;

public class CrudCA : MonoBehaviour
{
    public string character;
    public TMP_Text characterText;

    [System.Serializable]
    public class Player
    {
        public string character;

        public Player()
        {
        }
    }

    public void CreatePlayer()
    {
        //create a new player object with the 
        var player = new Player();
        player.character = character;

        //convert player object to JSON string
        var json = JsonUtility.ToJson(player);

        //print on consol
        Debug.Log(json);

        //Save the player to the database
        FirebaseDatabase
            .DefaultInstance
            .GetReference("player")
            .SetRawJsonValueAsync(json);
            //Print it out
            Debug.Log(json);
            
    }

    public void UpdatePlayer()
    {
        //update player charecter
        FirebaseDatabase
            .DefaultInstance
            .GetReference("player")
            .Child("character")
            .SetValueAsync("Second");
        
    }

    public void ReadPlayer()
    {
        FirebaseDatabase
            .DefaultInstance
            .GetReference("player")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                }
                else if (!task.IsCanceled)
                {
                    DataSnapshot snapshot = task.Result;
                    var json = snapshot.GetRawJsonValue();
                    var player = JsonUtility.FromJson<Player>(json);
                    Debug.Log($"Player character: {player.character}");
                    characterText.text = player.character;
                }
            });
    }


    public void DeletePlayer()
    {
        FirebaseDatabase
            .DefaultInstance
            .GetReference("player")
            .RemoveValueAsync();
    }
}
