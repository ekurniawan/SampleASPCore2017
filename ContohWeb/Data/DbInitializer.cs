using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ContohWeb.Models;


namespace ContohWeb.Data
{
    public static class DbInitializer
    {
        //untuk menambahkan data seeding / data diawal ketika database dibuat
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
               new Student {FirstMidName="Erick",LastName="Kurniawan",EnrollmentDate=DateTime.Parse("2016-09-01")},
               new Student {FirstMidName="Budi",LastName="Halim",EnrollmentDate=DateTime.Parse("2015-08-01")},
               new Student {FirstMidName="Djoni",LastName="Dwijana",EnrollmentDate=DateTime.Parse("2014-03-01")},
               new Student {FirstMidName="Joko",LastName="Purwanto",EnrollmentDate=DateTime.Parse("2014-03-01")},
               new Student {FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
               new Student {FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
               new Student {FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
               new Student {FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
               new Student {FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")}
            };

            foreach(var student in students)
            {
                context.Students.Add(student);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Algoritma dan Pemrograman",Credits=3},
                new Course{CourseID=4022,Title="Pemrograman Web",Credits=3},
                new Course{CourseID=4041,Title="Pemrograman Mobile",Credits=3},
                new Course{CourseID=1045,Title="Rekayasa Perangkat Lunak",Credits=3},
                new Course{CourseID=3141,Title="Basis Data",Credits=4},
                new Course{CourseID=2021,Title="Machine Learning",Credits=3},
                new Course{CourseID=2042,Title="Struktur Data",Credits=4}
            };

            foreach(var course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();

            var enrolments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
                new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
                new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
                new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
                new Enrollment{StudentID=3,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
                new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
                new Enrollment{StudentID=6,CourseID=1045},
                new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A}
            };

            foreach(var enrollment in enrolments)
            {
                context.Enrollments.Add(enrollment);
            }

            context.SaveChanges();
        }
    }
}
