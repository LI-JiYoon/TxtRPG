using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    public enum STATE { 시작, 마을, 상점, 휴식하기, 던전, 인벤토리, 상태보기 }    

    public static class Location
    {
        public static STATE preLocation = STATE.시작;
        public static STATE currentLocation = STATE.시작;
        
        /// <summary>
        /// 위치 갱신.
        /// </summary>
        /// <param name="state">내가 이동할 위치를 넣어주면 됨</param>
        public static void SetLocation(STATE state)
        {
            preLocation = currentLocation;
            currentLocation = state;
        }
    }
}
