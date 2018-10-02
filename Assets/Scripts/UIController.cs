using UnityEngine;

public class UIController
{
	private MainUIScreen _mainUiScreen;
	private float _cheatPropability = -1;
	
	public UIController(Game game)
	{
		_mainUiScreen = Object.Instantiate(Resources.Load<MainUIScreen>("MainUIScreen"));

		game.OnAiMoveDone += aiMove => _mainUiScreen.HighlighAiHand(aiMove);
		game.OnRoundStarted += () => _mainUiScreen.RoundResult.text = string.Empty;
		game.OnRoundEnded += result => _mainUiScreen.RoundResult.text = result == null ? "Draw" : result == true ? "Win" : "Fail";
		game.OnScoreUpdated += results => _mainUiScreen.Score.text = results;
		
		_mainUiScreen.Probability.onValueChanged.AddListener(value => _cheatPropability = !_mainUiScreen.HonestToogle.isOn ? value / 100 : -1);
		_mainUiScreen.StartNewGame.onClick.AddListener(() => game.Start(_cheatPropability));
		_mainUiScreen.PlayerHandRock.onClick.AddListener(() => game.MakeRound(EMoveSet.Rock, _cheatPropability));
		_mainUiScreen.PlayerHandPaper.onClick.AddListener(() => game.MakeRound(EMoveSet.Paper, _cheatPropability));
		_mainUiScreen.PlayerHandScissors.onClick.AddListener(() => game.MakeRound(EMoveSet.Scissors, _cheatPropability));
	}
}
