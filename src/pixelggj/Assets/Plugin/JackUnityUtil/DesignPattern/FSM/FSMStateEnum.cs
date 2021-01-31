using System;

namespace JackUtil {

    public enum FSMStateEnum {

        Idle

    }

    public static class FSMStateEnumExtention {

        public static int ToInt(this FSMStateEnum e) {
            return (int)e;
        }

    }
}