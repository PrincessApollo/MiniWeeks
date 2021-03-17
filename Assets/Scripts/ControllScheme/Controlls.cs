using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public static class Controlls
{
    public static ControllScheme Scheme
    {
        get
        {
            if (scheme != null) return scheme;
            else
            {
                var path = UnityEngine.Application.dataPath + "/controlls.ctrl";
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "{\"keys\": {\"PlayerOne-Forward\": \"W\",\"PlayerOne-Back\": \"S\",\"PlayerOne-Left\": \"A\",\"PlayerOne-Right\": \"D\",\"PlayerOne-Punch\": \"LeftAlt\",\"PlayerTwo-Forward\": \"UpArrow\",\"PlayerTwo-Back\": \"DownArrow\",\"PlayerTwo-Left\": \"LeftArrow\",\"PlayerTwo-Right\": \"RightArrow\",\"PlayerTwo-Punch\": \"RightAlt\"}}");
                    Debug.Log(path + " has been created");
                }
                scheme = JsonConvert.DeserializeObject<ControllScheme>(File.ReadAllText(path));
                return scheme;
            }

        }
        internal set
        {
            scheme = value;
        }
    }
    private static ControllScheme scheme;
}
[System.Serializable]
public class ControllScheme
{
    [JsonProperty]
    public Dictionary<string, string> keys;
    public ControllScheme(Dictionary<string, string> keys) => this.keys = keys;
}