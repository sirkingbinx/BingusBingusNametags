using System.Text;
using BepInEx;
using BingusNametags.Plugins;
using UnityEngine;

namespace BingusBingusTags
{
    [BepInDependency("bingus.nametags")]
    [BepInPlugin("bingus.bingus.nametags", "BingusBingusNametags", "1.0.0")]
    public class NametagInit : BaseUnityPlugin
    {
        private void Start() => Debug.Log("hi");
    }

    [BingusNametagsPlugin("ColorCode_PID", 1.2f)]
    public class ColorCodePid : INametag
    {
        public bool Enabled { get; set; } = true;
        public string Update(VRRig owner)
        {
            var s = new StringBuilder();

            // Color code (square with the color, and then color code itself)
            var r = Mathf.RoundToInt(owner.playerColor.r * 9);
            var g = Mathf.RoundToInt(owner.playerColor.g * 9);
            var b = Mathf.RoundToInt(owner.playerColor.b * 9);

            s.Append($"<color=#{ColorUtility.ToHtmlStringRGB(owner.playerColor)}>██ </color>");
            s.Append($"<color=red>{r}</color>");
            s.Append($"<color=green>{g}</color>");
            s.Append($"<color=blue>{b}</color>");
            
            // playerId
            s.Append($"  {owner.OwningNetPlayer.UserId.Substring(0, 5)}<color=grey>..</color>");

            return s.ToString();
        }
    }
}
