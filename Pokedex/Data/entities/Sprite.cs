using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class Sprite
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public byte[] Url { get; set; } = null!;

        public virtual Pokemon IdNavigation { get; set; } = null!;
    }
}
