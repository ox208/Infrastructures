﻿using Aiursoft.Scanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Aiursoft.WebTools.Tests.Models
{
    public class DemoService : ITransientDependency
    {
        public static bool Done = false;

        public void DoSomethingSlow()
        {
            Done = false;
            Thread.Sleep(200);
            Done = true;
        }
    }
}
