using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class User : DontDestroy<User>
{
    #region Change Fruit
    public enum Fruits
    {
        tomato,
        banana,
        //pineapple,
        //watermelon
    }
    [SerializeField]
    private Fruits current_fruits_enum;
    [SerializeField]
    private GameObject current_fruit_Pref;
    [SerializeField]
    private GameObject[] fruit_prefabs;
    [SerializeField]
    private Sprite[] fruits_images;

    private void InitFruitPrefabArr()
    {
        try
        {
            fruit_prefabs = new GameObject[]{
            Resources.Load("Prefabs/Fruits/fruit_tomato", typeof(GameObject)) as GameObject,
            Resources.Load<GameObject>("Prefabs/Fruits/fruit_banana")
            };
            current_fruits_enum = Fruits.tomato;
            current_fruit_Pref = fruit_prefabs[(int)current_fruits_enum];
        }
        catch (Exception)
        {
            throw new Exception("크리에이터! 과일 프리팹의 경로명을 확인해주세요");
        }
    }
    private void InitFruitImages() {
        try
        {
            fruits_images = new Sprite[] {
                Resources.Load<Sprite>("Images/Fruits/apple"),
                Resources.Load<Sprite>("Images/Fruits/banana")
            };
        }
        catch (Exception)
        {
            throw new Exception("크리에이터! 과일 이미지의 경로명을 확인해주세요"); throw;
        }
    
    }    

    //StageList Scene에서 호출하는 메서드. 현재과일을 토마토로 초기화한다. 
    public void SetCurrentFruit_Tomato()
    {
        current_fruits_enum = Fruits.tomato;
        current_fruit_Pref = fruit_prefabs[(int)current_fruits_enum];
    }

    public void SetCurrentFruitToNextFruit() {
        if (fruit_prefabs.Length == (int)current_fruits_enum + 1)
        {
            current_fruits_enum = Fruits.tomato;
        }
        else {
            //Debug.Log(current_fruits_enum);
            current_fruits_enum++;
            //Debug.Log("after current_fruits_enum++ " + current_fruits_enum);
            
        }
        current_fruit_Pref = fruit_prefabs[(int)current_fruits_enum];
    }
    public GameObject GetCurrentFruit() {
        return current_fruit_Pref;
    }

    public Sprite GetCurrentFruitImages() {
        return fruits_images[(int)current_fruits_enum];
    }
    #endregion

    #region option settings
    [SerializeField]
    private bool sfxOn, sfxOff, musicOn, musicOff;
    #endregion
    #region option settings Get, Set
    public bool GetsfxOn()
    {
        return sfxOn;
    }
    public bool GetsfxOff()
    {
        return sfxOff;
    }
    public bool GetmusicOn()
    {
        return musicOn;
    }
    public bool GetmusicOff()
    {
        return musicOff;
    }

    public void SetsfxOn(bool b)
    {
        sfxOn = b;
    }
    public void SetsfxOff(bool b)
    {
        sfxOff = b;
    }
    public void SetmusicOn(bool b)
    {
        musicOn = b;
    }
    public void SetmusicOff(bool b)
    {
        musicOff = b;
    }
    #endregion

    #region Number of Tomatoes I have
    [SerializeField]
    private int tomatoes;
    #endregion
    #region Tomatoes Get, Set
    public int GetTomatoesCount()
    {
        return tomatoes;
    }
    public void AddingTomatoes(int howmany)
    {
        tomatoes += howmany;
    }
    public void TomatoHaveThrowed()
    {
        tomatoes--;
    }
    #endregion

    #region Number of Katchups I have
    [SerializeField]
    private int katchups;
    #endregion
    #region Katchup Get, Set
    public int GetKatchupsCount()
    {
        return katchups;
    }
    public void AddingKatchups(int howmany)
    {
        katchups += howmany;
    }
    #endregion

    #region ClearedStageAndStarScore
    [Serializable]
    public class StageStarScore
    {
        [SerializeField]
        private int stage, starScore;

        private StageStarScore()
        {
            stage = 0;
            starScore = 0;
        }
        public StageStarScore(int stage, int score)
        {
            this.stage = stage;
            this.starScore = score;
        }
        public int Stage()
        {
            return stage;
        }
        public int StarScore()
        {
            return starScore;
        }
        public void SetStarScore(int starscore)
        {
            this.starScore = starscore;
        }
    }
    [SerializeField]
    public List<StageStarScore> arrClearedStageStarScore;
    public void SaveClearedStageStarScore(StageStarScore stageAndScore)
    {
        StageStarScore stageStarScore = arrClearedStageStarScore.Find(x => x.Stage() == stageAndScore.Stage());
        //print("is Founded?? " + arrClearedStageStarScore.Find(x => x.Stage() == stageAndScore.Stage()));
        //기존에 클리어한 애들중 방금 클리어한 스테이지가 있으면~ 최고스코어인지 체크후에 스코어만 바꿔주고, 
        if (stageStarScore != null)
        {
            if (stageStarScore.StarScore()<=stageAndScore.StarScore())
            {
                //신기록으로 교체! 
                stageStarScore.SetStarScore(stageAndScore.StarScore());
            }
        }
        else
        {
            //없으면~~  새로 .Add해준다. 
            arrClearedStageStarScore.Add(stageAndScore);
        }

        print("Saved!");
    }
    #endregion


    #region ===== Save&Load =====
    public void SaveUser()
    {
        SaveSystem.SaveUser(this);
    }
    public void LoadUser()
    {
        UserData data = SaveSystem.LoadUser();

        if (data != null)
        {
            Debug.Log("Load Successed");
            sfxOn = data.sfxOn;
            sfxOff = data.sfxOff;
            musicOn = data.musicOn;
            musicOff = data.musicOff;
            tomatoes = data.tomatoes;
            katchups = data.katchups;
            arrClearedStageStarScore = data.arrClearedStageStarScore;
        }
    }
    #endregion

    private void Start()
    {
        InitFruitPrefabArr();
        InitFruitImages();
    }

    
}
