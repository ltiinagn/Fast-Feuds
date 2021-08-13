using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public string[] levelNames = {"Level0-T", "Level1-1", "Level1-2", "Level1-3", "Level1-B", "Level2-1", "Level2-2", "Level2-B", "Level3-B"};
    public string[] rowNames = {"Tiles/Row1", "Tiles/Row2", "Tiles/Row3", "Tiles/Row4"};

    public int powerupVisibilityDuration = 5;
    public int powerupVisibilityDuration2 = 3; // for invulnerable
    public int powerupDisappearDuration = 3;

    public GameObject powerupWeaponPrefab;
    public GameObject powerupAddHealthPrefab;
    public GameObject powerupInvulnerablePrefab;
    public GameObject powerupDestroyAllEnemiesPrefab;
    public GameObject powerupDestroyAllProjectilesPrefab;

    // public Vector3[] offTileSpawnPoints = {}

    public int invulnerablePowerupDuration = 5;

    public string[] dialogueDummy = {};
    public string[][] dialogue0_T = new string[][] {
        new string[] {"Man I only wanted a break from chicken and brocc.", "I could probably burn some cals and move to a tile by pressing its corresponding key!"},
        new string[] {"I should pick up that weapon!"},
        new string[] {"What's this salty greasy smell? More fries?", "Wait no, THEY'RE MY STUDENTS!"},
        new string[] {"I can't reverse this feud junkies... So I guess I'mma just do what PE teachers do best.","Putting Physical back into education!", "LET'S DO THIS!"}
    };

    public string[][] dialogue1_1 = new string[][] {
        new string[] {"What are those? Chic..mera?", "Chicmera? Yeah let's go witht hat"},
        new string[] {"Man that sour smell straight up came from pits of a clown or WHAT."},
        new string[] {"I swear the patties are getting smaller by the year. Uhm I mean...","oh no these are the colleagues I give a damn about..."},
        new string[] {"OH NO MY STUD...","Wait... they're the class of kids whose parents are elitist highrollers...","...","SMASH THESE TIKTOK FREAKS LESGOOOO"},
        new string[] {"EASY GREASY LEMON ON MAH NINNIES!"}
    };

    public string[][] dialogue1_2 = new string[][] {
        new string[] {"The Straight Cut Fry I picked up earlier is kinda giving me A LOT of energy","Ah more junkies!"},
        new string[] {"Even more of them?!", "No problemo!"},
        new string[] {"I think that's it for now..."}
    };
    public string[][] dialogue1_B = new string[][] {
        new string[] {"Vice President Mike?","More like..","Mike vice is gluttony, am I right?"},
        new string[] {"I AM INVINCIBLE!!!" , "AND I THOUGHT YOU WERE PALEOOOOO!!!"},
    };

    public string[][] dialogue2_1 = new string[][] {
        new string[] {"To be honest, I'm more of a savoury person. But I love my sweets too"},
        new string[] {"Dude it's so weird how they open up this Dessert Exhibition and people just straight up turn into mutated freaks!","It's only 2019! Cant get any worse than this!"},
        new string[] {"These guys smell like black forest and a very wet bloodhound..."},
        new string[] {"Quick question,","Krispy Kreme or Dunkin Donuts?"},
        new string[] {"Hey it's you!","...sorry I forgot your name.. but it seems like you made fwens!","Nice to BEAT YOU!"}
    };
    public string[][] dialogue2_2 = new string[][] {
        new string[] {"Hi Tea Buffet 22 bucks per SMACKS!"},
        new string[] {"More cakes needs POUNDING!"},
        new string[] {"I think that's it for now... hmm?"}
    };
    public string[][] dialogue2_B = new string[][] {
        new string[] {"Lunch lady? Wait, that bubbletea for lunch and breakfast reserve teach? No." , " Mom?", " What the..."},
        new string[] {"Dude, I really miss my mom...","She used to make me this huge birthday cake that I could never finish...","God I miss her so much. And then I just find myself binging away the pain..."},
    };

    public string[][] dialogue3_B = new string[][] {
        new string[] {"You must be the cause of it all." , "They used to make cough syrup out of you did'nt they?", "And now you're just doing what you were supposed to do in the first place.","But it ends now.","Time to make soda POP."},
        new string[] {"Finally!","Though I dont think that this is the end..", "I'm really either high on shroom muffins or the Fast Feud Capitalist are finally done waiting..."},
    };
}