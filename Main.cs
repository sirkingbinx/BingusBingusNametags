using BepInEx;
using BingusNametags.Plugins;
using TMPro;
using UnityEngine;
using System.Text;

[BepInDependency("bingus.nametags", DependencyFlags.HardDependency)]
[BepInPlugin("bingus.grungus", "BingusBingusNametags", "1.0.0")]
public class NametagLoader : BaseUnityPlugin
{
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

        s.Append(" <color=gray>");

        // Special cosmetics
        string cosmetics = playerRig.concatStringOfCosmeticsAllowed;

        if (playerRig.GetPlayerRef() == NetworkSystem.Instance.MasterClient)
            s.Append("[MSR] ");
        if (rig.concatStringOfCosmeticsAllowed.Contains("LBADE."))
            s.Append("<color=red>[FG</color><color=blue>P]</color> ");
        if (rig.concatStringOfCosmeticsAllowed.Contains("LBAGS."))
            s.Append("<color=orange>[ILS]</color> ");
        if (rig.concatStringOfCosmeticsAllowed.Contains("LBAAD."))
            s.Append("<color=white>[ADM]</color> ");
        if (rig.concatStringOfCosmeticsAllowed.Contains("LBAAK."))
            s.Append("<color=yellow>[DEV]</color> ");
        if (rig.Creator.GetPlayerRef().CustomProperties.Count > 1)
            s.Append("[MDR] ");
        
        s.Append("</color>");

        tmpro.text = s.ToString();
    }
    
    void Start() {
        PluginManager.AddPluginUpdate(UpdateNametag, 1.2f, true);
    }
}
