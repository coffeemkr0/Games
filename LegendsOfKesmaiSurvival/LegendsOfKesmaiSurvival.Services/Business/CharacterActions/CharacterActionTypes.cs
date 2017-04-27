using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.CharacterActions
{
    /// <summary>
    /// Represents all of the available types of actions a character can perform.
    /// </summary>
    public enum CharacterActionTypes
    {
        Say,
        Yell,
        Whisper,
        Move,
        Attack,
        WarmSpell,
        CastSpell,
        MakeNoise,
        InteractWithObject,
        PickupItem,
        DropItem,
        UseItem
    }
}
