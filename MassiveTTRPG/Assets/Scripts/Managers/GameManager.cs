using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UserData currentUser;
    public GameData currentGame;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    public bool IsGM()
    {
        return currentGame.gmUserIds.Contains(currentUser.userId);
    }

    public bool IsPlayer()
    {
        return currentGame.playerUserIds.Contains(currentUser.userId);
    }

    public void CreateGame(string gameName)
    {
        currentGame = new GameData
        {
            gameId = System.Guid.NewGuid().ToString(),
            gameName = gameName,
            gmUserIds = new List<string> { currentUser.userId },
            userNames = new Dictionary<string, string>
            {
                { currentUser.userId, currentUser.username }
            }
        };

        if (!currentUser.gameIdsCreated.Contains(currentGame.gameId))
        {
            currentUser.gameIdsCreated.Add(currentGame.gameId);
            currentUser.gameIdsJoined.Add(currentGame.gameId);
        }

        SaveManager.SaveUser(currentUser);
        SaveManager.SaveGame(currentGame);
    }

    public void JoinGame(GameData game)
    {
        currentGame = game;

        if (!game.playerUserIds.Contains(currentUser.userId))
            game.playerUserIds.Add(currentUser.userId);

        if (!game.userNames.ContainsKey(currentUser.userId))
            game.userNames[currentUser.userId] = currentUser.username;

        if (!currentUser.gameIdsJoined.Contains(game.gameId))
            currentUser.gameIdsJoined.Add(game.gameId);

        SaveManager.SaveUser(currentUser);
        SaveManager.SaveGame(currentGame);
    }

    public void SaveCurrentState()
    {
        SaveManager.SaveUser(currentUser);
        SaveManager.SaveGame(currentGame);
    }
}
