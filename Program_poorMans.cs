using System.Runtime.CompilerServices;

namespace poorMansLocalizationTest;
    /// <summary>
    /// Test program for poorMansLocalization
    /// </summary>

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("poorMansLocalization main starting **************************");
        // there is nothing to stop using multiple instances with different languages
        // Test for base functionality
        var loc = new poorMansLocalization();
        //var loc = new poorMansLocalization(pLanguage:"el");
        loc.dumpMessages();
        // Test for specified resource file
        // main pc            "D:\codejc\CS_learning\Source\for_VS_code\poorMansLocalization\testLocalization.restext";
        // Windows -- virtual
        //string testFilePath = @"P:\CS_learning\Source\for_VS_code\poorMansLocalization\testLocalization.restext";
        // linux 
        // string testFilePath = @"/home/jc/jcCode/CS_Learning/for_VS_code/poorMansLocalization/testLocalization.restext";  
        // var loc = new poorMansLocalization(pFilepath:testFilePath);
        
        // test valid message with 2 parameters
        Console.WriteLine(loc.getMessage("TestParameters", "foo wos here", "today is saturday"));
        // test invalid message key
        string result = loc.getMessage("blah", "foo wos here", "today is saturday");
        Console.WriteLine(result);

    }
}
