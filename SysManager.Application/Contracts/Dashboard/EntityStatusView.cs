﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Application.Contracts.Dashboard
{
    public class EntityStatusView
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public int Count { get; set; }
    }
}
