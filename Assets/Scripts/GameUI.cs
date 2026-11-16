using TMPro;
using UnityEngine;

public static class GameUI
{
    private static float ActiveAccuracyFontSize = 50f;
    private static float InactiveAccuracyFontSize = 35f;
    private static Color ActiveAccuracyColor = Color.black;
    private static Color InactiveAccuracyColor = Color.gray;

    public static void UpdateInningDisplay(int inning)
    {
        TMP_Text text = FindText("CurrentInning-Text");
        if (text != null) text.text = $"Inning: {inning}";
    }

    public static void UpdatePlayerAccuracyDisplay(bool playerOne, int accuracyPercent)
    {
        string objectName = playerOne ? "PlayerOneAccuracy-Text" : "PlayerTwoAccuracy-Text";
        TMP_Text text = FindText(objectName);
        if (text != null)
        {
            text.text = $"Accuracy: {accuracyPercent}%";
            SetAccuracyStyle(text, true);
        }
    }

    public static void UpdatePitchingAndBattingAccuracyTextStyles(bool playerOneIsPitching)
    {
        TMP_Text playerOneText = FindText("PlayerOneAccuracy-Text");
        TMP_Text playerTwoText = FindText("PlayerTwoAccuracy-Text");
        if (playerOneText != null) SetAccuracyStyle(playerOneText, !playerOneIsPitching);
        if (playerTwoText != null) SetAccuracyStyle(playerTwoText, playerOneIsPitching);
    }

    public static void UpdatePlayerAccuracyDisplays(int playerOneAccuracyPercent, int playerTwoAccuracyPercent)
    {
        UpdatePlayerAccuracyDisplay(true, playerOneAccuracyPercent);
        UpdatePlayerAccuracyDisplay(false, playerTwoAccuracyPercent);
    }

    public static void ShowFinalResults(int playerOneAccuracyPercent, int playerTwoAccuracyPercent,
        bool playerOneHasHigherAccuracy, bool tied)
    {
        TMP_Text winnerText = FindText("Winner-Text");
        if (winnerText != null)
        {
            winnerText.text = tied ? "It's a tie!" : playerOneHasHigherAccuracy ? "Player One Wins!" : "Player Two Wins!";
        }

        TMP_Text playerOneText = FindText("PlayerOneFinalAccuracy-Text");
        if (playerOneText != null) playerOneText.text = $"Player One Final Accuracy: {playerOneAccuracyPercent}%";

        TMP_Text playerTwoText = FindText("PlayerTwoFinalAccuracy-Text");
        if (playerTwoText != null) playerTwoText.text = $"Player Two Final Accuracy: {playerTwoAccuracyPercent}%";
    }

    private static TMP_Text FindText(string objectName)
    {
        foreach (TMP_Text text in Object.FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (text.gameObject.name == objectName) return text;
        }

        return null;
    }

    private static void SetAccuracyStyle(TMP_Text text, bool isBatting)
    {
        text.fontSize = isBatting ? ActiveAccuracyFontSize : InactiveAccuracyFontSize;
        text.color = isBatting ? ActiveAccuracyColor : InactiveAccuracyColor;
    }
}
