using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class FileParser : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to the FileLoaded event of the SimFileHandler
        SimFileHandler.FileLoaded += ParseFile;
    }

    private void ParseFile(string filePath)
    {
        // Read the contents of the file
        string fileText = File.ReadAllText(filePath);

        // Split the contents of the file into individual lines
        string[] lines = fileText.Split('\n');

        // Create a list to hold the commands
        List<object[]> commands = new List<object[]>();

        // Parse each line into a command and add it to the list
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue; // Skip empty lines

            // Split the line into its components
            string[] components = line.Split(',');

            // Parse the components and add them to the list of commands
            switch (components[0])
            {
                case "CREATE":
                    commands.Add(new object[] { components[1], components[2], int.Parse(components[3]), int.Parse(components[4]) });
                    break;
                case "SETOBJCELL":
                    commands.Add(new object[] { components[1], components[2], int.Parse(components[3]), int.Parse(components[4]) });
                    break;
                default:
                    Debug.LogWarning("Unrecognized command: " + components[0]);
                    break;
            }
        }

        // Raise the CommandReceived event and pass the list of commands
        CommandReceived?.Invoke(commands);
    }

    // Define the CommandReceived event
    public delegate void CommandReceivedEventHandler(List<object[]> commands);
    public static event CommandReceivedEventHandler CommandReceived;
}
