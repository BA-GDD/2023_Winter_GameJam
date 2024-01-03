using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[System.Serializable]
public class PictureCut : MonoBehaviour
{
    [SerializeField] private Image _pictureCut;
    [SerializeField] private Image _pictureBase;
    private float _easingTime = 0.5f;

    public void SetUpPictureCut(PrologueSceneUI myBook)
    {
        _pictureCut.color = new Color(1, 1, 1, 0);
        _pictureBase.color = new Color(1, 1, 1, 0);

        Sequence seq = DOTween.Sequence();
        seq.Append(_pictureCut.DOFade(1, _easingTime));
        seq.Join(_pictureBase.DOFade(1, _easingTime));
        seq.AppendCallback(() =>
        {
            myBook.canNextUp = true;
        });
    }
}
