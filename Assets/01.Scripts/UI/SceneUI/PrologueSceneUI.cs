using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CutElement
{
    public PictureCut[] pcs;
}

public class PrologueSceneUI : SceneUIBase
{
    [SerializeField] private Transform _spawnParent;
    [SerializeField] private List<CutElement> _pictureCutBook = new List<CutElement>();
    private List<PictureCut> _savedInPhasePictureCutList = new List<PictureCut>();
    private int cutCount;
    private int phase;

    public bool canNextUp;
    private bool _isClearPhase;

    public override void SetUp()
    {
        PlayCutPicture();
    }

    private void PlayCutPicture()
    {
        if (canNextUp)
        {
            if (phase == _pictureCutBook.Count)
            {
                UIManager.Instanace.ChangeScene(UIDefine.UIType.Title);
                return;
            }
                canNextUp = false;

            if (_isClearPhase)
            {
                foreach (PictureCut pc in _savedInPhasePictureCutList)
                {
                    Destroy(pc.gameObject);
                }
                _savedInPhasePictureCutList.Clear();
                _isClearPhase = false;
            }

            PictureCut p = Instantiate(_pictureCutBook[phase].pcs[cutCount], _spawnParent);
            p.SetUpPictureCut(this);
            _savedInPhasePictureCutList.Add(p);

            cutCount++;

            if (cutCount == _pictureCutBook[phase].pcs.Length)
            {
                cutCount = 0;
                _isClearPhase = true;
                phase++;
            }
        }
    }

    private void Update()
    {
        if(Input.anyKeyDown)
            PlayCutPicture();
    }

    public override void Init()
    {
    }
}
