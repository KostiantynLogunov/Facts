﻿using Calabonga.EntityFrameworkCore.Entities.Base;
using System.Collections.Generic;

namespace Logunov.Facts.Web.Data
{
    public class Tag: Identity
    {
        public string Name { get; set; }

        public ICollection<Fact> Facts { get; set; }
    }
}
