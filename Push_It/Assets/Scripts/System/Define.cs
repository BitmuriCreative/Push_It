namespace Push_It
{
    public enum eStageLevel
    {
        Level_11 = 11,
        Level_21 = 21,
        Level_31 = 31,
        Level_41 = 41,
        Level_51 = 51,
        Level_61 = 61,
        Level_71 = 71,
        Level_81 = 81,
        Level_91 = 91,
        Level_101 = 101,
    }

    public enum eStageBackgound
    {
        //방 이동시 배경 변경.
        stage_room1,
        stage_room2,
        stage_room3,
        stage_room4,
        stage_room5,
        stage_room6,
        stage_room7,
        stage_room8,
        stage_room9,
        stage_room10,

        //방 이동 전 문달린 배경으로 변경.
        stage_room1_end = 10,
        stage_room2_end = 20,
        stage_room3_end = 30,
        stage_room4_end = 40,
        stage_room5_end = 50,
        stage_room6_end = 60,
        stage_room7_end = 70,
        stage_room8_end = 80,
        stage_room9_end = 90,
        stage_room10_end = 100,
    }

    public enum eAreaPosition
    {
        top,
        bottom,
    }

    public enum eAreaNumber
    {
        pos_0,
        pos_1,
        pos_2,
        pos_3,
        pos_4,
    }

    public enum eBitmuriName
    {
        none,
        small_bitmuri,
        tall_bitmuri,
        fat_bitmuri,
    }

    public enum eEffectSound
    {
        door_close,
    }
}
