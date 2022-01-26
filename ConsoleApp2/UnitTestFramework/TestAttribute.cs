using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestFramework
{
    public class TestClass : Attribute { }

    public class TestMethod : Attribute { }

    public class FirstMethodToTest : Attribute { }

    public class LastMethodToTest : Attribute { }
}
