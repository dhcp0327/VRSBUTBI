using UnityEngine;
using System.IO;
public class TextParser : MonoBehaviour
{
    private void Start()
    {
        SimFileHandler.FileLoaded += ParseFile;
    }
    private void ParseFile(string filePath)
    {
        string fileText = File.ReadAllText(filePath);
        Debug.Log("Contents of the file: " + fileText);
    }
}