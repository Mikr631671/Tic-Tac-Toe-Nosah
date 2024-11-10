using System.Collections.Generic;

public class LobbyScreenModel
{
    public string PlayerName { get; set; }
    public int PlayerLevel { get; set; }
    public List<string> AvailableGameModes { get; set; }

    public LobbyScreenModel()
    {
        // Data stub; in a real situation, data may be loaded from a server

        PlayerLevel = 5;
        AvailableGameModes = new List<string> { "PvP", "PvE", "EvE" };
    }
}
