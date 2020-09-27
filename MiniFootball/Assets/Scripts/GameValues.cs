
//Static klasa moze imati samo staticne clanove
public static class GameValues 
{
    //Enum za tezine
    public enum Difficulties { Easy, Medium, Hard };

    public static bool IsMultyplayer;
    public static Difficulties Difficulty = Difficulties.Easy;
}
