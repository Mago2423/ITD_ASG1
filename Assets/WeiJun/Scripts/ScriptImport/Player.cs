using UnityEngine;

// plain old data object(PODO) - monobehaviour is for game object
public class Player
{
    public string name;
    public string character;
    public Vector3 position;
    //constructor
    public Player(string name, string character, Vector3 position)
    {
        this.name = name;
        this.character = character;
        this.position = position;
    }
}
