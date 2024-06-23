/*
---
&_type=SoftwareSourceCode
&name=PoorMansLocalization.cs
&description="Simple Localization (language) class"
&codeRepository=https://github.com/jc508/poorMansLocalization
&version=1.0
&codeSampleType=CodeBit See https://filemeta.org/CodeBit.html
&keywords=CodeBit
&programmingLanguage=C#
&datePublished=2024-06-27
&dateModified=2024-06-27
&author="John Campbell"
&license=&license=https://opensource.org/licenses/BSD-3-Clause
&abstract="Simple implementation of Localization / multilingual using standard .restext files"
...
*/

/*
=== BSD 3 Clause License ===
https://opensource.org/licenses/BSD-3-Clause

Copyright 2024 John Campbell

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice,
this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation
and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its contributors
may be used to endorse or promote products derived from this software without
specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
POSSIBILITY OF SUCH DAMAGE.
*/

using System.Globalization;

class poorMansLocalization
{
    private string c_defaultResourceFolder     = "Resources";  
    private string c_defaultResourceFileSuffix = "restext";  
    private string c_defaultCulture = "";  
    private string c_cultureToUse = "";
    private string c_actualResourceFile = "";  
    private Dictionary<string, string> c_messages = new Dictionary<string, string>();      // The actual Keys and Values loaded

    /// <summary>
    /// Default constructor. Uses className + current Culture, else current Language, else default
    /// eg if current culture is 'en-AU'
    /// choices in order
    ///   1. Resources\poorMansLocalization.en-AU.restext
    ///   2. Resources\poorMansLocalization.en.restext
    ///   3. Resources\poorMansLocalization.restext
    /// </summary>
    /// <param name=None</param>
    /// <remarks>
    /// <para>Finds and loads the resource file.</para>
    /// </remarks>
    public poorMansLocalization()
    {
        // find the file file
        c_defaultCulture = CultureInfo.CurrentCulture.Name;
        Type myType = typeof(poorMansLocalization);
        // Adopt a name that doesn't get tangled in Namespaces like the full Microsoft version does
        string? fileBase = myType.Name;
        // Form a relative path
        string[] checkFileNames =  { 
                    $"{c_defaultResourceFolder}{Path.DirectorySeparatorChar}{fileBase}.{c_defaultCulture}.{c_defaultResourceFileSuffix}",
                    $"{c_defaultResourceFolder}{Path.DirectorySeparatorChar}{fileBase}.{c_defaultCulture.Substring(0,2)}.{c_defaultResourceFileSuffix}",
                    $"{c_defaultResourceFolder}{Path.DirectorySeparatorChar}{fileBase}.{c_defaultResourceFileSuffix}" };
        for (int ch = 0; ch < checkFileNames.Length; ch++)
        {
            // Console.Write(checkFileNames[ch]);            
            if (File.Exists(checkFileNames[ch]) ) 
            {
                // Console.WriteLine($" exists (choice {ch +1}.)");            
                c_actualResourceFile = checkFileNames[ch];
                break;
            }
            else {
                // Console.WriteLine($" does not exist (choice {ch +1}.)");            
            }
                
        }
        if (string.IsNullOrEmpty(c_actualResourceFile)) {
            Console.WriteLine("No resource file found");            
            throw new NotImplementedException();
        }
        // resourceFileName is populated
        LoadResourceFile();
    }    

    /// <summary>
    /// Specific constructor. Uses full filename OR a language override
    /// if filename is specified then only this name is acceptable
    /// if language is specified choices in order
    ///   1. Resources\poorMansLocalization.<language>.restext
    ///   2. Resources\poorMansLocalization.restext
    /// </summary>
    /// <param name=language    Language code eg 'cs' 'el' </param>
    /// <param name=filepath    fully qualified path to restext file</param>
    /// <remarks>
    /// <para>Finds and loads the resource file.</para>
    /// </remarks>
    public poorMansLocalization(string pLanguage = "", string pFilepath = "")
    {
        // find the file file
        // treat the parameters as OR one or the other
        // option 1. Language supplied
        if (! string.IsNullOrEmpty(pLanguage) )
        {
            c_cultureToUse = pLanguage;
            Type myType = typeof(poorMansLocalization);
            // Adopt a name that doesn't get tangled in Namespaces like the full Microsoft version does
            string? fileBase = myType.Name;
            string[] checkFileNames =  { 
                        $"{c_defaultResourceFolder}{Path.DirectorySeparatorChar}{fileBase}.{c_cultureToUse}.{c_defaultResourceFileSuffix}",
                        $"{c_defaultResourceFolder}{Path.DirectorySeparatorChar}{fileBase}.{c_defaultResourceFileSuffix}" };
            for (int ch = 0; ch < checkFileNames.Length; ch++)
            {
                //Console.Write(checkFileNames[ch]);            
                if (File.Exists(checkFileNames[ch]) ) 
                {
                    //Console.WriteLine($" exists (choice {ch}.)");            
                    c_actualResourceFile = checkFileNames[ch];
                    break;
                }
                else {
                    //Console.WriteLine($" does not exist (choice {ch}.)");            
                }
            }
        }  // end pLanguage <> ""
        // option 2. Fully qualified filepath supplied
        if (! string.IsNullOrEmpty(pFilepath)) 
        {
            if (File.Exists(pFilepath) ) 
            {
                //Console.WriteLine($" {pFilepath} exists ");            
                c_actualResourceFile = pFilepath;
            }
        }   // end pFilepath <> ""
        // give up if no resource file found
        if ( string.IsNullOrEmpty(c_actualResourceFile)) {
            Console.WriteLine("No resource file found");            
            throw new NotImplementedException();
        }
        // c_actualResourceFile is populated
        LoadResourceFile();
    }

    //************************************************************************
    /// <summary>
    /// Get the Value for the 'key' supplied
    /// </summary>
    /// <param name="Key"           The key from the restext file </param>
    /// <param name="param1"  to n  The values to substitute for positional fields ie {1} or {2}
    /// <remarks>
    /// <para>Get and formats the Value for the 'key' supplied.</para>
    /// </remarks>
    /// <returns>The 'Value' associated with this message key with placeholder variables filled in. </returns> 
    
    public string getMessage(string Key)
    {
        if (! c_messages.ContainsKey(Key))  {
            return $"!!Error MessageKey missing from resource file (key={Key})";
        }  else  {
            return c_messages[Key];
        }
    }
    public string getMessage(string Key, string param1)
    {
        string temp = getMessage(Key);
        temp = temp.Replace("{1}","param1");
        return temp;
    }
    public string getMessage(string Key, string param1, string param2)
    {
        string temp = getMessage(Key);
        temp = temp.Replace("{1}",param1);
        temp = temp.Replace("{2}",param2);
        return temp;
    }
    public string getMessage(string Key, string param1, string param2, string param3)
    {
        string temp = getMessage(Key);
        temp = temp.Replace("{1}",param1);
        temp = temp.Replace("{2}",param2);
        temp = temp.Replace("{3}",param3);
        return temp;
    }
    public string getMessage(string Key, string param1, string param2, string param3, string param4)
    {
        string temp = getMessage(Key);
        temp = temp.Replace("{1}",param1);
        temp = temp.Replace("{2}",param2);
        temp = temp.Replace("{3}",param3);
        temp = temp.Replace("{4}",param4);
        return temp;
    }
  
    //************************************************************************
    /// <summary>
    /// Print out the loaded message Distionary
    /// </summary>
    /// <remarks>
    /// <para>Prints out the loaded resource file. Just for sevalopment putposes</para>
    /// </remarks>
    /// <returns>Nothing </returns> 
    public void dumpMessages()
    {
        foreach(KeyValuePair<string, string> entry in c_messages)
        {
            Console.WriteLine($"    Message:    {entry.Key}         : {entry.Value}");      
        }
    }
    
    //************************************************************************
    private void LoadResourceFile()
    {
        var lines = File.ReadAllLines(c_actualResourceFile);
        string lKey = new string("");
        string lValue = new string("");
        for (int ndx = 0; ndx < lines.Length; ndx += 1)
        {
            var line = lines[ndx].Trim();
            if (string.IsNullOrEmpty(line)) continue;  // skip blank lines
            if (line.StartsWith("#") || line.StartsWith(";"))  continue;  // skip comments
            int equals = -1;
            equals = line.IndexOf("=");
            if (equals == -1)    // there is no '=' sign
            {
                //log it. this is a real error
                Console.WriteLine($"No '=' found in : {line}");      
                continue;
            }
           
            string[] tokens = line.Split("=",2);
            if (string.IsNullOrEmpty(tokens[0]))      // no key (keys cannot be empty but Values can)
            {
                //log it. this is a real error
                Console.WriteLine($"Key is missing from : {line}");      
                continue;
            }
            lKey   = tokens[0].Trim();
            lValue = tokens[1].Trim();
            c_messages.Add(lKey, lValue);
        }
    }    // end LoadResourceFile

}
