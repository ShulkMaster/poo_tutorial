using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class Entry
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;

        public virtual Pokemon Pokemon { get; set; } = null!;
    }
}
