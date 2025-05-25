using System.IO;
using UnityEngine;

public static class SaveManager
{

    private static string userPath => Path.Combine(Application.persistentDataPath, "user.json");
    private static string gamePath => Path.Combine(Application.persistentDataPath, "game.json");

    public static void SaveUser(UserData user)
    {
        string path = Path.Combine(Application.persistentDataPath, $"user_{user.userId}.json");
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(path, json);
    }

    public static UserData LoadUser(string userId)
    {
        string path = Path.Combine(Application.persistentDataPath, $"user_{userId}.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<UserData>(json);
        }
        return null;
    }

    public static void SaveGame(GameData game)
    {
        string path = Path.Combine(Application.persistentDataPath, $"game_{game.gameId}.json");
        string json = JsonUtility.ToJson(game, true);
        File.WriteAllText(path, json);
    }

    public static GameData LoadGame(string gameId)
    {
        string path = Path.Combine(Application.persistentDataPath, $"game_{gameId}.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }
}
