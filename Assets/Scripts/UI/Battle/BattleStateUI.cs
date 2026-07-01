using DG.Tweening;
using Game.Gameplay;
using Game.Managers;
using TMPro;
using UnityEngine;

public class BattleStateUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup battleUICanvasGroup;
    [SerializeField] private TMP_Text battleStateText;

    private Sequence runningTweenSequence;
    private Vector3 initialPos;

    private void OnEnable() {
        GameEventManager.Instance.BattleEvent.OnBattleStateChanged += HandleBattleStateChanged;
    }

    private void OnDisable() {
        GameEventManager.Instance.BattleEvent.OnBattleStateChanged -= HandleBattleStateChanged;
        
        if(runningTweenSequence != null && runningTweenSequence.IsActive()) {
            runningTweenSequence.Kill();
        }
    }

    private void Start() {
        battleUICanvasGroup.alpha = 0f;
        initialPos = transform.position;
    }

    private void HandleBattleStateChanged(BattleState state) {
        transform.position = initialPos;
        battleUICanvasGroup.alpha = 0f;
        
        battleStateText.text = state.ToString().Replace("_", " ").ToUpper();

        if(runningTweenSequence != null && runningTweenSequence.IsActive()) {
            runningTweenSequence.Kill();
        }
        
        runningTweenSequence = DOTween.Sequence();
        
        // Run Fade and Move tween at the same time
        runningTweenSequence.Append(battleUICanvasGroup.DOFade(1f, Game.Const.Tween.FADE_DURATION)
            .SetEase(Ease.OutQuad));
        
        runningTweenSequence.Join(transform.DOMoveY(initialPos.y + Game.Const.Tween.MOVE_OFFSET_Y, Game.Const.Tween.MOVE_DURATION)
            .SetEase(Ease.OutQuad)
        );

        // Run fade tween after the previous sequence is finished
        runningTweenSequence.Append(battleUICanvasGroup.DOFade(0f, Game.Const.Tween.FADE_DURATION)
            .SetEase(Ease.InQuad));
    }
}
