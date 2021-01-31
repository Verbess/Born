using System;
using System.Collections.Generic;
using JackUtil;

namespace PixelGGJNS {

    [Serializable]
    public class BlockModel {

        public int id { get; set; }
        public bool isGathered { get; set; }
        public bool isUsed { get; set; }

        public BlockModel(int id, bool isGathered, bool isUsed) {
            this.id = id;
            this.isGathered = isGathered;
            this.isUsed = isUsed;
        }

    }
}