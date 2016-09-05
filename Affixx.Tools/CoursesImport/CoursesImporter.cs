using Affixx.Core.Database;
using Affixx.Core.Database.Generated;
using Affixx.Core.Logging;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Affixx.Tools.CoursesImport
{
    public class CoursesImporter : ITool
    {
        private static readonly ILogger logger = LogManager.GetLogger();

        private readonly string inputFolderPath;
        private readonly Repository<UniversityCourse> repository;
        private readonly Dictionary<string, long> universityIdsByName;
        private readonly long createdBy;

        public CoursesImporter(string[] args)
        {
            if (args == null || args.Length < 2) {
                throw new ArgumentException("Usage: Affixx.Tools.exe <path-to-folder> <email-of-the-admin>", nameof(args));
            }

            inputFolderPath = args[0];
            var adminEmail = args[1];

            repository = new Repository<UniversityCourse>();
            universityIdsByName = new Repository<University>().Select().ToDictionary(x => x.Name, x => x.Id);
            createdBy = new Repository<User>().Select("Email=@email", new { email= adminEmail }).Single().Id;
        }

        public void Execute()
        {
            var files = Directory.EnumerateFiles(inputFolderPath).Where(x => x.EndsWith(".csv"));
            var configuration = new CsvConfiguration { HasHeaderRecord = true, IgnoreHeaderWhiteSpace = true };

            foreach (var file in files) {
                var universityName = Path.GetFileNameWithoutExtension(file);
                var universityId = universityIdsByName[universityName];
                var existingCourses = SelectExistingCourses(universityId);

                logger.Info($"Importing courses for \"{universityName}\"");

                using (var streamReader = new StreamReader(file)) {
                    using (var reader = new CsvReader(streamReader, configuration)) {
                        foreach (var record in reader.GetRecords<CourseInfo>()) {
                            if (existingCourses.Contains(record.CourseName)) {
                                logger.Warn($"Already have \"{record.CourseName}\" in the database, skipping");
                                continue;
                            }

                            var entity = new UniversityCourse {
                                Code = record.CourseCode,
                                CreatedAt = DateTime.UtcNow,
                                CreatedBy = createdBy,
                                Group = record.Group,
                                Name = record.CourseName,
                                UniversityId = universityId
                            };
                            repository.Save(entity);
                        }
                    }                        
                }
            }            
        }

        private HashSet<string> SelectExistingCourses(long universityId)
        {
            var courses = repository.Select("UniversityId=@universityId AND DeletedAt IS NULL", new { universityId });
            return new HashSet<string>(courses.Select(x => x.Name));
        }

        public class CourseInfo
        {
            public string Group { get; set; }
            public string CourseName { get; set; }
            public string CourseCode { get; set; }
        }
    }
}
