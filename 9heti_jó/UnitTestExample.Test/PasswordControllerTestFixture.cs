using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestExample.Test
{
    [Test, //nem tudom miért húzza alá mindet 
        TestCase("abcd", false),
        TestCase("", false), //kisbetű?
        TestCase("", false), //nagybetű?
        TestCase("", false), //túl rövid
        TestCase("", true), //megfelelő
        ] 


    class PasswordControllerTestFixture
    {
        // ????
    }
}
