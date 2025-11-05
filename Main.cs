using BepInEx;
using BingusNametags.Plugins;
using TMPro;
using UnityEngine;

[BepInDependency("bingus.nametags", DependencyFlags.HardDependency)]
[BepInPlugin("myname.mynametag", "MyNametag", "1.0.0")]
public class NametagLoader : BaseUnityPlugin
{
    static void UpdateNametag(TextMeshPro tmpro, VRRig playerRig)
    {
        // You can manipulate the TMPro however you want, note that the default color is the accent color (which you change with the color tags)
        tmpro.text = $"{playerRig.OwningNetPlayer.NickName}";
    }
    
    void Start() {
        // The default nametags take up 0.8f (platform) and 1f (name) offsets
        // The arguments: (Update function, nametag offset, use the accent color (blue by defualt))
        PluginManager.AddPluginUpdate(UpdateNametag, 1.2f, true);
    }
}
