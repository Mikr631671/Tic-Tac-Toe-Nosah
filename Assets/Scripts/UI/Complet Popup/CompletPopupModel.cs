using System;

public class CompletPopupModel
{
    private PlayerSymbolType? winningSymbol;
    private TimeSpan gameDuration;
    private string completWinTitleFormat = "Player {0} won\n in {1:D2}:{2:D2}!";
    private string completDrawTitleFormat = "Draw\n in {0:D2}:{1:D2}!";

    public CompletPopupModel(PlayerSymbolType? winningSymbol, TimeSpan gameDuration)
    {
        this.winningSymbol = winningSymbol;
        this.gameDuration = gameDuration;
    }

    public string GetFormattedTitle()
    {
        var result = (winningSymbol == null) ?
            string.Format(completDrawTitleFormat, gameDuration.Minutes, gameDuration.Seconds) :
            string.Format(completWinTitleFormat, winningSymbol, gameDuration.Minutes, gameDuration.Seconds);

        return result;
    }
}
