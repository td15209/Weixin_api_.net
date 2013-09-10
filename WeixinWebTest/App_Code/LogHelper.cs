/*******************************
 *	Author:	Dong [mailto:techdong@hotmail.com] 欢迎交流 Q群：289147891
 *	Date:	2013-09-05 22:18:33
 *	Desc:	
 * 
*******************************/

using System;
using System.IO;
using System.Web;

public class LogHelper
{
    public static string Path = HttpContext.Current.Server.MapPath("~/log.txt");
    public static void Log(params string[] infos)
    {
        var s = string.Join(Environment.NewLine, infos);
        var file = File.AppendText(Path);
        file.WriteLine("----------------------------- {0} -----------------------------", DateTime.Now);
        file.WriteLine(s);
        file.Flush();
        file.WriteLine("-------------------------------------------------------------------------");
        file.Close();
    }
}