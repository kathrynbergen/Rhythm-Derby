using TMPro;
using UnityEngine;

public static class GameUI
{
    private static float ActiveAccuracyFontSize = 50f;
    private static float InactiveAccuracyFontSize = 35f;
    private static Color ActiveAccuracyColor = Color.white;
    private static Color InactiveAccuracyColor = Color.black;

    public static void UpdateComboDisplay(bool playerOne, int combo)
    {
        string objectName = playerOne ? "PlayerOneCombo-Text" : "PlayerTwoCombo-Text";
        TMP_Text text = FindText(objectName);
        if (text != null)
        {
            text.text = $"Combo: {combo}";
        }
    }
    
    public static void UpdateInningDisplay(int inning)
    {
        TMP_Text text = FindText("CurrentInning-Text");
        if (text != null)
        {
            if (inning == 1) text.text = $"{inning}st Inning";
            else if (inning == 2) text.text = $"{inning}nd Inning";
            else if (inning == 3) text.text = $"{inning}rd Inning";
            else text.text = $"{inning}th Inning";
        }
    }

    public static void UpdateStrikesDisplay(int strikes)
    {
        TMP_Text text = FindText("Strikes-Text");
        if (text != null)
        {
            text.text = $"Strikes: {strikes}";
        }
    }

    public static void UpdatePlayerAccuracyDisplay(bool playerOne, int accuracyPercent)
    {
        string objectName = playerOne ? "PlayerOneAccuracy-Text" : "PlayerTwoAccuracy-Text";
        TMP_Text text = FindText(objectName);
        if (text != null)
        {
            text.text = $"Accuracy: {accuracyPercent}%";
            SetTextStyle(text, true);
        }
    }

    public static void UpdatePitchingAndBattingAccuracyTextStyles(bool playerOneIsPitching)
    {
        TMP_Text playerOneAccuracyText = FindText("PlayerOneAccuracy-Text");
        TMP_Text playerTwoAccuracyText = FindText("PlayerTwoAccuracy-Text");
        TMP_Text playerOneComboText = FindText("PlayerOneCombo-Text");
        TMP_Text playerTwoComboText = FindText("PlayerTwoCombo-Text");
        if (playerOneAccuracyText != null) SetTextStyle(playerOneAccuracyText, !playerOneIsPitching);
        if (playerTwoAccuracyText != null) SetTextStyle(playerTwoAccuracyText, playerOneIsPitching);
        if (playerOneComboText != null) SetTextStyle(playerOneComboText, !playerOneIsPitching);
        if (playerTwoComboText != null) SetTextStyle(playerTwoComboText, playerOneIsPitching);
    }

    public static void UpdatePlayerAccuracyDisplays(int playerOneAccuracyPercent, int playerTwoAccuracyPercent)
    {
        UpdatePlayerAccuracyDisplay(true, playerOneAccuracyPercent);
        UpdatePlayerAccuracyDisplay(false, playerTwoAccuracyPercent);
    }

    public static void ShowFinalResults(int playerOneAccuracyPercent, int playerTwoAccuracyPercent,
        int playerOneHighestCombo, int playerTwoHighestCombo,
        bool playerOneHasHigherAccuracy, bool tied)
    {
        TMP_Text winnerText = FindText("Winner-Text");
        if (winnerText != null)
        {
            winnerText.text = tied ? "It's a tie!" : playerOneHasHigherAccuracy ? "Player One Wins!" : "Player Two Wins!";
        }

        TMP_Text playerOneText = FindText("PlayerOneFinalAccuracy-Text");
        if (playerOneText != null) playerOneText.text = $"Accuracy: {playerOneAccuracyPercent}%";

        TMP_Text playerTwoText = FindText("PlayerTwoFinalAccuracy-Text");
        if (playerTwoText != null) playerTwoText.text = $"Accuracy: {playerTwoAccuracyPercent}%";

        TMP_Text playerOneComboText = FindText("PlayerOneFinalCombo-Text");
        if (playerOneComboText != null) playerOneComboText.text = $"Highest Combo: {playerOneHighestCombo}";

        TMP_Text playerTwoComboText = FindText("PlayerTwoFinalCombo-Text");
        if (playerTwoComboText != null) playerTwoComboText.text = $"Highest Combo: {playerTwoHighestCombo}";
    }

    private static TMP_Text FindText(string objectName)
    {
        foreach (TMP_Text text in Object.FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (text.gameObject.name == objectName) return text;
        }

        return null;
    }

    private static void SetTextStyle(TMP_Text text, bool isBatting)
    {
        text.fontSize = isBatting ? ActiveAccuracyFontSize : InactiveAccuracyFontSize;
        text.color = isBatting ? ActiveAccuracyColor : InactiveAccuracyColor;
    }
}
