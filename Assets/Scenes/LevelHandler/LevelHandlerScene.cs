using UnityEngine;
using System.Collections;

namespace RunAndJump {
	
	public class LevelHandlerScene : BaseScene {

		#region EDITOR VARIABLES
		
		[Header ("UI Panel Elements")]
		public TransitionPanel Transition;
		public GameInfoPanel GameInfo;
		public VirtualInputPanel VirtualInput;
		public PauseMenuPanel PauseMenu;
		public HintMessagePanel HintMessage;
		public GoalMenuPanel GoalMenu;

		#endregion

		#region CONSTANTS

		private enum GameState {
			Initializing = 0,
			Playing,
			Paused,
			Win,
			Lose,
		}

		private const int TOTAL_LIVES = 4;
		private const int COIN_SCORE = 100;
		private const int TREASURE_SCORE = 1000;

		#endregion

		#region VARIABLES

		private GameState _gameState = GameState.Initializing;
		private int _score;
		private int _lives;
		private float _time;
		private int _timeInSeconds;
		private int _treasuresCollected;
		private string _sceneName;
		private string _levelName;
		private int _levelId;

		#endregion

		#region MONOBEHAVIOUR METHODS

		protected override void Awake () {
			base.Awake ();
		}

		private void Start () {
			StartCoroutine(InitSequence());
		}

		private void Update () {
			if(_gameState == GameState.Playing) {
				UpdateTimerCountdown();
				UpdateGameInfoUI();
			}
		}

		private void OnDestroy() {
			UnSubscribeLevelElementsEvents();
		}

		#endregion

		private IEnumerator InitSequence() {
			_lives = TOTAL_LIVES;
			SubscribeLevelElementsEvents ();
			yield return StartCoroutine(InitScene());
		}

		private IEnumerator InitScene() {
			_score = 0;
			_treasuresCollected = 0;
			_time = 100;// Session.Instance.GetLevelMetadata().TotalTime;
			_levelId = Session.Instance.GetLevelId();
			_sceneName = Session.Instance.GetLevelMetadata().SceneName;
			_levelName = Session.Instance.GetLevelMetadata().LevelName;
			InputWrapper.Instance.EnableInput (false);

			HideAllThePanels ();
			Transition.gameObject.SetActive(true);
			Transition.DisplayIntro (true);
			Transition.DisplayGameOver (false);
			Transition.SetIntro (_levelId, _levelName, _lives);

			LevelHandlerUtils.DestroyLevel();
			
			yield return StartCoroutine( LevelHandlerUtils.LoadLevel(_sceneName));
			yield return new WaitForSeconds(1.5f);

			HideAllThePanels ();
			GameInfo.gameObject.SetActive(true);
			VirtualInput.gameObject.SetActive(true);
			HintMessage.gameObject.SetActive(true);
			UpdateGameInfoUI();
			InputWrapper.Instance.EnableInput (true);
			_gameState = GameState.Playing;
		}

		private void HideAllThePanels() {
			Transition.gameObject.SetActive(false);
			GameInfo.gameObject.SetActive(false);
			VirtualInput.gameObject.SetActive(false);
			HintMessage.gameObject.SetActive(false);
			GoalMenu.gameObject.SetActive(false);
			PauseMenu.gameObject.SetActive (false);
		}

		private void SubscribeLevelElementsEvents () {
			PlayerController.PlayerDeathEvent += new PlayerController.PlayerDeathDelegate(RestartLevel);
			InteractiveSignController.StartInteractionEvent += new InteractiveSignController.StartInteractionDelegate(DisplayHint);
			InteractiveSignController.StopInteractionEvent += new InteractiveSignController.StopInteractionDelegate(HideHint);
			InteractiveCoinController.StartInteractionEvent += new InteractiveCoinController.StartInteractionDelegate(CollectCoin);
			InteractiveTreasureController.StartInteractionEvent += new InteractiveTreasureController.StartInteractionDelegate(CollectTreasure);
			InteractiveGoalFlagController.StartInteractionEvent += new InteractiveGoalFlagController.StartInteractionDelegate(LevelFinish);
		}

		private void UnSubscribeLevelElementsEvents () {
			PlayerController.PlayerDeathEvent -= new PlayerController.PlayerDeathDelegate(RestartLevel);
			InteractiveSignController.StartInteractionEvent -= new InteractiveSignController.StartInteractionDelegate(DisplayHint);
			InteractiveSignController.StopInteractionEvent -= new InteractiveSignController.StopInteractionDelegate(HideHint);
			InteractiveCoinController.StartInteractionEvent -= new InteractiveCoinController.StartInteractionDelegate(CollectCoin);
			InteractiveTreasureController.StartInteractionEvent -= new InteractiveTreasureController.StartInteractionDelegate(CollectTreasure);
			InteractiveGoalFlagController.StartInteractionEvent -= new InteractiveGoalFlagController.StartInteractionDelegate(LevelFinish);
		}

		private void UpdateGameInfoUI() {
			GameInfo.SetScore(_score);
			GameInfo.SetLives(_lives);
			GameInfo.SetTime(_timeInSeconds);
		}

		private void UpdateTimerCountdown() {
			_time = _time - UnityEngine.Time.deltaTime;
			_timeInSeconds = Mathf.FloorToInt(_time);
		}

		#region LEVEL ELEMENTS EVENTS

		private void LevelFinish () {
			_gameState = GameState.Win;
			Time.timeScale = 0;
			GoalMenu.SetTime(_timeInSeconds);
			GoalMenu.SetScore(_score);
			GoalMenu.SetTreasures(_treasuresCollected);
			HideAllThePanels ();
			GoalMenu.EnableNext(Session.Instance.HasNext());
			GoalMenu.gameObject.SetActive(true);
		}

		private void CollectTreasure () {
			_score += TREASURE_SCORE;
			_treasuresCollected++;
		}

		private void CollectCoin () {
			_score += COIN_SCORE;
		}

		private void DisplayHint(string message) {
			HintMessage.SetMessage(message);
			HintMessage.Show();
		}

		private void HideHint() {
			HintMessage.Hide();
		}

		private void RestartLevel() {
			Debug.Log("RestartLevel called!");
			_gameState = GameState.Lose;
			_lives--;
			if(_lives <= 0) {
				StartCoroutine(GameOver());
			} else {
				HideAllThePanels();
				Transition.gameObject.SetActive(true);
				Transition.DisplayIntro (true);
				Transition.DisplayGameOver (false);
				Transition.SetIntro (_levelId, _levelName, _lives);
				LevelHandlerUtils.DestroyLevel();
				StartCoroutine(InitScene());
			}
		}

		private void LoadNextLevel() {
			HideAllThePanels();
			Session.Instance.PlayNext();
			StartCoroutine(InitScene());
		}

		private IEnumerator GameOver() {
			HideAllThePanels ();
			Transition.gameObject.SetActive(true);
			Transition.DisplayIntro (false);
			Transition.DisplayGameOver (true);
			LevelHandlerUtils.DestroyLevel();
			yield return new WaitForSeconds (2);
			GoToScene(Scene.Title);
		}

		#endregion

		#region ONCLICK EVENTS

		// All the events of the buttons, except VirtualInput, are handled here.

		public void PauseButtonOnClick() {
			Debug.Log("PauseButtonOnClick called...");
			_gameState = GameState.Paused;
			Time.timeScale = 0;
			PauseMenu.gameObject.SetActive(true);
			GameInfo.gameObject.SetActive(false);
			VirtualInput.gameObject.SetActive(false);
		}

		public void QuitButtonOnClick() {
			Debug.Log("QuitButtonOnClick called...");
			Time.timeScale = 1;
			GoToScene(Scene.Title);
		}

		public void PlayButtonOnClick() {
			Debug.Log("PlayButtonOnClick called...");
			Time.timeScale = 1;
			PauseMenu.gameObject.SetActive(false);
			GameInfo.gameObject.SetActive(true);
			VirtualInput.gameObject.SetActive(true);
			_gameState = GameState.Playing;
		}

		public void NextButtonOnClick() {
			Debug.Log("NextButtonOnClick called...");
			Time.timeScale = 1;
			LoadNextLevel();
		}
	
		#endregion
	}
}