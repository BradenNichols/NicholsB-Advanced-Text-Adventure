using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Advanced_Text_Adventure
{
    [Serializable]
    public struct SaveData
    {
        [JsonInclude] public bool canLoad;
        [JsonInclude] public int saveDataVersion;

        [JsonInclude] public int level;
        [JsonInclude] public int shopPoints;
    }
}
