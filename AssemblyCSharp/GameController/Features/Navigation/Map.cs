namespace AssemblyCSharp.GameController.Features.Navigation
{
    public static class Map
    {
        // --- Trái Đất ---
        public const int LANG_ARU = 0;
        public const int VACH_NUI_ARU = 42;
        public const int DOI_HOA_CUC = 1;
        public const int THUNG_LUNG_TRE = 2;
        public const int RUNG_NAM = 3;
        public const int RUNG_XUONG = 4;
        public const int DAO_KAME = 5;
        public const int DONG_KARIN = 6;
        public const int RUNG_BAMBOO = 27;
        public const int RUNG_DUONG_XI = 28;
        public const int NAM_KAME = 29;
        public const int DAO_BULONG = 30;

        // --- Namếc ---
        public const int LANG_MORI = 7;
        public const int VACH_NUI_MOORI = 43;
        public const int DOI_NAM_TIM = 8;
        public const int THI_TRAN_MOORI = 9;
        public const int THUNG_LUNG_MAIMA = 11;
        public const int VUC_MAIMA = 12;
        public const int DAO_GURU = 13;
        public const int THUNG_LUNG_NAMEC = 10;
        public const int NUI_HOA_VANG = 31;
        public const int NUI_HOA_TIM = 32;
        public const int NAM_GURU = 33;
        public const int DONG_NAM_GURU = 34;

        // --- Xayda ---
        public const int LANG_KAKAROT = 14;
        public const int VACH_NUI_KAKAROT = 44;
        public const int DOI_HOANG = 15;
        public const int LANG_PLANT = 16;
        public const int RUNG_NGUYEN_SINH = 17;
        public const int RUNG_THONG_XAYDA = 18;
        public const int VACH_NUI_DEN = 20;
        public const int THANH_PHO_VEGETA = 19;
        public const int RUNG_CO = 35;
        public const int RUNG_DA = 36;
        public const int THUNG_LUNG_DEN = 37;
        public const int BO_VUC_DEN = 38;

        // --- Trạm Tàu Vũ Trụ ---
        public const int TRAM_TAU_VU_TRU_TRAI_DAT = 24;
        public const int TRAM_TAU_VU_TRU_NAMEC = 25;
        public const int TRAM_TAU_VU_TRU_XAYDA = 26;


        public static string GetMapName(int mapId)
        {
            switch (mapId)
            {
                case LANG_ARU: return "Làng Aru";
                case DOI_HOA_CUC: return "Đồi hoa cúc";
                case THUNG_LUNG_TRE: return "Thung lũng tre";
                case RUNG_NAM: return "Rừng nấm";
                case RUNG_XUONG: return "Rừng xương";
                case DAO_KAME: return "Đảo Kamê";
                case DONG_KARIN: return "Đông Karin";
                case LANG_MORI: return "Làng Mori";
                case DOI_NAM_TIM: return "Đồi nấm tím";
                case THI_TRAN_MOORI: return "Thị trấn Moori";
                case THUNG_LUNG_NAMEC: return "Thung lũng Namếc";
                case THUNG_LUNG_MAIMA: return "Thung lũng Maima";
                case VUC_MAIMA: return "Vực maima";
                case DAO_GURU: return "Đảo Guru";
                case LANG_KAKAROT: return "Làng Kakarot";
                case DOI_HOANG: return "Đồi hoang";
                case LANG_PLANT: return "Làng Plant";
                case RUNG_NGUYEN_SINH: return "Rừng nguyên sinh";
                case RUNG_THONG_XAYDA: return "Rừng thông Xayda";
                case THANH_PHO_VEGETA: return "Thành phố Vegeta";
                case VACH_NUI_DEN: return "Vách núi đen";
                case RUNG_BAMBOO: return "Rừng Bamboo";
                case RUNG_DUONG_XI: return "Rừng dương xỉ";
                case NAM_KAME: return "Nam Kamê";
                case DAO_BULONG: return "Đảo Bulông";
                case NUI_HOA_VANG: return "Núi hoa vàng";
                case NUI_HOA_TIM: return "Núi hoa tím";
                case NAM_GURU: return "Nam Guru";
                case DONG_NAM_GURU: return "Đông Nam Guru";
                case RUNG_CO: return "Rừng cọ";
                case RUNG_DA: return "Rừng đá";
                case THUNG_LUNG_DEN: return "Thung lũng đen";
                case BO_VUC_DEN: return "Bờ vực đen";
                case VACH_NUI_ARU: return "Vách núi Aru";
                case VACH_NUI_MOORI: return "Vách núi Moori";
                case VACH_NUI_KAKAROT: return "Vách núi Kakarot";
                case TRAM_TAU_VU_TRU_TRAI_DAT:
                case TRAM_TAU_VU_TRU_NAMEC:
                case TRAM_TAU_VU_TRU_XAYDA: return "Trạm tàu vũ trụ";
                default: return "Bản đồ lạ (" + mapId + ")";
            }
        }


        public static bool IsSpaceStation(int mapId)
        {
            return mapId == TRAM_TAU_VU_TRU_TRAI_DAT ||
                   mapId == TRAM_TAU_VU_TRU_NAMEC ||
                   mapId == TRAM_TAU_VU_TRU_XAYDA;
        }

        public static int GetMapIdByName(string text)
        {
            for (int i = 0; i <= 100; i++)
            {
                string mapName = GetMapName(i);
                if (text.ToLower().Contains(mapName.ToLower()) && !mapName.Contains("Bản đồ lạ"))
                {
                    return i; 
                }
            }
            return -1;
        }
    }
}