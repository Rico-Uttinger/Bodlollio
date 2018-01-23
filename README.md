# About this project #
This project is a school-project. 

Replace the content of file: '/Bodlollio/Bodlollio/App_Start/NexmoData.cs' with following:
*Don't forget to replace the brackets enclosed words*
``` c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bodlollio
{
    public static class NexmoData
    {
		// Replace the in brackets ('[',']') enclosed words with your concret data
		public static string api_key = "[API-KEY]";
		public static string api_secret = "[API-SECRET]";
    }
}
```
Execute following command at project root: (optional)
``` shell
    git update-index --assume-unchanged /Bodlollio/Bodlollio/App_Start/NexmoData.cs
```

To test the required functionality: You register yourself with a
test-email and your phone-nr with the country-prefix. 
For example:
041 12 123 12 12

If you miss the country-prefix it could be that it dosn't work.

As Password you can use '123456' or anything else that has six characters.


