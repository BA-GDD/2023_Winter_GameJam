using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public struct CutElement
{
    public PictureCut[] pcs;
}

public class PrologueSceneUI : SceneUIBase
{
    private Volume _volume;
    [SerializeField] private GameObject _skipBtn;
    [SerializeField] private Transform _spawnParent;
    [SerializeField] private List<CutElement> _pictureCutBook = new List<CutElement>();
    private List<PictureCut> _savedInPhasePictureCutList = new List<PictureCut>();
    private int cutCount;
    private int phase;

    public bool canNextUp;
    private bool _isClearPhase;

    [SerializeField] private AudioClip _bgmClip;

    public override void SetUp()
    {
        PlayCutPicture();
        _volume = GameObject.Find("Global Volume").GetComponent<Volume>();
        _volume.enabled = false;
        _skipBtn.SetActive(GameManager.Instance.GameDataInstance.isLookPrologue);
        SoundManager.Instance.Play(_bgmClip, 0.3f, 1, 1, true, "Prologue");
    }

    public void Skip()
    {
        UIManager.Instanace.ChangeSceneFade(UIDefine.UIType.Title, true);
    }

    private void PlayCutPicture()
    {
        if (canNextUp)
        {
            if (phase == _pictureCutBook.Count)
            {
                Skip();
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
        SoundManager.Instance.Stop("Prologue");
        GameManager.Instance.GameDataInstance.isLookPrologue = true;
        _volume.enabled = true;
        GameManager.Instance.SaveData();
    }
}
