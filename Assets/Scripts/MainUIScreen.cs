using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class MainUIScreen : MonoBehaviour
{
	public Text Score;
	public Text RoundResult;
	public Button StartNewGame;
	public Toggle HonestToogle;
	public Slider Probability;
	public Text ProbabilityLabel;

	public Button PlayerHandRock;
	public Button PlayerHandPaper;
	public Button PlayerHandScissors;

	public Button AiHandRock;
	public Button AiHandPaper;
	public Button AiHandScissors;
	
	private void Start()
	{
		CheckBindings();
		
		Probability.onValueChanged.AddListener(
			probability => ProbabilityLabel.text = (probability / 100).ToString());

		HonestToogle.onValueChanged.AddListener(
			toogle =>
			{
				Probability.gameObject.SetActive(!toogle);
				ProbabilityLabel.gameObject.SetActive(!toogle);
			});
	}

	private void CheckBindings()
	{
		Assert.IsTrue(Score != null, "Results label not assigned!");
		Assert.IsTrue(RoundResult != null, "RoundResult not assigned!");
		Assert.IsTrue(StartNewGame != null, "StartNewGame button not assigned!");
		Assert.IsTrue(HonestToogle != null, "Honest toogle not assigned!");
		Assert.IsTrue(Probability != null, "Probability slider not assigned!");
		Assert.IsTrue(ProbabilityLabel != null, "ProbabilityLabel not assigned!");
		
		Assert.IsTrue(PlayerHandRock != null, "PlayerHandRock not assigned!");
		Assert.IsTrue(PlayerHandPaper != null, "PlayerHandPaper not assigned!");
		Assert.IsTrue(PlayerHandScissors != null, "PlayerHandScissors not assigned!");
		
		Assert.IsTrue(AiHandRock != null, "AiHandRock not assigned!");
		Assert.IsTrue(AiHandPaper != null, "AiHandPaper not assigned!");
		Assert.IsTrue(AiHandScissors != null, "AiHandScissors not assigned!");
	}

	public void HighlighAiHand(EMoveSet move)
	{
		switch (move)
		{
			case EMoveSet.Rock:
				AiHandRock.Select();
				break;
			case EMoveSet.Paper:
				AiHandPaper.Select();
				break;
			case EMoveSet.Scissors:
				AiHandScissors.Select();
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(move), move, null);
		}
	}
}
