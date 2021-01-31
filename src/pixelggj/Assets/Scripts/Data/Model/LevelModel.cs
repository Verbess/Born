using System;
using System.Collections.Generic;
using JackUtil;

namespace PixelGGJNS {

    [Serializable]
    public class LevelModel {
        public string levelId { get; set; }
        public List<BlockModel> blocks { get; set; }
        public LevelModel(string levelId) {
            this.levelId = levelId;
            blocks = new List<BlockModel>();
        }

        public BlockModel GetBlock(int blockId) {
            return blocks.Find(value => value.id == blockId);
        }
    }

}