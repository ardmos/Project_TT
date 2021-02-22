using System.Collections;
using System.Collections.Generic;

[System.Serializable] // <--- 파일로써 저장될 수 있도록 해주는 부분.
public class UserData
{
    public bool sfxOn, sfxOff, musicOn, musicOff;
    public int tomatoes, katchups;
    public List<User.StageStarScore> arrClearedStageStarScore;

    public UserData(User user)
    {
        sfxOn = user.GetsfxOn();
        sfxOff = user.GetsfxOff();
        musicOn = user.GetmusicOn();
        musicOff = user.GetmusicOff();
        tomatoes = user.GetTomatoesCount();
        katchups = user.GetKatchupsCount();
        arrClearedStageStarScore = user.arrClearedStageStarScore;
    }
}
