using Affixx.Tools.CoursesImport;

namespace Affixx.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            var tool = CreateTool(args);
            tool.Execute();
        }

        private static ITool CreateTool(string[] args)
        {
            //for now, we have only courses importer
            //return new CoursesImporter(args);
            return new CapitalizeCourseInfo(args);
        }
    }
}
