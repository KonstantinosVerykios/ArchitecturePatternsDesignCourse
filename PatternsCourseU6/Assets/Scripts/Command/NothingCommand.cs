using UnityEngine;

public class NothingCommand : ICommand
{
    public void Execute()
    {
        Debug.Log("Did nothing");
    }

    public void Undo()
    {
        Debug.Log("Undid nothing");
    }
}
