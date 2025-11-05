using BepInEx;
using BingusNametags.Plugins;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using TMPro;
using UnityEngine;

[BepInDependency("bingus.nametags", DependencyFlags.HardDependency)]
[BepInPlugin("bingus.grungus", "BingusBingusNametags", "1.0.0")]
public class NametagLoader : BaseUnityPlugin
{
    public static Dictionary<string, string> mods = new Dictionary<string, string>();

    static void UpdateNametag(TextMeshPro tmpro, VRRig playerRig)
    {
        StringBuilder s = new StringBuilder();

        // Color code (square with the color, and then color code itself)
        int r = Mathf.RoundToInt(playerRig.color.r * 9)
        int g = Mathf.RoundToInt(playerRig.color.g * 9)
        int b = Mathf.RoundToInt(playerRig.color.b * 9)

        s.Append($"<color=#{ColorUtility.ToHtmlStringRGB(playerRig.color)}>██ </color>");
        s.Append($"<color=red>{r}</color>");
        s.Append($"<color=green>{g}</color>");
        s.Append($"<color=blue>{b}</color>");

        // PID
        s.Append($"<color=red>{playerRig.OwningNetPlayer.UserId}</color>");

        tmpro.text = s.ToString();
    }

    static void UpdateModsNametag(TextMeshPro tmpro, VRRig rig)
    {
        StringBuilder nametag = new StringBuilder();

        foreach (string prop in rig.OwningNetPlayer.GetPlayerRef().CustomProperties.Keys)
            if (mods.TryGetValue(prop.ToLower(), out string realModName))
                nametag.Append($"[{realModName}]")

        tmpro.text = nametag.ToString();
    }
    
    void Start()
    {
        PluginManager.AddPluginUpdate(UpdateNametag, 1.2f, true);      // color code, pid
        PluginManager.AddPluginUpdate(UpdateModsNametag, 0.6f, true);  // prop checker
        
        GorillaTagger.OnPlayerSpawned += OnGorillaInit;
    }

    private const string GorillaInfoURL = "https://raw.githubusercontent.com/HanSolo1000Falcon/GorillaInfo/main/";

    void OnGorillaInit()
    {
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage knownModsResponse = httpClient.GetAsync(GorillaInfoURL + "KnownMods.txt").Result;
            
            knownModsResponse.EnsureSuccessStatusCode();

            using (Stream stream = knownModsResponse.Content.ReadAsStreamAsync().Result)
            using (StreamReader reader = new StreamReader(stream))
                mods = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
        }

        foreach (string prop in mods.Keys) {
            mods[prop.ToLower()] = mods[prop];
            mods.Remove(prop); // remove OG thing
        }
    }
}
