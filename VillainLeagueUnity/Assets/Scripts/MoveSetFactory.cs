using UnityEngine;
using System.Collections.Generic;

public static class MoveSetFactory
{
    public static CharacterMoveSet CreateCeceliaSylvanMoveSet()
    {
        CharacterMoveSet moveSet = ScriptableObject.CreateInstance<CharacterMoveSet>();
        moveSet.characterName = "Cecelia Sylvan";
        moveSet.role = "Mobile swordswoman / skirmisher / protector";
        moveSet.resource = new CharacterResource("Focus", 6, 1);
        moveSet.moves = new List<Move>();
        
        // Twin Slash
        Move twinSlash = new Move("cece_basic_slash", "Twin Slash", 
            "Deal physical damage with a quick two-hit sword combo.", 0);
        twinSlash.damage = 3;
        twinSlash.hits = 2;
        moveSet.moves.Add(twinSlash);
        
        // Blink Strike
        Move blinkStrike = new Move("cece_teleport_strike", "Blink Strike",
            "Teleport to a target and strike. Ignores Guard/Block effects on the target this hit.", 2);
        blinkStrike.damage = 5;
        blinkStrike.ignoreGuard = true;
        moveSet.moves.Add(blinkStrike);
        
        // Flash Step
        Move flashStep = new Move("cece_reposition", "Flash Step",
            "Teleport to any empty tile within range. Gain Evasion for 1 turn.", 1);
        flashStep.moveRange = 5;
        flashStep.evasion = 2;
        flashStep.durationTurns = 1;
        flashStep.targetType = MoveTargetType.Self;
        moveSet.moves.Add(flashStep);
        
        // Hero's Intercept
        Move heroIntercept = new Move("cece_guard_swap", "Hero's Intercept",
            "Teleport to an ally and take the next hit meant for them. Gain temporary Armor for 1 turn.", 2);
        heroIntercept.armor = 3;
        heroIntercept.durationTurns = 1;
        heroIntercept.targetType = MoveTargetType.SingleAlly;
        moveSet.moves.Add(heroIntercept);
        
        // Rally Heart
        Move rallyHeart = new Move("cece_rally", "Rally Heart",
            "Inspire allies in a small area, increasing their Attack for 2 turns.", 2);
        rallyHeart.attackBuff = 2;
        rallyHeart.radius = 2;
        rallyHeart.durationTurns = 2;
        rallyHeart.targetType = MoveTargetType.AllAllies;
        moveSet.moves.Add(rallyHeart);
        
        // Severing Cut
        Move severingCut = new Move("cece_precision_cut", "Severing Cut",
            "Deal damage and apply Bleed (damage over time).", 1);
        severingCut.damage = 4;
        severingCut.bleed = 2;
        severingCut.durationTurns = 2;
        moveSet.moves.Add(severingCut);
        
        // Swordbind
        Move swordbind = new Move("cece_disarm", "Swordbind",
            "Deal light damage and reduce target Attack for 2 turns.", 1);
        swordbind.damage = 2;
        swordbind.attackDebuff = 2;
        swordbind.durationTurns = 2;
        moveSet.moves.Add(swordbind);
        
        // Piercing Lunge
        Move piercingLunge = new Move("cece_piercing_lunge", "Piercing Lunge",
            "Deal damage that partially ignores Armor.", 2);
        piercingLunge.damage = 5;
        piercingLunge.armorPierce = 2;
        moveSet.moves.Add(piercingLunge);
        
        // Blade Whirl
        Move bladeWhirl = new Move("cece_blade_whirl", "Blade Whirl",
            "Spin attack that hits all enemies adjacent to Cecelia.", 2);
        bladeWhirl.damage = 3;
        bladeWhirl.radius = 1;
        bladeWhirl.targetType = MoveTargetType.AllEnemies;
        moveSet.moves.Add(bladeWhirl);
        
        // Afterimage
        Move afterimage = new Move("cece_afterimage", "Afterimage",
            "Gain Evasion and Counter for 1 turn.", 2);
        afterimage.evasion = 2;
        afterimage.counterDamage = 3;
        afterimage.durationTurns = 1;
        afterimage.targetType = MoveTargetType.Self;
        moveSet.moves.Add(afterimage);
        
        return moveSet;
    }
    
    public static CharacterMoveSet CreateDefaultMoveSet(string characterName)
    {
        CharacterMoveSet moveSet = ScriptableObject.CreateInstance<CharacterMoveSet>();
        moveSet.characterName = characterName;
        moveSet.role = "Fighter";
        moveSet.resource = new CharacterResource("Energy", 10, 2);
        moveSet.moves = new List<Move>();
        
        // Basic Attack
        Move basicAttack = new Move("basic_attack", "Basic Attack", 
            "A simple physical attack.", 0);
        basicAttack.damage = 5;
        moveSet.moves.Add(basicAttack);
        
        // Power Strike
        Move powerStrike = new Move("power_strike", "Power Strike",
            "A powerful attack that costs energy.", 3);
        powerStrike.damage = 10;
        moveSet.moves.Add(powerStrike);
        
        // Defend
        Move defend = new Move("defend", "Defend",
            "Increase defense temporarily.", 1);
        defend.defenseBuff = 3;
        defend.durationTurns = 1;
        defend.targetType = MoveTargetType.Self;
        moveSet.moves.Add(defend);
        
        return moveSet;
    }
}
