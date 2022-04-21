using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private List<KeyCommand> keyCommands = new List<KeyCommand>();

    //checks for input and executes input when necessary
    public void HandleInput()
    {
        foreach(KeyCommand keyCommand in keyCommands)
        {
            if (Input.GetKeyDown(keyCommand.key))
            {
                keyCommand.command.Execute();
            }
        }
    }

    //binds keycodes to specific commands
    public void BindInput(KeyCode keyCode, ICommand command)
    {
        //first unbind, because we only want one command per key
        UnBindInput(keyCode);

        //then add the new input
        keyCommands.Add(new KeyCommand() { key = keyCode, command = command });
    }

    
    //unbinds a key
    public void UnBindInput(KeyCode keyCode)
    {
        var items = keyCommands.FindAll(x => x.key == keyCode);
        items.ForEach(x => keyCommands.Remove(x));
    }
    public class KeyCommand
    {
        public KeyCode key;
        public ICommand command;
    }
}
